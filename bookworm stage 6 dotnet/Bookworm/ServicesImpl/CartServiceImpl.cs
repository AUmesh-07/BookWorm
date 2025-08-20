using Bookworm.Dtos.Request;
using Bookworm.Dtos.Response;
using Bookworm.Models;
using Bookworm.Repository;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Bookworm.Repositories;
using Bookworm.Exceptions;
namespace Bookworm.Services.Impl
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public CartService(ICartRepository cartRepository, ICartDetailRepository cartDetailRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public async Task<CartResponseDto> AddProductToCart(int customerId, CartItemRequestDto requestDto)
        {
            var cart = await GetOrCreateActiveCart(customerId);
            var product = await _productRepository.GetById(requestDto.ProductId)
                ?? throw new NotFoundException($"Product not found with id: {requestDto.ProductId}");

            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == requestDto.ProductId);

            if (cartDetail == null)
            {
                cartDetail = new CartDetail { Cart = cart, Product = product, ProductId = product.Id };
                cart.CartDetails.Add(cartDetail);
            }

            cartDetail.IsRented = requestDto.IsRented;

            if (requestDto.IsRented)
            {
                cartDetail.RentNumberOfDays = requestDto.RentNumberOfDays;
                cartDetail.OfferCost = (product.RentPerDay ?? 0) * (decimal)requestDto.RentNumberOfDays.GetValueOrDefault();
            }
            else
            {
                cartDetail.RentNumberOfDays = null;
                cartDetail.OfferCost = product.OfferPrice??0;
            }

            RecalculateCartTotal(cart);
            var savedCart = await _cartRepository.Save(cart);
            return ToCartResponseDto(savedCart);
        }

        public async Task<CartResponseDto> RemoveProductFromCart(int customerId, int productId)
        {
            var cart = await GetActiveCart(customerId);
            var cartDetail = await _cartDetailRepository.FindByCartIdAndProductId(cart.Id, productId)
                ?? throw new NotFoundException("Product not found in cart");

            cart.CartDetails.Remove(cartDetail);
            _cartDetailRepository.Delete(cartDetail);

            RecalculateCartTotal(cart);
            var savedCart = await _cartRepository.Save(cart);
            return ToCartResponseDto(savedCart);
        }

        public async Task<CartResponseDto> GetCartByCustomerId(int customerId)
        {
            var cart = await GetActiveCart(customerId);
            return ToCartResponseDto(cart);
        }

        private async Task<Cart> GetActiveCart(int customerId)
        {
            return await _cartRepository.FindByCustomerIdAndIsActiveTrue(customerId)
                ?? throw new NotFoundException($"No active cart found for customer id: {customerId}");
        }

        private async Task<Cart> GetOrCreateActiveCart(int customerId)
        {
            var cart = await _cartRepository.FindByCustomerIdAndIsActiveTrue(customerId);
            if (cart != null)
            {
                return cart;
            }

            var customer = await _customerRepository.GetById(customerId)
                ?? throw new NotFoundException($"Customer not found with id: {customerId}");

            var newCart = new Cart
            {
                Customer = customer,
                CustomerId = customerId,
                IsActive = true,
                Cost = 0,
                CartDetails = new HashSet<CartDetail>()
            };

            return await _cartRepository.Save(newCart);
        }

        private void RecalculateCartTotal(Cart cart)
        {
            cart.Cost = cart.CartDetails.Sum(cd => cd.OfferCost);
        }

        // --- MANUAL MAPPING METHODS ---
        private CartResponseDto ToCartResponseDto(Cart cart)
        {
            return new CartResponseDto
            {
                CartId = cart.Id,
                CustomerId = cart.CustomerId,
                TotalCost = cart.Cost ??0,
                TotalItems = cart.CartDetails.Count,
                Items = cart.CartDetails.Select(ToCartItemResponseDto).ToList()
            };
        }

        private CartItemResponseDto ToCartItemResponseDto(CartDetail cartDetail)
        {
            return new CartItemResponseDto
            {
                ProductId = cartDetail.ProductId,
                ProductName = cartDetail.Product.Name,
                Author = cartDetail.Product.Author,
                ImageSource = cartDetail.Product.ImageSource,
                ShortDescription = cartDetail.Product.ShortDescription,
                ItemCost = cartDetail.OfferCost,
                IsRented = cartDetail.IsRented,
                RentNumberOfDays = cartDetail.RentNumberOfDays,
                Quantity = 1 // Assuming quantity is always 1 per line item
            };
        }
    }
}