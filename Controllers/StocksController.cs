using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SupplyChain.DatabaseContext;
using SupplyChain.DTOs;
using SupplyChain.Models;
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

        [HttpGet("all-data")]
        public async Task<IActionResult> GetAllData()
        {
            var products = await _context.Product
                .OrderBy(p => p.ProductID)
                .ToListAsync();

            var transactions = await _context.InventoryAuditLog
                .OrderByDescending(t => t.ChangedAt)
                .ToListAsync();

            return Ok(new
            {
                Products = products,
                StockTransactions = transactions
            });
        }

        [HttpGet("admin/orders")]
        public async Task<IActionResult> GetAllOrderedItemsForAdmin()
        {
            var data = await _context.OrderItem
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.User)
                .Include(oi => oi.Product)
                .Select(oi => new OrderItemAdminDTO
                {
                    OrderItemID = oi.OrderItemID,
                    OrderID = oi.OrderID,
                    UserEmail = oi.Order.User.Email,
                    ProductName = oi.Product.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    OrderDate = oi.Order.OrderDate,
                    Status = oi.Order.Status
                })
                .ToListAsync();

            return Ok(data);
        }




    }
}
