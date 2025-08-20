namespace Bookworm.ResponseDTO
{
    public class ShelfResponseDTO
    {
        public List<ShelfItemDTO> Items { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
