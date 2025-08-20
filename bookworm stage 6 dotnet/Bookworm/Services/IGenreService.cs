using Bookworm.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookworm.Services
{
    public interface IGenreService
    {
        Task<Genre> SaveGenre(Genre genre);
        Task<Genre> GetGenreById(int id);
        Task<List<Genre>> GetAllGenres();
        Task DeleteGenre(int id);
    }
}