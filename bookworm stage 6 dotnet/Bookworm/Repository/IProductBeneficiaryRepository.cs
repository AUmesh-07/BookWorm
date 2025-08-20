using System.Collections.Generic;
using Bookworm.Models;

namespace Bookworm.Repository
{
    public interface IProductBeneficiaryRepository
    {
        IEnumerable<ProductBeneficiary> FindByProduct(Product product);
        void DeleteByProduct(Product product);
        void Add(ProductBeneficiary entity);
        void SaveChanges();
    }
}
