namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all supported container cleanup policies
/// </summary>
public static class ContainerCleanupPolicy
{

    /// <summary>
    /// Indicates that the runtime must delete the container immediately after execution
    /// </summary>
    public const string Always = "always";
    /// <summary>
    /// Indicates that the runtime must eventually delete the container, after waiting for a specific amount of time.
    /// </summary>
    public const string Eventually = "eventually";
    /// <summary>
    /// Indicates that the runtime must never delete the container.
    /// </summary>
    public const string Never = "never";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all supported values
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Always;
        yield return Eventually;
        yield return Never;
    }

}