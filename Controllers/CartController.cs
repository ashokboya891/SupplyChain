using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.DTOs;
using SupplyChain.IServiceContracts;

namespace SupplyChain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Get the cart for the user
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetUserCartAsync(userId);
            return Ok(cart);
        }

        // Add item to the cart
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItem item)
        {
            await _cartService.AddItemToCartAsync(item);
            return Ok("Item added to cart");
        }

        // Remove item from the cart
        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(string userId, string productId)
        {
            await _cartService.RemoveItemFromCartAsync(userId, productId);
            return Ok("Item removed from cart");
        }

        // Clear the cart
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            await _cartService.ClearUserCartAsync(userId);
            return Ok("Cart cleared");
        }

        // New endpoint to update cart item quantity
        [HttpPut("{userId}/{productId}")]
        public async Task<IActionResult> UpdateCartItemQuantity(string userId, string productId, [FromBody] int quantity)
        {
            await _cartService.UpdateCartItemQuantityAsync(userId, productId, quantity);
            return Ok("Cart item quantity updated");
        }
    }
}
