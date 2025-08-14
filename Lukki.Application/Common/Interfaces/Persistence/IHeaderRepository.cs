using Lukki.Domain.HeaderAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;


public interface IHeaderRepository
{
    Task AddAsync(MyHeader header);
    Task<MyHeader?> GetByNameAsync(string name);
    Task<List<string>> GetAllNamesAsync();
}