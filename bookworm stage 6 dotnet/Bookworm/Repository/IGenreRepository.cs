using Bookworm.Models;
using Bookworm.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> Save(Genre genre);
        Task<Genre?> GetById(int id);
        Task<List<Genre>> GetAll();
        Task DeleteById(int id);
        Task<bool> ExistsById(int id);
    }
    public class GenreRepository : IGenreRepository
    {
        private readonly BookwormDbContext _context;

        public GenreRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> Save(Genre genre)
        {
            if (genre.Id == 0)
            {
                _context.Genres.Add(genre);
            }
            else
            {
                _context.Genres.Update(genre);
            }
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task DeleteById(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}