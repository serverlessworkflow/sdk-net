using Jint;
using Jint.Runtime.Interop;
using System.Collections;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an <see cref="IRuntimeExpressionEvaluator"/> that uses the JavaScript language to evaluate expressions
/// </summary>
public sealed class JSRuntimeExpressionEvaluator
    : IRuntimeExpressionEvaluator
{

    /// <inheritdoc/>
    public async Task<JsonNode?> EvaluateAsync(string expression, JsonObject input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        ArgumentNullException.ThrowIfNull(input);
        expression = expression.Trim();
        if (expression.StartsWith("${")) expression = expression[2..^1].Trim();
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        var js = new Engine(options =>
        {
            options.LimitMemory(1_000_000)
                .TimeoutInterval(TimeSpan.FromMilliseconds(500))
                .MaxStatements(500)
                .LimitRecursion(16)
                .CancellationToken(cancellationToken)
                .SetWrapObjectHandler((engine, target, type) =>
                {
                    var instance = ObjectWrapper.Create(engine, target);
                    if (DetermineIfObjectIsArrayLikeClrCollection(target.GetType())) instance.Prototype = engine.Intrinsics.Array.PrototypeObject;
                    return instance;
                })
            ;
        });
        js.SetValue("$", input);
        if (arguments != null) foreach (var property in arguments) js.SetValue(property.Key, property.Value);
        var jsValue = await js.Evaluate(expression).UnwrapIfPromiseAsync(cancellationToken);
        js.SetValue("__result", jsValue);
        var json = js.Evaluate("JSON.stringify(__result)").AsString();
        try
        {
            return JsonNode.Parse(json);
        }
        catch (JsonException ex)
        {
            throw new Exception($"An error occurred while deserializing the output of the expression evaluation: {ex.Message}");
        }
    }

    static bool DetermineIfObjectIsArrayLikeClrCollection(Type type)
    {
        var isDictionary = typeof(IDictionary).IsAssignableFrom(type);
        if (isDictionary) return false;
        if (typeof(ICollection).IsAssignableFrom(type)) return true;
        foreach (var interfaceType in type.GetInterfaces())
        {
            if (!interfaceType.IsGenericType) continue;
            if (interfaceType.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>) || interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)) return true;
        }
        return false;
    }

}
