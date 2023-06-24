namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IBasicAuthenticationBuilder"/>
/// </summary>
public class BasicAuthenticationBuilder
    : AuthenticationDefinitionBuilder, IBasicAuthenticationBuilder
{

    /// <summary>
    /// Initializes a new <see cref="BasicAuthenticationBuilder"/>
    /// </summary>
    public BasicAuthenticationBuilder() : base(new AuthenticationDefinition() { Scheme = AuthenticationScheme.Basic, Properties = new BasicAuthenticationProperties() }) { }

    /// <summary>
    /// Gets the <see cref="BasicAuthenticationProperties"/> of the authentication definition to build
    /// </summary>
    protected BasicAuthenticationProperties Properties => (BasicAuthenticationProperties)this.AuthenticationDefinition.Properties!;

    /// <inheritdoc/>
    public virtual IBasicAuthenticationBuilder WithUserName(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
        this.Properties.Username = username;
        return this;
    }

    /// <inheritdoc/>
    public virtual IBasicAuthenticationBuilder WithPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
        this.Properties.Password = password;
        return this;
    }

}
