using Neuroglia;
using ServerlessWorkflow.Sdk.Serialization;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a reusable definition of a workflow authentication mechanism
/// </summary>
[DataContract]
public class AuthenticationDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the authentication definition's name
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "name", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Alias = "name", Order = 1)]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets the authentication definition's scheme
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 2, Name = "scheme", IsRequired = true), JsonPropertyOrder(2), JsonPropertyName("scheme"), YamlMember(Alias = "scheme", Order = 2)]
    public virtual string Scheme { get; set; } = null!;

    /// <summary>
    /// Gets/sets a <see cref="OneOf{T1, T2}"/> that represents the authentication definition's <see cref="AuthenticationProperties"/>
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 3, Name = "properties", IsRequired = true), JsonPropertyOrder(3), JsonPropertyName("properties"), YamlMember(Alias = "properties", Order = 3)]
    [JsonConverter(typeof(OneOfConverter<string, IDictionary<string, object>>))]
    protected virtual OneOf<string, IDictionary<string, object>> PropertiesValue { get; set; } = null!;

    /// <summary>
    /// Gets/sets the <see cref="AuthenticationDefinition"/>'s properties
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual AuthenticationProperties Properties
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(this.PropertiesValue.T1Value)) return new SecretBasedAuthenticationProperties(this.PropertiesValue.T1Value);
            if (this.PropertiesValue?.T2Value == null) return null!;
            return this.Scheme switch
            {
                AuthenticationScheme.Basic => new BasicAuthenticationProperties(this.PropertiesValue.T2Value),
                AuthenticationScheme.Bearer => new BearerAuthenticationProperties(this.PropertiesValue.T2Value),
                AuthenticationScheme.OAuth2 => new OAuth2AuthenticationProperties(this.PropertiesValue.T2Value),
                _ => new ExtensionAuthenticationProperties(this.PropertiesValue.T2Value)
            };
        }
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            switch (value)
            {
                case BasicAuthenticationProperties:
                    this.Scheme = AuthenticationScheme.Basic;
                    break;
                case BearerAuthenticationProperties:
                    this.Scheme = AuthenticationScheme.Bearer;
                    break;
                case OAuth2AuthenticationProperties:
                    this.Scheme = AuthenticationScheme.OAuth2;
                    break;
                case SecretBasedAuthenticationProperties secretBasedProperties:
                    this.PropertiesValue = secretBasedProperties.Secret;
                    break;
                default:
                    throw new NotSupportedException($"The specified authentication info type '{value.GetType()}' is not supported");
            }
            this.PropertiesValue = value.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }

    /// <inheritdoc/>
    [DataMember(Order = 4, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => this.Name;

}