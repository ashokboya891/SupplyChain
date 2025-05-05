using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using SupplyChain.DatabaseContext;
using SupplyChain.DTOs;
using SupplyChain.IServiceContracts;
using SupplyChain.Models;
using System.Data;
using System.Security.Claims;

namespace SupplyChain.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _service;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public CustomersController(ICustomerService service,ILogger<CustomersController> logger, 
            ApplicationDbContext con,IConfiguration config)
        {
            _service = service;
            this._logger = logger;
            this._context = con;
            this._config = config;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> GetAll(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 2)
        {
            var customers = await _service.GetAllAsync(pageIndex, pageSize);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            return customer != null ? Ok(customer) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CreateCustomerDto dto)
        {
            var customer = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = customer.CustomerID }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Product.ToListAsync();
            return Ok(products);
        }

        [HttpPost("[action]")]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            // Logic to add item to cart
            return Ok("Item added to cart");
        }

        [HttpPost]
        [Route("api/place-order")]
        public IActionResult PlaceOrder([FromBody] PlaceOrderRequest request)
        {
            Guid userId = GetUserId();

            var connectionString = _config.GetConnectionString("con");

            var dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));

            foreach (var item in request.Items)
            {
                dt.Rows.Add(item.ProductID, item.Quantity);
            }

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("usp_PlaceOrder", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                var tvpParam = command.Parameters.AddWithValue("@Items", dt);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "OrderItemType";

                connection.Open();
                command.ExecuteNonQuery();
            }

            return Ok("Order placed successfully");
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
