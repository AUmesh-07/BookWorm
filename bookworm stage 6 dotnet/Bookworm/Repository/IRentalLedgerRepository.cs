using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookworm.Models;
using System;

namespace Bookworm.Repository
{
    public interface IRentalLedgerRepository
    {
        Task<RentalLedger> SaveAsync(RentalLedger ledger);
        Task<RentalLedger?> FindByIdAsync(long id);
    }

    public class RentalLedgerRepository : IRentalLedgerRepository
    {
        private readonly BookwormDbContext _context;

        public RentalLedgerRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<RentalLedger> SaveAsync(RentalLedger ledger)
        {
            _context.RentalLedgers.Add(ledger);
            await _context.SaveChangesAsync();
            return ledger;
        }

        public async Task<RentalLedger?> FindByIdAsync(long id)
        {
            return await _context.RentalLedgers
                .Include(l => l.UserLibrary)
                .FirstOrDefaultAsync(l => l.UserLibraryId == id);
        }
    }
}
