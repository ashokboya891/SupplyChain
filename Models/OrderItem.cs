using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.Models
{
    //[NotMapped]
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemID { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(0.01, 100000)]
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public virtual Orders? Order { get; set; }
        public virtual Product? Product { get; set; }
    }

}
