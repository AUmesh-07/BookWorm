using Bookworm.Models;
using Bookworm.Repository;
using Microsoft.EntityFrameworkCore;

public interface IInvoiceRepository
{
    Task<Invoice> SaveAsync(Invoice invoice);
    Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(int customerId);
    Task<IEnumerable<InvoiceDetail>> GetInvoiceDetailsByInvoiceIdAsync(long invoiceId);
}

public class InvoiceRepository : IInvoiceRepository
{
    private readonly BookwormDbContext _context;
    public InvoiceRepository(BookwormDbContext context) => _context = context;

    public async Task<Invoice> SaveAsync(Invoice invoice)
    {
        if (invoice.InvoiceId == 0)
            _context.Invoices.Add(invoice);
        else
            _context.Invoices.Update(invoice);

        await _context.SaveChangesAsync();
        return invoice;
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(int customerId)
    {
        return await _context.Invoices
                             .Where(im => im.CustomerId == customerId)
                             .ToListAsync();
    }

    public async Task<IEnumerable<InvoiceDetail>> GetInvoiceDetailsByInvoiceIdAsync(long invoiceId)
    {
        return await _context.InvoiceDetails
                             .Where(id => id.InvoiceId == invoiceId)
                             .ToListAsync();
    }
}
