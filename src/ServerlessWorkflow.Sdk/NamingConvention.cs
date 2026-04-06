namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes constants about the Serverless Workflow DSL naming convention
/// </summary>
public static class NamingConvention
{

    static readonly int maxLength = 63;

    /// <summary>
    /// Determines whether or not the specified value is a valid name, following <see href="https://datatracker.ietf.org/doc/html/rfc1123">RFC 1123</see> DNS label name
    /// </summary>
    /// <param name="name">The name to check</param>
    /// <returns>A boolean indicating whether or not the specified name follows specification for <see href="https://datatracker.ietf.org/doc/html/rfc1123">RFC 1123</see> DNS label name</returns>
    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        return name.Length <= maxLength
            && name.All(c => char.IsDigit(c) || c == '-' || (char.IsLetter(c) && char.IsLower(c)))
            && char.IsLetterOrDigit(name.First())
            && char.IsLetterOrDigit(name.Last());
    }

}