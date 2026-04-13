namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Represents an helper class used to perform string formatting operations
/// </summary>
public static class StringFormatter
{

    /// <summary>
    /// Formats the specified string template by replacing the placeholders with the provided parameters
    /// </summary>
    /// <param name="template">The string template containing the placeholders to replace</param>
    /// <param name="parameters">A key-value collection containing the parameters to replace in the template, where the key represents the placeholder name and the value represents the value to replace it with</param>
    /// <returns>The formatted string</returns>
    public static string Format(string template, IDictionary<string, object?>? parameters)
    {
        if (string.IsNullOrWhiteSpace(template) || parameters == null || !parameters.Any()) return template;
        var text = template;
        foreach (var parameter in parameters) text = text.Replace("{" + parameter.Key + "}", parameter.Value?.ToString(), StringComparison.OrdinalIgnoreCase);
        return text;
    }

}