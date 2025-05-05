using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.Models
{
    //[NotMapped]
    [Table("RestockRequest")]
    public class RestockRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CurrentStock { get; set; }

        [Required]
        public DateTime RequestedOn { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Default value

        // Navigation property
        public virtual Product? Product { get; set; }
    }
}
