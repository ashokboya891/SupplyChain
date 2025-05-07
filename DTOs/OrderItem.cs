namespace SupplyChain.DTOs
{
    public class OrderItems
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }

    public class PlaceOrderRequest
    {
        public int CustomerID { get; set; }
        public List<OrderItems> Items { get; set; }
    }

}
