namespace Identity.API.Services.Interfaces;


/// <summary>
/// Represents a service for providing the current date and time.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current date and time in Coordinated Universal Time (UTC).
    /// </summary>
    DateTime UtcNow { get; }
}