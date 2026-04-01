namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an event
/// </summary>
[Description("Represents the definition of an event")]
[DataContract]
public sealed record EventDefinition
{

    /// <summary>
    /// Gets/sets a key/value mapping of the attributes of the configured event
    /// </summary>
    [Description("A key/value mapping of the attributes of the configured event")]
    [Required]
    [DataMember(Order = 1, Name = "with"), JsonPropertyOrder(1), JsonPropertyName("with")]
    public required JsonObject With { get; init; }

}