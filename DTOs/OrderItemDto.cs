namespace SupplyChain.DTOs
{
    public class OrderItemDto
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        // Optional: Include product details
        public string? ProductName { get; set; }
    }
}
