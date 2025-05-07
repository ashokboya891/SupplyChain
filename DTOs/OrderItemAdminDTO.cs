namespace SupplyChain.DTOs
{
    public class OrderItemAdminDTO
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public string UserEmail { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
