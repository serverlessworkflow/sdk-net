#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="string"/>s
/// </summary>
public static class StringExtensions
{

    /// <summary>
    /// Determines whether the specified string is formatted as a runtime expression.
    /// </summary>
    /// <param name="value">The string to evaluate for runtime expression formatting.</param>
    /// <returns>true if the string starts with "${" and ends with "}"; otherwise, false.</returns>
    public static bool IsRuntimeExpression(this string value) => value.TrimStart().StartsWith("${") && value.TrimEnd().EndsWith("}");

}
