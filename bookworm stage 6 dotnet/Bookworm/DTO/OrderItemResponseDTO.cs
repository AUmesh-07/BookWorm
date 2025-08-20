namespace Bookworm.ResponseDTO
{
    public class OrderItemResponseDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string AcquisitionType { get; set; }
        public double Price { get; set; }
        public DateTime? RentalEndDate { get; set; }
    }
}
