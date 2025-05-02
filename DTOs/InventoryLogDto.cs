namespace SupplyChain.DTOs
{
    public class InventoryLogDto
    {
        public int LogID { get; set; }
        public int ProductID { get; set; }
        public int ChangeAmount { get; set; }
        public string? Reason { get; set; }
        public DateTime ChangedAt { get; set; }

        // Optional: Include product details
        public string? ProductName { get; set; }
    }
}
