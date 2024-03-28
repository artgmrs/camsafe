using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;

namespace CamSafe.Business.Interfaces
{
    public interface ICustomerBusiness
    {
        public Task<IEnumerable<Customer>> GetAllCustomersAsync();
        public Task<Customer> GetCustomerById(string customerId);
        public Task<Customer> CreateCustomerAsync(CustomerRequest customer);
    }
}
