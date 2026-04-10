namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Exposes all default statuses of a correlation context 
/// </summary>
public static class CorrelationContextStatus
{

    /// <summary>
    /// Indicates that the context is currently active and in use.
    /// </summary>
    public const string Active = "active";
    /// <summary>
    /// Indicates that the context is inactive or paused
    /// </summary>
    public const string Inactive = "inactive";
    /// <summary>
    /// Indicates that the correlation process has been successfully completed.
    /// </summary>
    public const string Completed = "completed";
    /// <summary>
    /// Indicates that the correlation process has been cancelled.
    /// </summary>
    public const string Cancelled = "cancelled";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> used to enumerate the default correlation context statuses
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> used to enumerate the default correlation context statuses</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Active;
        yield return Inactive;
        yield return Completed;
        yield return Cancelled;
    }

}