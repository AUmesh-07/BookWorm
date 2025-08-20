using System.Threading.Tasks;
using Bookworm.Models;
using Bookworm.Repository;
using Microsoft.EntityFrameworkCore;


namespace Bookworm.Repository
{
    public interface ICartRepository
    {
        Task<Cart?> FindByCustomerIdAndIsActiveTrue(int customerId);
        Task<Cart> Save(Cart cart);
        Task<Cart?> GetById(int id);
    }
    public class CartRepository : ICartRepository
    {
        private readonly BookwormDbContext _context;

        public CartRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> FindByCustomerIdAndIsActiveTrue(int customerId)
        {
            return await _context.Carts
                                 .Include(c => c.CartDetails)
                                 .ThenInclude(cd => cd.Product)
                                 .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.IsActive);
        }

        public async Task<Cart> Save(Cart cart)
        {
            if (cart.Id == 0)
            {
                // New cart, add it to the context
                _context.Carts.Add(cart);
            }
            else
            {
                // Existing cart, update it
                _context.Carts.Update(cart);
            }

            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart?> GetById(int id)
        {
            return await _context.Carts.FindAsync(id);
        }
    }
}

