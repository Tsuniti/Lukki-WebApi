using Lukki.Domain.CustomerAggregate;
using Lukki.Domain.CustomerAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetByEmailAsync(string email);
    Task AddAsync(Customer customer);
    Task<Customer?> GetByIdAsync(CustomerId id);
}