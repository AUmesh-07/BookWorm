using Bookworm.ResponseDTO;

namespace Bookworm.Services
{
    public interface IUserLibraryService
    {
        Task<ShelfResponseDTO> GetShelfForCustomerAsync(int customerId);
        Task<LibraryResponseDTO> GetLibraryForCustomerAsync(int customerId);
    }
}
