#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="DateTimeOffset"/>s
/// </summary>
public static class DateTimeOffsetExtensions
{

    /// <summary>
    /// Gets an object describing the specified <see cref="DateTimeOffset"/>
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTimeOffset"/> to get the descriptor of</param>
    /// <returns>A new <see cref="DateTimeDescriptor"/> describing the specified <see cref="DateTimeOffset"/></returns>
    public static DateTimeDescriptor GetDescriptor(this DateTimeOffset dateTime) => new()
    {
        Iso8601 = dateTime.ToString("o"),
        Epoch = new()
        {
            Milliseconds = (ulong)dateTime.ToUnixTimeSeconds() * 1000
        }
    };

}