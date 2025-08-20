using Bookworm.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookworm.Services
{
    public interface IProductTypeService
    {
        Task<ProductType> SaveProductType(ProductType productType);
        Task<ProductType> GetProductTypeById(int id);
        Task<List<ProductType>> GetAllProductTypes();
        Task DeleteProductType(int id);
    }
}