namespace Bookworm.DTO
{
    public class InvoiceDto
    {
        public long InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<InvoiceDetailDto> InvoiceDetails { get; set; }
        public List<InvoiceDetailDto> Items { get; set; }
    }
}
