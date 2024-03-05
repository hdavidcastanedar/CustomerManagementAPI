using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(c => new CustomerDto(
                c.Id,
                new NameDto(c.Name.Value),
                new DniDto(c.Dni.Value),
                new AddressDto(c.Address.Street, c.Address.City, c.Address.State),
                new ContactInfoDto(c.ContactInfo.Phone, c.ContactInfo.Mobile, c.ContactInfo.Email)));
        }

        public async Task<CustomerDto> GetByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Customer with ID {id} not found.");
            return new CustomerDto(
                customer.Id,
                new NameDto(customer.Name.Value),
                new DniDto(customer.Dni.Value),
                new AddressDto(customer.Address.Street, customer.Address.City, customer.Address.State),
                new ContactInfoDto(customer.ContactInfo.Phone, customer.ContactInfo.Mobile, customer.ContactInfo.Email));
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto customerDto)
        {
            try
            {
                var customer = Customer.Create(
                    Guid.NewGuid(),
                    new Name(customerDto.Name.Value),
                    new Dni(customerDto.Dni.Value),
                    new Address(customerDto.Address.Street, customerDto.Address.City, customerDto.Address.State),
                    new ContactInfo(customerDto.ContactInfo.Phone, customerDto.ContactInfo.Mobile, customerDto.ContactInfo.Email));
                await _customerRepository.AddAsync(customer);
                return customerDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task UpdateAsync(CustomerDto customerDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customerDto.Id) ?? throw new KeyNotFoundException($"Customer with ID {customerDto.Id} not found.");
            var customer = Customer.Create(
                                        existingCustomer.Id,
                                       new Name(customerDto.Name.Value),
                                       new Dni(customerDto.Dni.Value),
                                       new Address(customerDto.Address.Street, customerDto.Address.City, customerDto.Address.State),
                                       new ContactInfo(customerDto.ContactInfo.Phone, customerDto.ContactInfo.Mobile, customerDto.ContactInfo.Email));
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Customer with ID {id} not found.");
            await _customerRepository.DeleteAsync(customer.Id);
        }
    }
}
