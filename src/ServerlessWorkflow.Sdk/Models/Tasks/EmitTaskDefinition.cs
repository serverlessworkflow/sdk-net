namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the configuration of a task used to emit an event
/// </summary>
[Description("Represents the configuration of a task used to emit an event")]
[DataContract]
public sealed record EmitTaskDefinition
    : TaskDefinition
{

    /// <summary>
    /// Gets/sets the configuration of an event's emission
    /// </summary>
    [Description("The configuration of an event's emission")]
    [DataMember(Order = 1, Name = "emit"), JsonPropertyOrder(1), JsonPropertyName("emit")]
    public required EventEmissionDefinition Emit { get; init; }

}
