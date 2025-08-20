using Bookworm.Models;
using Bookworm.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Repositories
{
    public interface ILanguageRepository
    {
        Task<Language> Save(Language language);
        Task<Language?> GetById(int id);
        Task<List<Language>> GetAll();
        Task DeleteById(int id);
        Task<bool> ExistsById(int id);
    }
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookwormDbContext _context;

        public LanguageRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<Language> Save(Language language)
        {
            if (language.Id == 0)
            {
                _context.Languages.Add(language);
            }
            else
            {
                _context.Languages.Update(language);
            }
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task<Language?> GetById(int id)
        {
            return await _context.Languages.FindAsync(id);
        }

        public async Task<List<Language>> GetAll()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task DeleteById(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            if (language != null)
            {
                _context.Languages.Remove(language);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Languages.AnyAsync(l => l.Id == id);
        }
    }

}