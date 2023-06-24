namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IBearerAuthenticationBuilder"/>
/// </summary>
public class BearerAuthenticationBuilder
    : AuthenticationDefinitionBuilder, IBearerAuthenticationBuilder
{

    /// <summary>
    /// Initializes a new <see cref="BearerAuthenticationBuilder"/>
    /// </summary>
    public BearerAuthenticationBuilder() : base(new AuthenticationDefinition() { Scheme = AuthenticationScheme.Bearer, Properties = new BearerAuthenticationProperties() }) { }

    /// <summary>
    /// Gets the <see cref="BearerAuthenticationProperties"/> of the authentication definition to build
    /// </summary>
    protected BearerAuthenticationProperties Properties => (BearerAuthenticationProperties)this.AuthenticationDefinition.Properties!;

    /// <inheritdoc/>
    public virtual IBearerAuthenticationBuilder WithToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));
        this.Properties.Token = token;
        return this;
    }

}
