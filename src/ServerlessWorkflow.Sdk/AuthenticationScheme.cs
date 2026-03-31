namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes all default authentication schemes
/// </summary>
public static class AuthenticationScheme
{

    /// <summary>
    /// Gets the 'Basic' authentication scheme
    /// </summary>
    public const string Basic = "Basic";
    /// <summary>
    /// Gets the 'Bearer' authentication scheme
    /// </summary>
    public const string Bearer = "Bearer";
    /// <summary>
    /// Gets the 'Certificate' authentication scheme
    /// </summary>
    public const string Certificate = "Certificate";
    /// <summary>
    /// Gets the 'Digest' authentication scheme
    /// </summary>
    public const string Digest = "Digest";
    /// <summary>
    /// Gets the 'OAUTH2' authentication scheme
    /// </summary>
    public const string OAuth2 = "OAuth2";
    /// <summary>
    /// Gets the 'OpenIDConnect' authentication scheme
    /// </summary>
    public const string OpenIDConnect = "OpenIDConnect";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the authentication schemes supported by default
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing the authentication schemes supported by default
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the authentication schemes supported by default</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Basic;
        yield return Bearer;
        yield return Certificate;
        yield return Digest;
        yield return OAuth2;
        yield return OpenIDConnect;
    }

}
