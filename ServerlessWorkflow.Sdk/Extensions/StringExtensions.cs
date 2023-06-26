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

}
