namespace SupplyChain.DTOs
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime CreatedAt { get; set; }

        // Optional: Computed property
        public bool NeedsReorder => Stock <= ReorderLevel;
    }
}
