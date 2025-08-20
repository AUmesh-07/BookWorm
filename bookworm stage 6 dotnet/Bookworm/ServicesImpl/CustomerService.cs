using Bookworm.Models;
using Bookworm.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookworm.Services;
namespace Bookworm.ServicesImpl
{

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> SaveCustomerAsync(Customer customer)
        {
            // Add business logic and validation here, just like in the Java code.
            // e.g., hashing the password, checking for duplicate email/phone.
            return await _customerRepository.SaveCustomerAsync(customer);
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer not found with id: {id}");
            }
            return customer;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customerDetails)
        {
            var existingCustomer = await GetCustomerByIdAsync(id);

            existingCustomer.Name = customerDetails.Name;
            existingCustomer.Email = customerDetails.Email;
            existingCustomer.Phone = customerDetails.Phone;
            existingCustomer.DateOfBirth = customerDetails.DateOfBirth;
            existingCustomer.Age = customerDetails.Age;
            existingCustomer.Address = customerDetails.Address;

            // Handle password update separately and securely in a real app.
            if (!string.IsNullOrEmpty(customerDetails.PasswordHash))
            {
                existingCustomer.PasswordHash = customerDetails.PasswordHash;
            }

            return await _customerRepository.UpdateCustomerAsync(existingCustomer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            if (!await _customerRepository.ExistsByIdAsync(id))
            {
                throw new KeyNotFoundException($"Customer not found with id: {id}");
            }
            await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
