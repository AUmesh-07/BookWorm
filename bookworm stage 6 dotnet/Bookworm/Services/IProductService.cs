using System.Collections.Generic;
using System.Threading.Tasks;
using Bookworm.DTO;
using Bookworm.Dtos.Request;
using Bookworm.Dtos.Response;

namespace Bookworm.Services
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateProduct(ProductRequestDto requestDto);
        Task<ProductResponseDto> UpdateProduct(int id, ProductRequestDto requestDto);
        Task<ProductResponseDto> GetProductById(int id);
        Task<List<ProductResponseDto>> GetAllProducts();
        Task DeleteProduct(int id);
        Task<List<ProductResponseDto>> FindProductsByAuthor(string authorName);
        Task<List<ProductResponseDto>> FindProductsByGenre(int genreId);
        Task<List<ProductResponseDto>> FindProductsByName(string name);
        Task<List<ProductResponseDto>> FindProductsByLanguage(int languageId);
    }
}