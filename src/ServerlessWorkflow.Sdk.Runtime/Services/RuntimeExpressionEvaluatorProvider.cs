namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an <see cref="IRuntimeExpressionEvaluatorProvider"/> used to provide instances of <see cref="JQRuntimeExpressionEvaluator"/>
/// </summary>
/// <param name="evaluators">An <see cref="IEnumerable{T}"/> containing all registered <see cref="IRuntimeExpressionEvaluator"/>s</param>
public sealed class RuntimeExpressionEvaluatorProvider(IEnumerable<IRuntimeExpressionEvaluator> evaluators)
    : IRuntimeExpressionEvaluatorProvider
{

    /// <inheritdoc/>
    public IRuntimeExpressionEvaluator? GetEvaluator(string language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        language = language.Trim();
        return evaluators.FirstOrDefault(e => e.Supports(language));
    }

}