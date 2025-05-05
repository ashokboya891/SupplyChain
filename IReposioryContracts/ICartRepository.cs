using SupplyChain.DTOs;

namespace SupplyChain.IReposioryContracts
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetCartAsync(string userId);
        Task AddToCartAsync(CartItem item);
        Task RemoveFromCartAsync(string userId, string productId);
        Task ClearCartAsync(string userId);

        //Task<List<CartItem>> Checkout(string userId);
    }
}
