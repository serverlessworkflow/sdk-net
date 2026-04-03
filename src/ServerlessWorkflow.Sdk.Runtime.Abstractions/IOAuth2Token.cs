using System.Reflection.Metadata;

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of an OAUTH2 token
/// </summary>
public interface IOAuth2Token
{

    /// <summary>
    /// Gets the UTC date and time at which the <see cref="IOAuth2Token"/> has been created
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the OAUTH2 token type
    /// </summary>
    string? TokenType { get; }

    /// <summary>
    /// Gets the OAUTH2 token id
    /// </summary>
    string? TokenId { get; }

    /// <summary>
    /// Gets the OAUTH2 access token
    /// </summary>
    string? AccessToken { get; }

    /// <summary>
    /// Gets the OAUTH2 refresh token
    /// </summary>
    string? RefreshToken { get; }

    /// <summary>
    /// Gets the <see cref="IOAuth2Token"/> Time To Live, in seconds
    /// </summary>
    int Ttl { get; }

    /// <summary>
    /// Gets the UTC date and time at which the <see cref="IOAuth2Token"/> expires
    /// </summary>
    DateTime? ExpiresAt { get; }

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="IOAuth2Token"/> has expired
    /// </summary>
    bool HasExpired { get; }

}
