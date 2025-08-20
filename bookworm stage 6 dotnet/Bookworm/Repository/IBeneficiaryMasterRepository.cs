using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookworm.Models; // replace with your actual models namespace

namespace Bookworm.Repository
{
    public interface IBeneficiaryMasterRepository
    {
        Task<BeneficiaryMaster?> GetById(int beneficiaryId);

        Task AddAsync(BeneficiaryMaster entity);

        Task SaveChangesAsync();
    }


    public class BeneficiaryMasterRepository : IBeneficiaryMasterRepository
    {
        private readonly BookwormDbContext _context;

        public BeneficiaryMasterRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<BeneficiaryMaster?> GetById(int beneficiaryId)
        {
            return await _context.BeneficiaryMasters
                .FirstOrDefaultAsync(b => b.BenId == beneficiaryId);
        }

        public async Task AddAsync(BeneficiaryMaster entity)
        {
            await _context.BeneficiaryMasters.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
