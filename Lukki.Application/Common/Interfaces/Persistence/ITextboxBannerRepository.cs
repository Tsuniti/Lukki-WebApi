using Lukki.Domain.TextboxBannerAggregate;
using Lukki.Domain.TextboxBannerAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface ITextboxBannerRepository
{
    Task AddAsync(TextboxBanner textboxBanner);
    Task<TextboxBanner?> GetByNameAsync(string name);
    
    Task<TextboxBanner?> GetByIdAsync(TextboxBannerId id);

    
}