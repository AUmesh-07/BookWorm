using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bookworm.Models;

namespace Bookworm.Repository
{
    public class ProductBeneficiaryRepository : IProductBeneficiaryRepository
    {
        private readonly BookwormDbContext _context;

        public ProductBeneficiaryRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductBeneficiary> FindByProduct(Product product)
        {
            return _context.ProductBeneficiaries
                .Include(pb => pb.Beneficiary)
                .Where(pb => pb.Product.Id == product.Id)
                .ToList();
        }

        public void DeleteByProduct(Product product)
        {
            var existing = _context.ProductBeneficiaries
                .Where(pb => pb.Product.Id == product.Id)
                .ToList();

            if (existing.Any())
            {
                _context.ProductBeneficiaries.RemoveRange(existing);
            }
        }

        public void Add(ProductBeneficiary entity)
        {
            _context.ProductBeneficiaries.Add(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
