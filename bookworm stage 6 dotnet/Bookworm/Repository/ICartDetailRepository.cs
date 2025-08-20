using System.Threading.Tasks;
using Bookworm.Models;
using Bookworm.Repository;
using Microsoft.EntityFrameworkCore;



namespace Bookworm.Repositories
{
    public interface ICartDetailRepository
    {
        Task<CartDetail?> FindByCartIdAndProductId(int cartId, int productId);
        Task<CartDetail> Save(CartDetail cartDetail);
        void Delete(CartDetail cartDetail);
    }
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly BookwormDbContext _context;

        public CartDetailRepository(BookwormDbContext context)
        {
            _context = context;
        }

        public async Task<CartDetail?> FindByCartIdAndProductId(int cartId, int productId)
        {
            return await _context.CartDetails
                                 .FirstOrDefaultAsync(cd => cd.CartId == cartId && cd.ProductId == productId);
        }

        public async Task<CartDetail> Save(CartDetail cartDetail)
        {
            if (cartDetail.Id == 0)
            {
                // New cart detail, add it to the context
                _context.CartDetails.Add(cartDetail);
            }
            else
            {
                // Existing cart detail, update it
                _context.CartDetails.Update(cartDetail);
            }

            await _context.SaveChangesAsync();
            return cartDetail;
        }

        public void Delete(CartDetail cartDetail)
        {
            _context.CartDetails.Remove(cartDetail);
            _context.SaveChanges();
        }
    }
}