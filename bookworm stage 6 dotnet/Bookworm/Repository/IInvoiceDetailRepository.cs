using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookworm.Models; // Replace with your actual models namespace

namespace Bookworm.Repository
{
    public interface IInvoiceDetailRepository
    {
        
        Task<InvoiceDetail?> FindByIdAsync(int invoiceDetailId);

        Task<List<InvoiceDetail>> FindByInvoiceAsync(int invoiceId);


    }

    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly BookwormDbContext _context;

        public InvoiceDetailRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDetail>> FindByInvoiceAsync(int invoiceId)
        {
            return await _context.InvoiceDetails
                                 .Where(detail => detail.InvoiceId == invoiceId)
                                 .ToListAsync();
        }
        public async Task<InvoiceDetail?> FindByIdAsync(int id)
        {
            return await _context.InvoiceDetails
                .FirstOrDefaultAsync(d => d.InvDtlId == id);
        }

    }
}
