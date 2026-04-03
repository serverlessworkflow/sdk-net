namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to provide runtime expression evaluators
/// </summary>
public interface IRuntimeExpressionEvaluatorProvider
{

    /// <summary>
    /// Gets an <see cref="IRuntimeExpressionEvaluator"/> that supports the specified language, if any
    /// </summary>
    /// <param name="language">The expression language to get an <see cref="IRuntimeExpressionEvaluator"/> for</param>
    /// <returns>The first registered <see cref="IRuntimeExpressionEvaluator"/>, if any, that supports the specified expression language</returns>
    IRuntimeExpressionEvaluator? GetEvaluator(string language);

}