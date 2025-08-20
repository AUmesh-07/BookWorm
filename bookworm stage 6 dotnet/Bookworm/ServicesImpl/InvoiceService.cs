using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookworm.DTO;
using Bookworm.Repository;
using Bookworm.Models;
using Bookworm.Repositories;
using Bookworm.Repository;
using Bookworm.DTO;
using Bookworm.RequestDTO;
using Bookworm.ResponseDTO;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IProductRepository _productRepository; // Assuming a Product repository exists

    public InvoiceService(IInvoiceRepository invoiceRepository, IProductRepository productRepository)
    {
        _invoiceRepository = invoiceRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<InvoiceDto>> GetUserOrdersAsync(int customerId)
    {
        var invoices = await _invoiceRepository.GetInvoicesByCustomerIdAsync(customerId);
        var invoiceDtos = new List<InvoiceDto>();

        foreach (var invoice in invoices)
        {
            var invoiceDetails = await _invoiceRepository.GetInvoiceDetailsByInvoiceIdAsync(invoice.InvoiceId);
            var detailDtos = new List<InvoiceDetailDto>();

            foreach (var detail in invoiceDetails)
            {
                var product = await _productRepository.GetProductByIdAsync(detail.ProductId); // Fetch product info
                detailDtos.Add(new InvoiceDetailDto
                {
                    ProductName = product.Name,
                    Quantity = detail.Quantity,
                    RentNoOfDays = detail.RentNoOfDays,
                    SellPrice = detail.SellPrice,
                    TranType = detail.TranType
                });
            }

            invoiceDtos.Add(new InvoiceDto
            {
                InvoiceId = invoice.InvoiceId,
                Amount = invoice.Amount,
                Date = invoice.Date,
                Items = detailDtos
            });
        }

        return invoiceDtos;
    }
}