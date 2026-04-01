namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes constants representing different transition options for a workflow
/// </summary>
public static class FlowDirective
{

    /// <summary>
    /// Indicates that the workflow should continue its execution, possibly exiting the current branch and/or completing execution if transitionning from the last task
    /// </summary>
    public const string Continue = "continue";
    /// <summary>
    /// Indicates that the workflow should end its execution, possibly ignoring other defined tasks in the flow
    /// </summary>
    public const string End = "end";
    /// <summary>
    /// Indicates that the workflow should exit the current branch, iteration or loop, possibly completing execution if transitionning from the main branch
    /// </summary>
    public const string Exit = "exit";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all supported flow directives
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported flow directives
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all supported flow directives</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Continue;
        yield return End;
        yield return Exit;
    }

}
