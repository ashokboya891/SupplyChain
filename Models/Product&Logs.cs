using System.ComponentModel.DataAnnotations;

namespace SupplyChain.Models
{
    public class ProductTblData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
            //public ICollection<OrderItemProd> OrderItems { get; set; } = new List<OrderItemProd>();

    }
    public class StockTransaction
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int QuantityChanged { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Product? Product { get; set; }
    }

    //public class OrderItemWithProductDTO
    //{
    //    [Key]
    //    public int OrderItemID { get; set; }

    //    public int OrderID { get; set; }
    //    public int ProductID { get; set; }
    //    public int Quantity { get; set; }
    //    public decimal UnitPrice { get; set; }
    //    public string ProductName { get; set; } = string.Empty;
    //    public virtual Orders? Order { get; set; }
    //    public virtual Product? Product { get; set; }
    //}

}
