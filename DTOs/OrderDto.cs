namespace SupplyChain.DTOs
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        // Optional: Include customer details
        public string? CustomerName { get; set; }

        // Optional: Include order items
        public List<OrderItemDto>? Items { get; set; }
    }
}
