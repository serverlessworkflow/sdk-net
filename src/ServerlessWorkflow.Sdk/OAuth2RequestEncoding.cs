namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes all supported request encodings for OAUTH2 requests
/// </summary>
public static class OAuth2RequestEncoding
{
    /// <summary>
    /// Represents the "application/x-www-form-urlencoded" content type
    /// </summary>
    public const string FormUrl = "application/x-www-form-urlencoded";
    /// <summary>
    /// Represents the "application/json" content type
    /// </summary>
    public const string Json = "application/json";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all supported values
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return FormUrl;
        yield return Json;
    }

}
