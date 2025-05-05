namespace SupplyChain.DTOs
{
    public class CartItem
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
