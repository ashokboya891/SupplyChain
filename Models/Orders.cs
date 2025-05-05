using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SupplyChain.Models
{
    //[NotMapped]
    [Table("Orders")]
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }


        [Required]
        public string UserId { get; set; }  // Foreign key
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }  // Navigation property

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        [Range(0.01, 9999999999.99)]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Default value

        // Navigation properties
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }

}
