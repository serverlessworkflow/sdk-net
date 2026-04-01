namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes the default runtime expression evaluation modes
/// </summary>
public static class RuntimeExpressionEvaluationMode
{

    /// <summary>
    /// Gets the 'strict' runtime expression evaluation mode, which requires all expressions to be enclosed within ${ } for proper identification and evaluation.
    /// </summary>
    public const string Strict = "strict";
    /// <summary>
    /// Gets the 'loose' runtime expression evaluation mode, which evaluates any value provided. If the evaluation fails, it results in a string with the expression as its content.
    /// </summary>
    public const string Loose = "loose";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> that contains all supported runtime expression evaluation modes
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> that contains all supported runtime expression evaluation modes
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> that contains all supported runtime expression evaluation modes</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Strict;
        yield return Loose;
    }

}