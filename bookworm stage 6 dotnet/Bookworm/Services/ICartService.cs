using System.Threading.Tasks;
using Bookworm.Dtos.Request;
using Bookworm.Dtos.Response;

namespace Bookworm.Services
{
    public interface ICartService
    {
        Task<CartResponseDto> AddProductToCart(int customerId, CartItemRequestDto requestDto);
        Task<CartResponseDto> RemoveProductFromCart(int customerId, int productId);
        Task<CartResponseDto> GetCartByCustomerId(int customerId);
    }
}