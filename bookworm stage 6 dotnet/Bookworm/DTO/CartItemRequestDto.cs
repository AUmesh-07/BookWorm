using System.Text.Json.Serialization;

namespace Bookworm.Dtos.Request
{
    public class CartItemRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;

        [JsonPropertyName("isRented")]
        public bool IsRented { get; set; }

        public int? RentNumberOfDays { get; set; }
    }
}