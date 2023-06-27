namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure a 'Bearer' authentication scheme
/// </summary>

[DataContract]
public class BearerAuthenticationProperties
    : AuthenticationProperties
{

    /// <inheritdoc/>
    public BearerAuthenticationProperties() { }

    /// <inheritdoc/>
    public BearerAuthenticationProperties(IDictionary<string, object> properties) : base(properties) { }

    /// <summary>
    /// Gets/sets the token used to authenticate
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string Token
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Token).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            this.Properties[nameof(Token).ToCamelCase()] = value;
        }
    }

}
