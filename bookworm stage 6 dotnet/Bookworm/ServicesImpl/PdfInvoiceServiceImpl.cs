using Bookworm.Documents;
using Bookworm.DTO;
using Bookworm.Models;
using Bookworm.Services;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Linq;

namespace Bookworm.ServicesImpl
{
    public class PdfInvoiceServiceImpl : IPdfInvoiceService
    {
        public PdfInvoiceServiceImpl()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // 1. Implement the new method signature from the interface.
        public byte[] CreateInvoicePdf(Invoice invoice)
        {
            // 2. Map the real Invoice entity to the DTO that the PDF template needs.
            var invoiceDto = MapEntityToDto(invoice);

            // 3. Pass the real data to the PDF document template.
            var document = new InvoiceDocument(invoiceDto);
            byte[] pdfBytes = document.GeneratePdf();

            return pdfBytes;
        }

        // 4. Add a private helper method to convert your database model to the PDF model.
        private InvoiceDto MapEntityToDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                InvoiceId = (int)invoice.InvoiceId,
                Amount = invoice.Amount,
                Date = invoice.Date,
                CustomerId = invoice.CustomerId,
                CustomerName = $"{invoice.Customer.Name}",
                CustomerEmail = invoice.Customer.Email,
                InvoiceDetails = invoice.InvoiceDetails.Select(detail => new InvoiceDetailDto
                {
                    InvDtlId = detail.InvDtlId,
                    Quantity = detail.Quantity,
                    RentNoOfDays = detail.RentNoOfDays,
                    RoyaltyAmount = detail.RoyaltyAmount,
                    SellPrice = detail.SellPrice,
                    TranType = detail.TranType,
                    ProductId = detail.Product.Id,
                    ProductName = detail.Product.Name,
                    ProductAuthor = detail.Product.Author
                }).ToList()
            };
        }

        // 5. The CreateDummyInvoice() method is no longer needed and can be deleted.
    }
}
