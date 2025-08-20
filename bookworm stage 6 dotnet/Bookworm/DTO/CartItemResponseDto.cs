namespace Bookworm.Dtos.Response
{
    public class CartItemResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Author { get; set; }
        public string ImageSource { get; set; }
        public string ShortDescription { get; set; }
        public decimal ItemCost { get; set; }
        public int Quantity { get; set; }
        public bool IsRented { get; set; }
        public int? RentNumberOfDays { get; set; }
    }
}