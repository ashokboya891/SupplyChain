using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.Models
{
    [Table("InventoryAuditLog")] // Matches your table name
    public class InventoryAuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Required]
        public int ChangeAmount { get; set; } // Positive = Add, Negative = Subtract

        [StringLength(255)]
        public string? Reason { get; set; } // Nullable

        [Required]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        // Navigation property (if using EF Core relationships)
        public virtual Product? Product { get; set; }
    }
}
