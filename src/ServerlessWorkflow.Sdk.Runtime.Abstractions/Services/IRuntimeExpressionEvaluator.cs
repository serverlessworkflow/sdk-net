namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to evaluate runtime expressions
/// </summary>
public interface IRuntimeExpressionEvaluator
{

    /// <summary>
    /// Determines whether the specified language is supported by the expression evaluator
    /// </summary>
    /// <param name="language">The expression language to check</param>
    /// <returns>A boolean indicating whether the specified language is supported by the expression evaluator</returns>
    bool Supports(string language);

    /// <summary>
    /// Evaluates the specified expression with the given input and arguments, if any
    /// </summary>
    /// <param name="expression">The expression to evaluate</param>
    /// <param name="input">The input to evaluate the expression with</param>
    /// <param name="arguments">The arguments, if an, to evaluate the expression with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the expression evaluation</returns>
    Task<JsonNode?> EvaluateAsync(string expression, JsonObject input, JsonObject? arguments = null, CancellationToken cancellationToken = default);

}
