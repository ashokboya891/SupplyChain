namespace SupplyChain.DTOs
{
    public class StockRequestDto
    {
        public int RequestID { get; set; }
        public int ProductID { get; set; }
        public int CurrentStock { get; set; }
        public DateTime RequestedOn { get; set; }
        public string Status { get; set; }

        // Optional: Include product details
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}
