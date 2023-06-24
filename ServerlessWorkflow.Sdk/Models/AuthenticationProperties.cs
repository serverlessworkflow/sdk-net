namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure an authentication mechanism
/// </summary>
[DataContract, KnownType(nameof(GetKnownTypes))]
public abstract class AuthenticationProperties
{

    /// <summary>
    /// Initializes a new <see cref="AuthenticationProperties"/>
    /// </summary>
    protected AuthenticationProperties() 
    {
        this.Properties = new Dictionary<string, object>();
    }

    /// <summary>
    /// Initializes a new <see cref="AuthenticationProperties"/>
    /// </summary>
    /// <param name="properties">A key/value mapping of the authentication properties to wrap</param>
    protected AuthenticationProperties(IDictionary<string, object> properties)
    {
        this.Properties = properties ?? throw new ArgumentNullException(nameof(properties));
    }

    /// <summary>
    /// Gets/sets a key/value mapping of the authentication properties to wrap
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual IDictionary<string, object> Properties { get; set; }

    static Type[] GetKnownTypes()
    {
        return new Type[]
        {
            typeof(BasicAuthenticationProperties),
            typeof(BearerAuthenticationProperties),
            typeof(OAuth2AuthenticationProperties),
            typeof(SecretBasedAuthenticationProperties)
        };
    }

}
