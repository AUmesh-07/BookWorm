namespace Bookworm.ResponseDTO
{
    public class OrderResponseDTO
    {
        public long InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemResponseDTO> Items { get; set; }
    }
}
