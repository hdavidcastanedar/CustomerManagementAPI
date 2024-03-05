using Domain.Entities;

namespace Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}