using Lukki.Domain.FooterAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IFooterRepository
{
    Task AddAsync(Footer footer);
    Task<Footer?> GetByNameAsync(string name);
}