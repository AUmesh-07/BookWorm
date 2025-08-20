namespace Bookworm.DTO
{
    public class InvoiceDetailDto
    {
        public int InvDtlId { get; set; }
        public int Quantity { get; set; }
        public int? RentNoOfDays { get; set; }
        public decimal RoyaltyAmount { get; set; }
        public decimal SellPrice { get; set; }
        public string TranType { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductAuthor { get; set; }
        public string ProductType { get; set; }
    }
}
