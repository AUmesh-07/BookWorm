namespace Bookworm.RequestDTO
{
    public class OrderRequestDTO
    {
        public int CustomerId { get; set; }
        public List<OrderItemRequestDTO> Items { get; set; }
    }
}
