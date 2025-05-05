    using Microsoft.Extensions.Caching.Distributed;
    using SupplyChain.DTOs;
    using SupplyChain.IReposioryContracts;
    using System.Text.Json;


namespace SupplyChain.ReposioryContracts
{

    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redis;

        public CartRepository(IDistributedCache redis)
        {
            _redis = redis;
        }

        public async Task<List<CartItem>> GetCartAsync(string userId)
        {
            string cartKey = $"cart:{userId}";
            var cartJson = await _redis.GetStringAsync(cartKey);

            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItem>();

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        public async Task AddToCartAsync(CartItem item)
        {
            string cartKey = $"cart:{item.UserId}";
            var cart = await GetCartAsync(item.UserId);

            var existingItem = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem != null)
                existingItem.Quantity += item.Quantity;
            else
                cart.Add(item);

            await _redis.SetStringAsync(cartKey, JsonSerializer.Serialize(cart));
        }

        public async Task RemoveFromCartAsync(string userId, string productId)
        {
            string cartKey = $"cart:{userId}";
            var cart = await GetCartAsync(userId);

            cart.RemoveAll(x => x.ProductId == productId);
            await _redis.SetStringAsync(cartKey, JsonSerializer.Serialize(cart));
        }

        public async Task ClearCartAsync(string userId)
        {
            string cartKey = $"cart:{userId}";
            await _redis.RemoveAsync(cartKey);
        }
        //public async Task<List<CartItem>> Checkout(string userId)
        //{
        //    string cartKey = $"cart:{userId}";
        //    var cart = await GetCartAsync(userId);

        //    if (string.IsNullOrEmpty(cartJson))
        //        return new List<CartItem>();

        //    return JsonSerializer.Deserialize<List<CartItem>>(cartJson);

        //}
    }
}
