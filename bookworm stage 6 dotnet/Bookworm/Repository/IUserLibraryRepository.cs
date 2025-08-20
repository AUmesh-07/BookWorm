using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookworm.Models;

namespace Bookworm.Repository
{
    public interface IUserLibraryRepository
    {
        Task<UserLibrary> SaveAsync(UserLibrary entity);

        Task<UserLibrary?> FindByIdAsync(long userLibraryId);

        Task<List<UserLibrary>> FindByCustomerIdAndAcquisitionTypeAndStatusAsync(
            int customerId,
            string acquisitionType,
            string status
        );
    }

    public class UserLibraryRepository : IUserLibraryRepository
    {
        private readonly BookwormDbContext _context;

        public UserLibraryRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<UserLibrary> SaveAsync(UserLibrary entity)
        {
            if (entity.UserLibraryId == 0)
                _context.UserLibraries.Add(entity);
            else
                _context.UserLibraries.Update(entity);

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserLibrary?> FindByIdAsync(long userLibraryId)
        {
            return await _context.UserLibraries
                                 .FirstOrDefaultAsync(ul => ul.UserLibraryId == userLibraryId);
        }

        public async Task<List<UserLibrary>> FindByCustomerIdAndAcquisitionTypeAndStatusAsync(
            int customerId,
            string acquisitionType,
            string status
        )
        {
            return await _context.UserLibraries
                                 .Where(ul => ul.CustomerId == customerId
                                              && ul.AcquisitionType == acquisitionType
                                              && ul.Status == status)
                                 .ToListAsync();
        }
    }
}
