namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the base class for all <see cref="IAuthenticationDefinitionBuilder"/> implementations
/// </summary>
public abstract class AuthenticationDefinitionBuilder
    : IAuthenticationDefinitionBuilder
{

    /// <summary>
    /// Initializes a new <see cref="AuthenticationDefinitionBuilder"/>
    /// </summary>
    /// <param name="authenticationDefinition">The <see cref="Models.AuthenticationDefinition"/> to configure</param>
    protected AuthenticationDefinitionBuilder(AuthenticationDefinition authenticationDefinition)
    {
        this.AuthenticationDefinition = authenticationDefinition ?? throw new ArgumentNullException(nameof(authenticationDefinition));
    }

    /// <summary>
    /// Gets the <see cref="Models.AuthenticationDefinition"/> to configure
    /// </summary>
    protected AuthenticationDefinition AuthenticationDefinition { get; }

    /// <inheritdoc/>
    public virtual IAuthenticationDefinitionBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.AuthenticationDefinition.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAuthenticationDefinitionBuilder WithExtensionProperty(string name, object value)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.AuthenticationDefinition.ExtensionData ??= new Dictionary<string, object>();
        this.AuthenticationDefinition.ExtensionData[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAuthenticationDefinitionBuilder WithExtensionProperties(IDictionary<string, object> properties)
    {
        this.AuthenticationDefinition.ExtensionData = properties ?? throw new ArgumentNullException(nameof(properties));
        return this;
    }

    /// <inheritdoc/>
    public virtual void LoadFromSecret(string secret)
    {
        if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentNullException(nameof(secret));
        this.AuthenticationDefinition.Properties = new SecretBasedAuthenticationProperties(secret);
    }

    /// <inheritdoc/>
    public virtual AuthenticationDefinition Build() => this.AuthenticationDefinition;

}
