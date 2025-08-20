using Bookworm.DTO;
using Bookworm.Dtos.Request;
using Bookworm.Dtos.Response;
using Bookworm.Exceptions;
using Bookworm.Models;
using Bookworm.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookworm.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductService(IProductRepository productRepository, IGenreRepository genreRepository, ILanguageRepository languageRepository, IProductTypeRepository productTypeRepository)
        {
            _productRepository = productRepository;
            _genreRepository = genreRepository;
            _languageRepository = languageRepository;
            _productTypeRepository = productTypeRepository;
        }

        public async Task<ProductResponseDto> CreateProduct(ProductRequestDto requestDto)
        {
            var product = new Product();
            await MapDtoToEntity(requestDto, product);
            var savedProduct = await _productRepository.Save(product);
            return ToResponseDto(savedProduct);
        }

        public async Task<ProductResponseDto> UpdateProduct(int id, ProductRequestDto requestDto)
        {
            var existingProduct = await _productRepository.GetById(id)
                ?? throw new NotFoundException($"Product not found with id: {id}");

            await MapDtoToEntity(requestDto, existingProduct);
            var updatedProduct = await _productRepository.Save(existingProduct);
            return ToResponseDto(updatedProduct);
        }

        public async Task<ProductResponseDto> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id)
                ?? throw new NotFoundException($"Product not found with id: {id}");

            return ToResponseDto(product);
        }

        public async Task<List<ProductResponseDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAll();
            return products.Select(ToResponseDto).ToList();
        }

        public async Task DeleteProduct(int id)
        {
            if (!await _productRepository.ExistsById(id))
            {
                throw new NotFoundException($"Product not found with id: {id}");
            }
            await _productRepository.DeleteById(id);
        }

        public async Task<List<ProductResponseDto>> FindProductsByAuthor(string authorName)
        {
            var products = await _productRepository.FindByAuthorContainingIgnoreCase(authorName);
            return products.Select(ToResponseDto).ToList();
        }

        public async Task<List<ProductResponseDto>> FindProductsByGenre(int genreId)
        {
            var products = await _productRepository.FindByGenreId(genreId);
            return products.Select(ToResponseDto).ToList();
        }

        public async Task<List<ProductResponseDto>> FindProductsByName(string name)
        {
            var products = await _productRepository.FindByNameContainingIgnoreCase(name);
            return products.Select(ToResponseDto).ToList();
        }

        public async Task<List<ProductResponseDto>> FindProductsByLanguage(int languageId)
        {
            var products = await _productRepository.FindByLanguageId(languageId);
            return products.Select(ToResponseDto).ToList();
        }

        // --- MAPPING METHODS ---
        private async Task MapDtoToEntity(ProductRequestDto dto, Product product)
        {
            // Map scalar properties
            product.Name = dto.Name;
            product.EnglishName = dto.EnglishName;
            product.Author = dto.Author;
            product.Isbn = dto.Isbn;
            product.LongDescription = dto.LongDescription;
            product.ShortDescription = dto.ShortDescription;
            product.ImageSource = dto.ImageSource;
            product.IsRentable = dto.IsRentable;
            product.MinRentDays = dto.MinRentDays;
            product.RentPerDay = dto.RentPerDay;
            product.BasePrice = dto.BasePrice;
            product.OfferPrice = dto.OfferPrice;
            product.SpecialCost = dto.SpecialCost;

            // Fetch and set related entities
            product.Genre = await _genreRepository.GetById(dto.GenreId)
                ?? throw new NotFoundException($"Genre not found with id: {dto.GenreId}");

            product.Language = await _languageRepository.GetById(dto.LanguageId)
                ?? throw new NotFoundException($"Language not found with id: {dto.LanguageId}");

            product.ProductType = await _productTypeRepository.GetById(dto.ProductTypeId)
                ?? throw new NotFoundException($"ProductType not found with id: {dto.ProductTypeId}");
        }

        private ProductResponseDto ToResponseDto(Product product)
        {
            // Simple mapper to convert a Product entity to a response DTO.
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                EnglishName = product.EnglishName,
                Author = product.Author,
                Isbn = product.Isbn,
                LongDescription = product.LongDescription,
                ShortDescription = product.ShortDescription,
                ImageSource = product.ImageSource,
                IsRentable = product.IsRentable ?? false,
                MinRentDays = product.MinRentDays,
                RentPerDay = product.RentPerDay,
                BasePrice = product.BasePrice,
                OfferPrice = product.OfferPrice ?? 0,
                SpecialCost = product.SpecialCost,
                GenreId = product.Genre?.Id ?? 0,
                GenreName = product.Genre?.Description,
                LanguageId = product.Language?.Id ?? 0,
                LanguageName = product.Language?.Description,
                ProductTypeId = product.ProductType?.Id ?? 0,
                ProductTypeName = product.ProductType?.Description
            };
        }
    }
}