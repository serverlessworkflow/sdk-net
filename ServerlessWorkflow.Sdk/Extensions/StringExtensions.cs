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

}
