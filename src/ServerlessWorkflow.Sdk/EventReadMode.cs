namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all supported event read modes
/// </summary>
public static class EventReadMode
{

    /// <summary>
    /// Indicates that only the data of consumed events should be read
    /// </summary>
    public const string Data = "data";
    /// <summary>
    /// Indicates that the whole event envelope should be read, including context attributes
    /// </summary>
    public const string Envelope = "envelope";
    /// <summary>
    /// Indicates that the event's raw data should be read, without additional transformation (i.e. deserialization)
    /// </summary>
    public const string Raw = "raw";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all supported event read modes
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported event read modes
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported event read modes</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Data;
        yield return Envelope;
        yield return Raw;
    }

}