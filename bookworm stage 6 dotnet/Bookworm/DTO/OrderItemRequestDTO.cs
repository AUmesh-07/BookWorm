namespace Bookworm.RequestDTO
{
    public class OrderItemRequestDTO
    {
        public int ProductId { get; set; }
        public string AcquisitionType { get; set; }
        public int? RentalPeriodDays { get; set; }
    }
}
