using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyChain.DatabaseContext;

namespace SupplyChain.Controllers
{
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
    }
}
