using Bookworm.Models;
using Bookworm.Repositories;
using Bookworm.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bookworm.Services.Impl
{
    public class RoyaltyCalculationServiceImpl : IRoyaltyCalculationService
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IProductBeneficiaryRepository _productBeneficiaryRepository;
        private readonly IRoyaltyCalculationRepository _royaltyCalculationRepository;

        public RoyaltyCalculationServiceImpl(
            IInvoiceDetailRepository invoiceDetailRepository,
            IProductBeneficiaryRepository productBeneficiaryRepository,
            IRoyaltyCalculationRepository royaltyCalculationRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
            _productBeneficiaryRepository = productBeneficiaryRepository;
            _royaltyCalculationRepository = royaltyCalculationRepository;
        }

        public async Task CalculateRoyaltyForInvoiceAsync(Invoice invoice)
        {
            // Step 1: Fetch all invoice items for this invoice
            var invoiceItems = await _invoiceDetailRepository.FindByInvoiceAsync((int)invoice.InvoiceId);


            if (invoiceItems == null || !invoiceItems.Any())
            {
                Console.WriteLine($"🚫 No invoice details found for invoice ID: {invoice.InvoiceId}");
                return;
            }

            foreach (var item in invoiceItems)
            {
                var product = item.Product;
                var salesPrice = item.SellPrice;
                var transactionType = item.TranType;

                if (product == null)
                {
                    Console.WriteLine("⚠️ Skipping item with null product in invoice detail.");
                    continue;
                }

                // Step 2: Fetch beneficiaries for this product
                // Change this line:
                var beneficiaries = _productBeneficiaryRepository.FindByProduct(product);


                if (beneficiaries == null || !beneficiaries.Any())
                {
                    Console.WriteLine($"🔍 No beneficiaries found for product ID: {product.Id}");
                    continue;
                }

                foreach (var pb in beneficiaries)
                {
                    var percentage = pb.Percentage;

                    if (percentage <= 0)
                    {
                        Console.WriteLine($"❌ Skipping invalid percentage for beneficiary ID: {pb.Beneficiary.BenId}");
                        continue;
                    }

                    var royaltyAmount = salesPrice * (percentage / 100m);

                    // Step 3: Save the royalty record
                    var royalty = new RoyaltyCalculation
                    {
                        Beneficiary = pb.Beneficiary,
                        Product = product,
                        Invoice = invoice,
                        RoyaltyDate = DateTime.UtcNow.Date,
                        TransactionType = transactionType,
                        SalesPrice = salesPrice,
                        RoyaltyOnSalesPrice = royaltyAmount
                    };

                    await _royaltyCalculationRepository.SaveAsync(royalty);

                    Console.WriteLine($"✅ Royalty saved: {pb.Beneficiary.BenName} -> ₹{royaltyAmount}");
                }
            }

            Console.WriteLine($"🎉 Royalty calculation completed for invoice ID: {invoice.InvoiceId}");
        }
    }
}
