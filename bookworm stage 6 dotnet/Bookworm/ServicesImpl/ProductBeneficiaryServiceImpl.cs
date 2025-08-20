using System;
using System.Collections.Generic;
using System.Linq;
using Bookworm.Models;
using Bookworm.Repositories;
using Bookworm.Repository;
using Bookworm.DTO;
using Bookworm.RequestDTO;
using Bookworm.ResponseDTO;

namespace Bookworm.Service
{
    public class ProductBeneficiaryService : IProductBeneficiaryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBeneficiaryMasterRepository _beneficiaryMasterRepository;
        private readonly IProductBeneficiaryRepository _productBeneficiaryRepository;

        public ProductBeneficiaryService(
            IProductRepository productRepository,
            IBeneficiaryMasterRepository beneficiaryMasterRepository,
            IProductBeneficiaryRepository productBeneficiaryRepository)
        {
            _productRepository = productRepository;
            _beneficiaryMasterRepository = beneficiaryMasterRepository;
            _productBeneficiaryRepository = productBeneficiaryRepository;
        }

        public void AssignBeneficiariesToProduct(AssignBeneficiariesRequestDTO requestDTO)
        {
            // Block on async method here to get actual Product
            var product = _productRepository.GetById(requestDTO.ProductId).GetAwaiter().GetResult()
                          ?? throw new Exception("Product not found");

            _productBeneficiaryRepository.DeleteByProduct(product);

            foreach (var br in requestDTO.Beneficiaries)
            {
                // Block on async method here to get actual Beneficiary
                var beneficiary = _beneficiaryMasterRepository.GetById(br.BeneficiaryId).GetAwaiter().GetResult()
                                  ?? throw new Exception($"Beneficiary not found: {br.BeneficiaryId}");

                var productBeneficiary = new ProductBeneficiary
                {
                    Product = product,
                    Beneficiary = beneficiary,
                    Percentage = br.Percentage
                };

                _productBeneficiaryRepository.Add(productBeneficiary);
            }

            _productBeneficiaryRepository.SaveChanges();
        }

        public List<ProductBeneficiaryResponseDTO> GetBeneficiariesForProduct(int productId)
        {
            // Block on async method here as well
            var product = _productRepository.GetById(productId).GetAwaiter().GetResult()
                          ?? throw new Exception("Product not found");

            var beneficiaries = _productBeneficiaryRepository.FindByProduct(product);

            return beneficiaries.Select(pb => new ProductBeneficiaryResponseDTO
            {
                BeneficiaryId = pb.Beneficiary.BenId,
                BeneficiaryName = pb.Beneficiary.BenName,
                Percentage = pb.Percentage
            }).ToList();
        }
    }
}
