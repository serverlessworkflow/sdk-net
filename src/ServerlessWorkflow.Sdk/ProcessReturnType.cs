namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all supported process return types
/// </summary>
public static class ProcessReturnType
{

    /// <summary>
    /// Indicates that the process must return only the content of its Standard Output (STDOUT) stream
    /// </summary>
    public const string Stdout = "stdout";
    /// <summary>
    /// Indicates that the process must return only the content of its Standard Error (STDERR) stream
    /// </summary>
    public const string Stderr = "stderr";
    /// <summary>
    /// Indicates that the process must return only its exit code
    /// </summary>
    public const string Code = "code";
    /// <summary>
    /// Indicates that the process must return an object that wraps the content of its STDOUT stream, the content of its STDERR stream and its exit code
    /// </summary>
    public const string All = "all";
    /// <summary>
    /// Indicates that the process must not return anything
    /// </summary>
    public const string None = "none";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Stdout;
        yield return Stderr;
        yield return Code;
        yield return All;
        yield return None;
    }

}
