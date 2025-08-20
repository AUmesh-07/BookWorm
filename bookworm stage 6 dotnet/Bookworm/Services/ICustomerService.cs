using Bookworm.Models;

namespace Bookworm.Services
{
   
        public interface ICustomerService
        {

            Task<Customer> SaveCustomerAsync(Customer customer);
            Task<Customer> GetCustomerByIdAsync(int id);
            Task<List<Customer>> GetAllCustomersAsync();
            Task<Customer> UpdateCustomerAsync(int id, Customer customerDetails);
            Task DeleteCustomerAsync(int id);
        }
    
}
