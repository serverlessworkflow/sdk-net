#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IRuntimeExpressionEvaluator"/>s.
/// </summary>
public static class IRuntimeExpressionEvaluatorExtensions
{

    /// <summary>
    /// Evaluates the specified expression with the given input and arguments, if any, and returns the result as a boolean value.
    /// </summary>
    /// <param name="expressionEvaluator">The <see cref="IRuntimeExpressionEvaluator"/> to use to evaluate the expression</param>
    /// <param name="expression">The expression to evaluate</param>
    /// <param name="input">The input to evaluate the expression with</param>
    /// <param name="arguments">The arguments, if any, to evaluate the expression with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A boolean indicating whether the condition specified by the expression is satisfied or not</returns>
    public static async Task<bool> EvaluateConditionAsync(this IRuntimeExpressionEvaluator expressionEvaluator, string expression, JsonObject input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        var node = await expressionEvaluator.EvaluateAsync(expression, input, arguments, cancellationToken);
        if (node is null) return false;
        return node.GetValue<bool>();
    }

    /// <summary>
    /// Evaluates the specified value, which can be either a <see cref="TimeoutDefinition"/>, an ISO 8601 duration expression or a runtime expression, with the given input and arguments, if any.
    /// </summary>
    /// <param name="expressionEvaluator">The <see cref="IRuntimeExpressionEvaluator"/> to use to evaluate the value, if it's a runtime expression</param>
    /// <param name="value">The value to evaluate, which can be either a <see cref="TimeoutDefinition"/>, an ISO 8601 duration expression or a runtime expression</param>
    /// <param name="input">The input to evaluate the value with</param>
    /// <param name="arguments">The arguments, if any, to evaluate the value with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the value evaluation</returns>
    public static async Task<Duration?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, OneOf<TimeoutDefinition, string>? value, JsonObject input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        return value is null ? null : await value.MatchAsync
        (
            async (timeout, ct) => await expressionEvaluator.EvaluateAsync(timeout.After, input, arguments, ct),
            async (expression, ct) =>
            {
                if (!expression.IsRuntimeExpression()) return Duration.FromTimeSpan(System.Xml.XmlConvert.ToTimeSpan(expression));
                var node = await expressionEvaluator.EvaluateAsync(expression, input, arguments, ct);
                if (node is null) return null!;
                return JsonSerializer.Deserialize(node, Sdk.Serialization.Json.JsonSerializationContext.Default.Duration);
            },
            cancellationToken
        );
    }

    /// <summary>
    /// Evaluates the specified value, which can be either a <see cref="Duration"/>, an ISO 8601 duration expression or a runtime expression, with the given input and arguments, if any.
    /// </summary>
    /// <param name="expressionEvaluator">The <see cref="IRuntimeExpressionEvaluator"/> to use to evaluate the value, if it's a runtime expression</param>
    /// <param name="value">The value to evaluate, which can be either a <see cref="Duration"/>, an ISO 8601 duration expression or a runtime expression</param>
    /// <param name="input">The input to evaluate the value with</param>
    /// <param name="arguments">The arguments, if any, to evaluate the value with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the value evaluation</returns>
    public static async Task<Duration?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, OneOf<Duration, string>? value, JsonObject input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        return value is null ? null : await value.MatchAsync
        (
            (duration, ct) => Task.FromResult<Duration?>(duration),
            async (expression, ct) =>
            {
                if (!expression.IsRuntimeExpression()) return Duration.FromTimeSpan(System.Xml.XmlConvert.ToTimeSpan(expression));
                var node = await expressionEvaluator.EvaluateAsync(expression, input, arguments, ct);
                if (node is null) return null!;
                return JsonSerializer.Deserialize(node, Sdk.Serialization.Json.JsonSerializationContext.Default.Duration);
            },
            cancellationToken
        );
    }

}
