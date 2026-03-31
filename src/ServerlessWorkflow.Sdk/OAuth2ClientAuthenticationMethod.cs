namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all supported OAUTH2 authentication methods
/// </summary>
public static class OAuth2ClientAuthenticationMethod
{

    /// <summary>
    /// Represents the "client_secret_basic" authentication method, where the client secret is sent using HTTP Basic Authentication.
    /// </summary>
    public const string Basic = "client_secret_basic";
    /// <summary>
    /// Represents the "client_secret_post" authentication method, where the client secret is sent in the body of the POST request.
    /// </summary>
    public const string Post = "client_secret_post";
    /// <summary>
    /// Represents the "client_secret_jwt" authentication method, where the client authenticates using a JWT signed with the client secret.
    /// </summary>
    public const string JwT = "client_secret_jwt";
    /// <summary>
    /// Represents the "private_key_jwt" authentication method, where the client authenticates using a JWT signed with a private key.
    /// </summary>
    public const string PrivateKey = "private_key_jwt";
    /// <summary>
    /// Represents the "none" authentication method, where no client authentication is performed.
    /// </summary>
    public const string None = "none";

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
        yield return Basic;
        yield return Post;
        yield return JwT;
        yield return PrivateKey;
        yield return None;
    }

}