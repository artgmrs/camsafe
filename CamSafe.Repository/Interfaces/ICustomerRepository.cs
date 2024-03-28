using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;

namespace CamSafe.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetAllCustomersAsync();
        public Task<Customer?> GetCustomerByIdAsync(string customerId);
        public Task<int> InsertCustomerAsync(CustomerRequest customer);
    }
}
