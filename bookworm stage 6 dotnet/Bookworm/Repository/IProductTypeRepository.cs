using Bookworm.Models;
using Bookworm.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Repositories
{
    public interface IProductTypeRepository
    {
        Task<ProductType> Save(ProductType productType);
        Task<ProductType?> GetById(int id);
        Task<List<ProductType>> GetAll();
        Task DeleteById(int id);
        Task<bool> ExistsById(int id);
    }
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly BookwormDbContext _context;

        public ProductTypeRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<ProductType> Save(ProductType productType)
        {
            if (productType.Id == 0)
            {
                _context.ProductTypes.Add(productType);
            }
            else
            {
                _context.ProductTypes.Update(productType);
            }
            await _context.SaveChangesAsync();
            return productType;
        }

        public async Task<ProductType?> GetById(int id)
        {
            return await _context.ProductTypes.FindAsync(id);
        }

        public async Task<List<ProductType>> GetAll()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task DeleteById(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.ProductTypes.AnyAsync(pt => pt.Id == id);
        }
    }
}