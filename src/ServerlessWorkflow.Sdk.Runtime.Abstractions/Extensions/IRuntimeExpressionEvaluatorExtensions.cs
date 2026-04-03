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
    public static async Task<bool> EvaluateConditionAsync(this IRuntimeExpressionEvaluator expressionEvaluator, string expression, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
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
    public static async Task<Duration?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, OneOf<TimeoutDefinition, string>? value, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        return value is null ? null : await value.MatchAsync
        (
            async (timeout, ct) => await expressionEvaluator.EvaluateAsync(timeout.After, input, arguments, ct).ConfigureAwait(false),
            async (expression, ct) =>
            {
                if (!expression.IsRuntimeExpression()) return Duration.FromTimeSpan(System.Xml.XmlConvert.ToTimeSpan(expression));
                var node = await expressionEvaluator.EvaluateAsync(expression, input, arguments, ct).ConfigureAwait(false);
                if (node is null) return null!;
                return JsonSerializer.Deserialize(node, Serialization.Json.JsonSerializationContext.Default.Duration);
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
    public static async Task<Duration?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, OneOf<Duration, string>? value, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        return value is null ? null : await value.MatchAsync
        (
            (duration, ct) => Task.FromResult<Duration?>(duration),
            async (expression, ct) =>
            {
                if (!expression.IsRuntimeExpression()) return Duration.FromTimeSpan(System.Xml.XmlConvert.ToTimeSpan(expression));
                var node = await expressionEvaluator.EvaluateAsync(expression, input, arguments, ct).ConfigureAwait(false);
                if (node is null) return null!;
                return JsonSerializer.Deserialize(node, Serialization.Json.JsonSerializationContext.Default.Duration);
            },
            cancellationToken
        );
    }

    /// <summary>
    /// Evaluates the specified value, which can be either a <see cref="JsonObject"/>, a JSON string or a runtime expression, with the given input and arguments, if any.
    /// </summary>
    /// <param name="expressionEvaluator">The <see cref="IRuntimeExpressionEvaluator"/> to use to evaluate the value, if it's a runtime expression</param>
    /// <param name="value">The value to evaluate, which can be either a <see cref="JsonObject"/>, a JSON string or a runtime expression</param>
    /// <param name="input">The input to evaluate the value with</param>
    /// <param name="arguments">The arguments, if any, to evaluate the value with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the value evaluation</returns>
    public static async Task<JsonNode?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, OneOf<JsonObject, string>? value, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        if (value is null) return null;
        return await value.MatchAsync
        (
            async (jsonObject, ct) => await expressionEvaluator.EvaluateAsync(jsonObject, input, arguments, cancellationToken).ConfigureAwait(false),
            async (expression, ct) =>
            {
                if (!expression.IsRuntimeExpression()) return JsonSerializer.Deserialize(expression, Serialization.Json.JsonSerializationContext.Default.JsonNode);
                return await expressionEvaluator.EvaluateAsync(expression, input, arguments, ct).ConfigureAwait(false);
            },
            cancellationToken
        );
    }

    /// <summary>
    /// Evaluates the specified <see cref="JsonNode"/>, if any.
    /// </summary>
    /// <param name="expressionEvaluator">The <see cref="IRuntimeExpressionEvaluator"/> to use to evaluate the value, if it's a runtime expression</param>
    /// <param name="value">The <see cref="JsonNode"/> to evaluate, if any</param>
    /// <param name="input">The input to evaluate the value with</param>
    /// <param name="arguments">The arguments, if any, to evaluate the value with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the value evaluation</returns>
    public static async Task<JsonNode?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, JsonNode? value, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        if (value is null) return null;
        return value switch
        {
            JsonArray jsonArray => await expressionEvaluator.EvaluateAsync(jsonArray, input, arguments, cancellationToken).ConfigureAwait(false),
            JsonObject jsonObject => await expressionEvaluator.EvaluateAsync(jsonObject, input, arguments, cancellationToken).ConfigureAwait(false),
            JsonValue jsonValue => await expressionEvaluator.EvaluateAsync(jsonValue, input, arguments, cancellationToken).ConfigureAwait(false),
            _ => value
        };
    }

    /// <summary>
    /// Evaluates the specified <see cref="JsonArray"/>, if any.
    /// </summary>
    /// <param name="expressionEvaluator">The <see cref="IRuntimeExpressionEvaluator"/> to use to evaluate the value, if it's a runtime expression</param>
    /// <param name="value">The <see cref="JsonArray"/> to evaluate, if any</param>
    /// <param name="input">The input to evaluate the value with</param>
    /// <param name="arguments">The arguments, if any, to evaluate the value with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the value evaluation</returns>
    public static async Task<JsonNode?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, JsonArray? value, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        if (value is null) return null;
        var nodes = new List<JsonNode>(value.Count);
        foreach (var node in value) nodes.Add(await expressionEvaluator.EvaluateAsync(node, input, arguments, cancellationToken).ConfigureAwait(false) ?? throw new InvalidOperationException("Unexpected null value"));
        return new JsonArray(nodes.ToArray());
    }

    /// <summary>
    /// Evaluates the specified <see cref="JsonObject"/>, if any.
    /// </summary>
    /// <param name="expressionEvaluator">The <see cref="IRuntimeExpressionEvaluator"/> to use to evaluate the value, if it's a runtime expression</param>
    /// <param name="value">The <see cref="JsonObject"/> to evaluate, if any</param>
    /// <param name="input">The input to evaluate the value with</param>
    /// <param name="arguments">The arguments, if any, to evaluate the value with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the value evaluation</returns>
    public static async Task<JsonNode?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, JsonObject? value, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        if (value is null) return null;
        var properties = new List<KeyValuePair<string, JsonNode?>>(value.Count);
        foreach (var property in value) properties.Add(new KeyValuePair<string, JsonNode?>(property.Key, await expressionEvaluator.EvaluateAsync(property.Value, input, arguments, cancellationToken).ConfigureAwait(false) ?? throw new InvalidOperationException("Unexpected null value")));
        return new JsonObject(properties);
    }

    /// <summary>
    /// Evaluates the specified <see cref="JsonValue"/>, if any.
    /// </summary>
    /// <param name="expressionEvaluator">The <see cref="IRuntimeExpressionEvaluator"/> to use to evaluate the value, if it's a runtime expression</param>
    /// <param name="value">The <see cref="JsonValue"/> to evaluate, if any</param>
    /// <param name="input">The input to evaluate the value with</param>
    /// <param name="arguments">The arguments, if any, to evaluate the value with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the value evaluation</returns>
    public static async Task<JsonNode?> EvaluateAsync(this IRuntimeExpressionEvaluator expressionEvaluator, JsonValue? value, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        if (value is null) return null;
        if (value.TryGetValue<string>(out var expression) && expression.IsRuntimeExpression()) return await expressionEvaluator.EvaluateAsync(expression, input, arguments, cancellationToken).ConfigureAwait(false);
        return value;
    }

}
