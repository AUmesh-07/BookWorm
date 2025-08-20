using Bookworm.Dtos.Request;
using Bookworm.Dtos.Response;
using Bookworm.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("api/v1/carts")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CartResponseDto>> GetCart([FromRoute] int customerId)
        {
            var cart = await _cartService.GetCartByCustomerId(customerId);
            return Ok(cart);
        }

        [HttpPost("{customerId}/items")]
        public async Task<ActionResult<CartResponseDto>> AddProductToCart(
            [FromRoute] int customerId,
            [FromBody] CartItemRequestDto requestDto)
        {
            var updatedCart = await _cartService.AddProductToCart(customerId, requestDto);
            return Ok(updatedCart);
        }

        [HttpDelete("{customerId}/items/{productId}")]
        public async Task<ActionResult<CartResponseDto>> RemoveProductFromCart(
            [FromRoute] int customerId,
            [FromRoute] int productId)
        {
            var updatedCart = await _cartService.RemoveProductFromCart(customerId, productId);
            return Ok(updatedCart);
        }
    }
}