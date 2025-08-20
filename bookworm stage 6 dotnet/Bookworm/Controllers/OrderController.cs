using Bookworm.Repository;
using Bookworm.RequestDTO;
using Bookworm.ResponseDTO;
using Bookworm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize(Roles = "ROLE_USER")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerRepository _customerRepository;

        public OrdersController(IOrderService orderService, ICustomerRepository customerRepository)
        {
            _orderService = orderService;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Creates a new order from request body items.
        /// POST: /api/orders/create
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<OrderResponseDTO>> CreateOrder([FromBody] OrderRequestDTO orderRequest)
        {
            if (!await IsAuthorizedCustomerAsync(orderRequest.CustomerId))
            {
                return Forbid();
            }

            var createdOrder = await _orderService.CreateOrderAsync(orderRequest);
            return CreatedAtAction(nameof(CreateOrder), new { id = createdOrder.InvoiceId }, createdOrder);
        }

        /// <summary>
        /// Creates a new order directly from the customer's active shopping cart.
        /// POST: /api/orders/create-from-cart/{customerId}
        /// </summary>
        [HttpPost("create-from-cart/{customerId}")]
        public async Task<ActionResult<OrderResponseDTO>> CreateOrderFromCart([FromRoute] int customerId)
        {
            if (await IsAuthorizedCustomerAsync(customerId))
            {
                // Only run order creation if the user is authorized
                var createdOrder = await _orderService.CreateOrderFromCartAsync(customerId);
                return CreatedAtAction(nameof(CreateOrderFromCart), new { id = createdOrder.InvoiceId }, createdOrder);
            }
            else
            {
                // Return forbidden if the user is NOT authorized
               return Forbid();
            }
        }

        /// <summary>
        /// Validates the authenticated user matches the customer ID.
        /// </summary>
        //private async Task<bool> IsAuthorizedCustomerAsync(int customerId)
        //{
        //    var currentUsername = User.FindFirstValue(ClaimTypes.Name); // username/email from JWT
        //    var customer = await _customerRepository.GetById(customerId);
        //    return customer != null && customer.Email == currentUsername;
        //}


        private async Task<bool> IsAuthorizedCustomerAsync(int customerId)
        {
            // Use ClaimTypes.Email for clarity and to handle tokens that don't have a 'name' claim.
            var currentUsername = User.FindFirstValue(ClaimTypes.Email);

            // Check if the user is authenticated at all.
            if (string.IsNullOrEmpty(currentUsername))
            {
                return false;
            }

            var customer = await _customerRepository.GetById(customerId);

            // Perform a null check and a case-insensitive comparison for emails.
            return customer != null && string.Equals(customer.Email, currentUsername, StringComparison.OrdinalIgnoreCase);
        }
    }
}
