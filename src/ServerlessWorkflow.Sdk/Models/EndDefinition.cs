namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to explicitly define execution completion of a workflow instance or workflow execution path.
/// </summary>
[DataContract]
public class EndDefinition
    : StateOutcomeDefinition
{

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to terminate the executing workflow. If true, completes all execution flows in the given workflow instance. Defaults to false.
    /// </summary>
    [DataMember(Order = 1, Name = "terminate"), JsonPropertyOrder(1), JsonPropertyName("terminate"), YamlMember(Alias = "terminate", Order = 1)]
    public virtual bool Terminate { get; set; } = false;

    /// <summary>
    /// Gets/sets an <see cref="IEnumerable{T}"/> containing the events that should be produced
    /// </summary>
    [DataMember(Order = 2, Name = "produceEvents"), JsonPropertyOrder(2), JsonPropertyName("produceEvents"), YamlMember(Alias = "produceEvents", Order = 2)]
    public virtual IEnumerable<ProduceEventDefinition>? ProduceEvents { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the state should trigger compensation. Default is false.
    /// </summary>
    [DataMember(Order = 3, Name = "compensate"), JsonPropertyOrder(3), JsonPropertyName("compensate"), YamlMember(Alias = "compensate", Order = 3)]
    public virtual bool Compensate { get; set; } = false;

}