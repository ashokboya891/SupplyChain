using SupplyChain.DTOs;
using SupplyChain.IReposioryContracts;
using SupplyChain.IServiceContracts;

namespace SupplyChain.ServiceContracts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<CartItem>> GetUserCartAsync(string userId)
        {
            return await _cartRepository.GetCartAsync(userId);
        }

        public async Task AddItemToCartAsync(CartItem item)
        {
            await _cartRepository.AddToCartAsync(item);
        }

        public async Task RemoveItemFromCartAsync(string userId, string productId)
        {
            await _cartRepository.RemoveFromCartAsync(userId, productId);
        }

        public async Task ClearUserCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }
    }
}
