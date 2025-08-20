namespace Bookworm.ResponseDTO
{
    public class LibraryItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Author { get; set; }
        public string ImageSource { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? RentalExpiryDate { get; set; }
    }
}
