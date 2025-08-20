using Bookworm.Models;

namespace Bookworm.Services
{
    public interface IRoyaltyCalculationService
    {
        Task CalculateRoyaltyForInvoiceAsync(Invoice invoice);
    }
}
