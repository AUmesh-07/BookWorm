using Bookworm.Exceptions;
using Bookworm.Models;
using Bookworm.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookworm.Services.Impl
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeService(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public async Task<ProductType> SaveProductType(ProductType productType)
        {
            return await _productTypeRepository.Save(productType);
        }

        public async Task<ProductType> GetProductTypeById(int id)
        {
            return await _productTypeRepository.GetById(id)
                ?? throw new NotFoundException($"ProductType not found with id: {id}");
        }

        public async Task<List<ProductType>> GetAllProductTypes()
        {
            return await _productTypeRepository.GetAll();
        }

        public async Task DeleteProductType(int id)
        {
            if (!await _productTypeRepository.ExistsById(id))
            {
                throw new NotFoundException($"ProductType not found with id: {id}");
            }
            await _productTypeRepository.DeleteById(id);
        }
    }
}