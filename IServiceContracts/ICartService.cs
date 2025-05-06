using SupplyChain.DTOs;

namespace SupplyChain.IServiceContracts
{
    public interface ICartService
    {
        Task<List<CartItem>> GetUserCartAsync(string userId);
        Task AddItemToCartAsync(CartItem item);
        Task RemoveItemFromCartAsync(string userId, string productId);
        Task ClearUserCartAsync(string userId);
        // Add new method for updating cart item quantity
        Task UpdateCartItemQuantityAsync(string userId, string productId, int quantity);
    }
}
