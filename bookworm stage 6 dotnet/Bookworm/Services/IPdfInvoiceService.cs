using Bookworm.DTO;
using Bookworm.Models; // Make sure to include this using statement

namespace Bookworm.Services
{
    public interface IPdfInvoiceService
    {
        byte[] CreateInvoicePdf(Invoice invoice);
    }
}
