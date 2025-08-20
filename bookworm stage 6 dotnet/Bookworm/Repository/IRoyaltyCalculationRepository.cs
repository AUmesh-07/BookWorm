using System.Threading.Tasks;
using Bookworm.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Repository
{
    public interface IRoyaltyCalculationRepository
    {
        Task SaveAsync(RoyaltyCalculation royaltyCalculation);
    }

    public class RoyaltyCalculationRepository : IRoyaltyCalculationRepository
    {
        private readonly BookwormDbContext _context;

        public RoyaltyCalculationRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(RoyaltyCalculation royaltyCalculation)
        {
            _context.RoyaltyCalculations.Add(royaltyCalculation);
            await _context.SaveChangesAsync();
        }
    }
}
