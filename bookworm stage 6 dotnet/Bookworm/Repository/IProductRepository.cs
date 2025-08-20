using Bookworm.Models;
using Bookworm.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookworm.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetById(int id);
        Task<List<Product>> GetAll();
        Task<Product> Save(Product product);
        Task DeleteById(int id);
        Task<bool> ExistsById(int id);

        // Custom query methods
        Task<List<Product>> FindByGenreId(int genreId);
        Task<List<Product>> FindByNameContainingIgnoreCase(string name);
        Task<List<Product>> FindByAuthorContainingIgnoreCase(string author);
        Task<List<Product>> FindByIsRentable(bool isRentable);
        Task<List<Product>> FindByLanguageId(int languageId);
        Task<Product> GetProductByIdAsync(int productId);

    }
    public class ProductRepository : IProductRepository
    {
        private readonly BookwormDbContext _context;

        public ProductRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
                                 .Include(p => p.Genre)
                                 .Include(p => p.Language)
                                 .Include(p => p.ProductType)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                                 .Include(p => p.Genre)
                                 .Include(p => p.Language)
                                 .Include(p => p.ProductType)
                                 .ToListAsync();
        }

        public async Task<Product> Save(Product product)
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                _context.Products.Update(product);
            }
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteById(int id)
        {
            var product = await GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Product>> FindByGenreId(int genreId)
        {
            return await _context.Products
                                 .Where(p => p.Genre.Id == genreId)
                                 .Include(p => p.Genre)
                                 .Include(p => p.Language)
                                 .Include(p => p.ProductType)
                                 .ToListAsync();
        }

        public async Task<List<Product>> FindByNameContainingIgnoreCase(string name)
        {
            return await _context.Products
                                 .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
                                 .Include(p => p.Genre)
                                 .Include(p => p.Language)
                                 .Include(p => p.ProductType)
                                 .ToListAsync();
        }

        public async Task<List<Product>> FindByAuthorContainingIgnoreCase(string author)
        {
            return await _context.Products
                                 .Where(p => EF.Functions.Like(p.Author, $"%{author}%"))
                                 .Include(p => p.Genre)
                                 .Include(p => p.Language)
                                 .Include(p => p.ProductType)
                                 .ToListAsync();
        }

        public async Task<List<Product>> FindByIsRentable(bool isRentable)
        {
            return await _context.Products
                                 .Where(p => p.IsRentable == isRentable)
                                 .Include(p => p.Genre)
                                 .Include(p => p.Language)
                                 .Include(p => p.ProductType)
                                 .ToListAsync();
        }

        public async Task<List<Product>> FindByLanguageId(int languageId)
        {
            return await _context.Products
                                 .Where(p => p.Language.Id == languageId)
                                 .Include(p => p.Genre)
                                 .Include(p => p.Language)
                                 .Include(p => p.ProductType)
                                 .ToListAsync();
        }
    }
  }