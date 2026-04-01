namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes process types
/// </summary>
public static class ProcessType
{

    /// <summary>
    /// Gets the 'container' process type
    /// </summary>
    public const string Container = "container";
    /// <summary>
    /// Gets the 'script' process type
    /// </summary>
    public const string Script = "script";
    /// <summary>
    /// Gets the 'shell' process type
    /// </summary>
    public const string Shell = "shell";
    /// <summary>
    /// Gets the 'workflow' process type
    /// </summary>
    public const string Workflow = "workflow";
    /// <summary>
    /// Gets the 'extension' process type
    /// </summary>
    public const string Extension = "extension";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all supported process types
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported process types 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Container;
        yield return Script;
        yield return Shell;
        yield return Workflow;
    }

}