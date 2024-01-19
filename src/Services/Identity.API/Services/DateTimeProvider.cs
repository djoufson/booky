using Identity.API.Services.Interfaces;

namespace Identity.API.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
