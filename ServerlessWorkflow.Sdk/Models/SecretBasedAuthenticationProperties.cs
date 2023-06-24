namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents <see cref="AuthenticationProperties"/> loaded from a specific secret
/// </summary>
[DataContract]
public class SecretBasedAuthenticationProperties
    : AuthenticationProperties
{

    /// <summary>
    /// Initializes a new <see cref="SecretBasedAuthenticationProperties"/>
    /// </summary>
    public SecretBasedAuthenticationProperties() { }

    /// <summary>
    /// Initializes a new <see cref="SecretBasedAuthenticationProperties"/>
    /// </summary>
    /// <param name="secret">The name of the secret to load the <see cref="SecretBasedAuthenticationProperties"/> from</param>
    public SecretBasedAuthenticationProperties(string secret)
    {
        if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentNullException(nameof(secret));
        this.Secret = secret;
    }

    /// <summary>
    /// Gets the name of the secret to load the <see cref="SecretBasedAuthenticationProperties"/> from
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "secret", IsRequired = true), JsonPropertyName("secret"), YamlMember(Alias = "secret")]
    public virtual string Secret { get; set; } = null!;

}
