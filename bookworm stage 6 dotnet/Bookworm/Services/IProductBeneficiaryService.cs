using System.Collections.Generic;

using Bookworm.RequestDTO;
using Bookworm.ResponseDTO;

namespace Bookworm.Service
{
    public interface IProductBeneficiaryService
    {
        void AssignBeneficiariesToProduct(AssignBeneficiariesRequestDTO requestDTO);
        List<ProductBeneficiaryResponseDTO> GetBeneficiariesForProduct(int productId);
    }
}
