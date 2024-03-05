using Application.DTOs;

namespace Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetByIdAsync(Guid id);
    Task<CustomerDto> CreateAsync(CustomerDto customerDto);
    Task UpdateAsync(CustomerDto customerDto);
    Task DeleteAsync(Guid id);
}

