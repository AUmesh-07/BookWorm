using Bookworm.Models;
using Bookworm.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> SaveCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<Customer> FindByEmailAsync(string email);
        Task<Customer> FindByPhoneAsync(string phone);
        Task<bool> ExistsByIdAsync(int id);
        Task<Customer?> GetById(int id);
        Task<Customer> GetByEmail(string email);


    }
}

public class CustomerRepository : ICustomerRepository
{
    private readonly BookwormDbContext _context;

    public CustomerRepository(BookwormDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> GetByEmail(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetById(int id)
    {
        return await _context.Customers.FindAsync(id);
    }
    public async Task<Customer> SaveCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Customer> FindByEmailAsync(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer> FindByPhoneAsync(string phone)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Phone == phone);
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _context.Customers.AnyAsync(e => e.Id == id);
    }
}
