namespace SupplyChain.DTOs
{
    public class OrderItem
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }

    public class PlaceOrderRequest
    {
        public int CustomerID { get; set; }
        public List<OrderItem> Items { get; set; }
    }

}
