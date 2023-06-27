namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="string"/>s
/// </summary>
public static class StringExtensions
{

    /// <summary>
    /// Converts the string to camel case
    /// </summary>
    /// <param name="input">The string to convert</param>
    /// <returns>The camel-cased string</returns>
    public static string ToCamelCase(this string input) => YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention.Instance.Apply(input);

    /// <summary>
    /// Converts the string to hyphen case
    /// </summary>
    /// <param name="input">The string to convert</param>
    /// <returns>The hyphen-cased string</returns>
    public static string ToHyphenCase(this string input) => YamlDotNet.Serialization.NamingConventions.HyphenatedNamingConvention.Instance.Apply(input);

    /// <summary>
    /// Converts the string to snake case
    /// </summary>
    /// <param name="input">The string to convert</param>
    /// <returns>The snake-cased string</returns>
    public static string ToSnakeCase(this string input) => YamlDotNet.Serialization.NamingConventions.UnderscoredNamingConvention.Instance.Apply(input);

    /// <summary>
    /// Determines whether or not the specified input is JSON format
    /// </summary>
    /// <param name="text">The input to check</param>
    /// <returns>A boolean indicating whether or not the specified text is JSON format</returns>
    public static bool IsJson(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));
        var text2 = text.Trim();
        if (!text2.StartsWith("[") || !text2.EndsWith("]"))
        {
            if (text2.StartsWith("{")) return text2.EndsWith("}");
            else return false;
        }
        return true;
    }

}
