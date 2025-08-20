using System.Collections.Generic;

namespace Bookworm.Dtos.Response
{
    public class CartResponseDto
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalCost { get; set; }
        public int TotalItems { get; set; }
        public List<CartItemResponseDto> Items { get; set; }
    }
}