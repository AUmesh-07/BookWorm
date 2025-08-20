using System;
using System.Threading.Tasks;
using Bookworm.DTO;
using Bookworm.Models;
using Bookworm.Repository;
using Bookworm.RequestDTO;

namespace Bookworm.Services
{
    public interface IBeneficiaryMasterService
    {
        Task<BeneficiaryMaster> CreateBeneficiaryAsync(BeneficiaryRequestDTO requestDTO);
    }

    public class BeneficiaryMasterService : IBeneficiaryMasterService
    {
        private readonly IBeneficiaryMasterRepository _beneficiaryMasterRepository;

        public BeneficiaryMasterService(IBeneficiaryMasterRepository beneficiaryMasterRepository)
        {
            _beneficiaryMasterRepository = beneficiaryMasterRepository;
        }

        public async Task<BeneficiaryMaster> CreateBeneficiaryAsync(BeneficiaryRequestDTO requestDTO)
        {
            if (requestDTO == null)
                throw new ArgumentNullException(nameof(requestDTO));

            var beneficiary = new BeneficiaryMaster
            {
                BenName = requestDTO.BenName,
                BenEmail = requestDTO.BenEmail,
                BenPan = requestDTO.BenPan
                // map other properties if needed
            };

            await _beneficiaryMasterRepository.AddAsync(beneficiary);
            await _beneficiaryMasterRepository.SaveChangesAsync();

            return beneficiary;
        }
    }
}
