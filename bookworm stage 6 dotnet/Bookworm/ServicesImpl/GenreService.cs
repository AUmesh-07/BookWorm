using Bookworm.Exceptions;
using Bookworm.Models;
using Bookworm.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookworm.Services.Impl
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<Genre> SaveGenre(Genre genre)
        {
            return await _genreRepository.Save(genre);
        }

        public async Task<Genre> GetGenreById(int id)
        {
            return await _genreRepository.GetById(id)
                ?? throw new NotFoundException($"Genre not found with id: {id}");
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _genreRepository.GetAll();
        }

        public async Task DeleteGenre(int id)
        {
            if (!await _genreRepository.ExistsById(id))
            {
                throw new NotFoundException($"Genre not found with id: {id}");
            }
            await _genreRepository.DeleteById(id);
        }
    }
}