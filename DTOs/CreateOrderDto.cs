namespace SupplyChain.DTOs
{
    // DTOs/CreateOrderDto.cs
    public class CreateOrderDto
    {
        public int CustomerID { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    //public class OrderItemDto
    //{
    //    public int ProductID { get; set; }
    //    public int Quantity { get; set; }
    //}
}
