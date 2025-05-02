using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Models
{
    [Table("Customers")] // Matches your table name
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; } // Nullable since it's checked in DB

        [StringLength(15)]
        public string? Phone { get; set; } // Nullable

        [StringLength(255)]
        public string? Address { get; set; } // Nullable

        public DateTime? CreatedAt { get; set; } // Nullable
    }
}
