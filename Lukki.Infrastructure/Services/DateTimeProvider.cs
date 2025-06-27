using Lukki.Application.Common.Interfaces.Services;

namespace Lukki.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}