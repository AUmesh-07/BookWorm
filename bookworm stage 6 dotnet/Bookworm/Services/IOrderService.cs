using Bookworm.RequestDTO;
using Bookworm.ResponseDTO;

namespace Bookworm.Services
{
   
        public interface IOrderService
        {
            Task<OrderResponseDTO> CreateOrderAsync(OrderRequestDTO orderRequest);
            Task<OrderResponseDTO> CreateOrderFromCartAsync(int customerId);
        }
    
}
