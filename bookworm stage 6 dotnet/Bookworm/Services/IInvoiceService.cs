using System.Collections.Generic;
using System.Threading.Tasks;
using Bookworm.DTO;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceDto>> GetUserOrdersAsync(int customerId);
}