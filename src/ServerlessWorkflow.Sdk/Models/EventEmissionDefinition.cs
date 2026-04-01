namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the configuration of an event's emission
/// </summary>
[Description("Represents the configuration of an event's emission")]
[DataContract]
public sealed record EventEmissionDefinition
{

    /// <summary>
    /// Gets/sets the definition of the event to emit
    /// </summary>
    [Description("The definition of the event to emit")]
    [Required]
    [DataMember(Order = 1, Name = "event"), JsonPropertyOrder(1), JsonPropertyName("event")]
    public required EventDefinition Event { get; init; }

}
