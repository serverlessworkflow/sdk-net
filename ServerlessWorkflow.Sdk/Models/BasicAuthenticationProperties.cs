namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure a 'Basic' authentication scheme
/// </summary>
[DataContract]
public class BasicAuthenticationProperties
    : AuthenticationProperties
{

    /// <inheritdoc/>
    public BasicAuthenticationProperties() { }

    /// <inheritdoc/>
    public BasicAuthenticationProperties(IDictionary<string, object> properties) : base(properties) { }

    /// <summary>
    /// Gets/sets the username to use when authenticating
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string Username
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Username).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            this.Properties[nameof(Username).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets the password to use when authenticating
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string Password
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Password).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            this.Properties[nameof(Password).ToCamelCase()] = value;
        }
    }

}
