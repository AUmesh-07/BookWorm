using Bookworm.Exceptions;
using Bookworm.Models;
using Bookworm.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookworm.Services.Impl
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<Language> SaveLanguage(Language language)
        {
            return await _languageRepository.Save(language);
        }

        public async Task<Language> GetLanguageById(int id)
        {
            return await _languageRepository.GetById(id)
                ?? throw new NotFoundException($"Language not found with id: {id}");
        }

        public async Task<List<Language>> GetAllLanguages()
        {
            return await _languageRepository.GetAll();
        }

        public async Task DeleteLanguage(int id)
        {
            if (!await _languageRepository.ExistsById(id))
            {
                throw new NotFoundException($"Language not found with id: {id}");
            }
            await _languageRepository.DeleteById(id);
        }
    }
}