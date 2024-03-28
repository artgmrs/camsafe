using CamSafe.Business.Interfaces;
using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;
using CamSafe.Repository.Interfaces;

namespace CamSafe.Business.Businesses
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerBusiness(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomerAsync(CustomerRequest customerRequest)
        {
            if (string.IsNullOrEmpty(customerRequest.Name))
            {
                throw new CustomException("Name can't be empty.");
            }

            var result = await _customerRepository.InsertCustomerAsync(customerRequest);

            var customer = new Customer()
            {
                Id = result,
                Name = customerRequest.Name
            };

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerById(string customerId)
        {
            return await _customerRepository.GetCustomerByIdAsync(customerId);
        }
    }
}
