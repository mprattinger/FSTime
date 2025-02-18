using FSTime.Domain.Common.Interfaces;

namespace FSTime.Infrastructure.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
