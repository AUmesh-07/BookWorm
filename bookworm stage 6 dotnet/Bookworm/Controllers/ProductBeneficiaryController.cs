using Microsoft.AspNetCore.Mvc;
using Bookworm.RequestDTO;
using Bookworm.ResponseDTO;
using Bookworm.Service;
using System.Collections.Generic;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductBeneficiaryController : ControllerBase
    {
        private readonly IProductBeneficiaryService _productBeneficiaryService;

        public ProductBeneficiaryController(IProductBeneficiaryService productBeneficiaryService)
        {
            _productBeneficiaryService = productBeneficiaryService;
        }

        [HttpPost("assign-beneficiaries")]
        public IActionResult AssignBeneficiariesToProduct([FromBody] AssignBeneficiariesRequestDTO requestDTO)
        {
            _productBeneficiaryService.AssignBeneficiariesToProduct(requestDTO);
            return Ok("Beneficiaries assigned successfully.");
        }

        [HttpGet("{productId}/beneficiaries")]
        public ActionResult<List<ProductBeneficiaryResponseDTO>> GetBeneficiariesByProduct(int productId)
        {
            var response = _productBeneficiaryService.GetBeneficiariesForProduct(productId);
            return Ok(response);
        }
    }
}
