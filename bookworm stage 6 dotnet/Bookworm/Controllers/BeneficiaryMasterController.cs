using Microsoft.AspNetCore.Mvc;
using Bookworm.DTO;          // Assuming your DTOs are here
using Bookworm.Models;       // Assuming your entities are here
using Bookworm.Services;     // Assuming your service interface is here
using System.Threading.Tasks;
using Bookworm.RequestDTO;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeneficiaryMasterController : ControllerBase
    {
        private readonly IBeneficiaryMasterService _beneficiaryMasterService;

        public BeneficiaryMasterController(IBeneficiaryMasterService beneficiaryMasterService)
        {
            _beneficiaryMasterService = beneficiaryMasterService;
        }

        [HttpPost]
        public async Task<ActionResult<BeneficiaryMaster>> CreateBeneficiary([FromBody] BeneficiaryRequestDTO requestDTO)
        {
            var newBeneficiary = await _beneficiaryMasterService.CreateBeneficiaryAsync(requestDTO);
            return Ok(newBeneficiary);
        }
    }
}
