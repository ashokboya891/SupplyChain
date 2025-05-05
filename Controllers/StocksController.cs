using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SupplyChain.DatabaseContext;
using System.Data;

namespace SupplyChain.Controllers
{
    [Authorize(policy: "RequireAdminRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public StocksController(ApplicationDbContext con)
        {
            _context=con;
        }
        // GET api/restock/pending
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequests()
        {
            var requests = await _context.RestockRequest
                .Where(r => r.Status == "Pending")
                .Include(r => r.Product)
                .Select(r => new {
                    r.RequestID,
                    r.ProductID,
                    ProductName = r.Product.ProductName,
                    r.CurrentStock,
                    r.Product.ReorderLevel,
                    r.RequestedOn
                })
                .ToListAsync();

            return Ok(requests);
        }
        // POST api/restock/{id}/fulfill
        [HttpPost("[action]")]
        public async Task<IActionResult> FulfillRequest(int requestId, int quantity)
        {
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "usp_FulfillRestockRequest";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RequestID", requestId));
            cmd.Parameters.Add(new SqlParameter("@ReceivedQuantity", quantity));

            try
            {
                await cmd.Connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return NoContent();
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }
        }

        public record FulfillRequestDto(int Quantity);
    }
}
