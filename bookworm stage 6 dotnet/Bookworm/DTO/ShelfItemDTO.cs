namespace Bookworm.ResponseDTO
{
    public class ShelfItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Author { get; set; }
        public string ImageSource { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }
}
