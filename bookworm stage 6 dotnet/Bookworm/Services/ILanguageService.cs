using Bookworm.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookworm.Services
{
    public interface ILanguageService
    {
        Task<Language> SaveLanguage(Language language);
        Task<Language> GetLanguageById(int id);
        Task<List<Language>> GetAllLanguages();
        Task DeleteLanguage(int id);
    }
}