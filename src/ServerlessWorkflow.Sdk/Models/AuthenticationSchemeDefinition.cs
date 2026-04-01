namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the base class for all authentication scheme definitions
/// </summary>
[Description("Represents the base class for all authentication scheme definitions")]
[DataContract]
public abstract record AuthenticationSchemeDefinition
    : Extendable
{

    /// <summary>
    /// Gets the name of the authentication scheme
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public abstract string Scheme { get; }

    /// <summary>
    /// Gets/sets the name of the secret, if any, used to configure the authentication scheme
    /// </summary>
    [Description("The name of the secret, if any, used to configure the authentication scheme")]
    [DataMember(Order = 1, Name = "use"), JsonPropertyOrder(1), JsonPropertyName("use")]
    public virtual string? Use { get; init; }

}