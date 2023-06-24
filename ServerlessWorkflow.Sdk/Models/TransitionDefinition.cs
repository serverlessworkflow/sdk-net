namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to define a state transition
/// </summary>
[DataContract]
public class TransitionDefinition
    : StateOutcomeDefinition
{

    /// <summary>
    /// Gets/sets the name of state to transition to
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "nextState", IsRequired = true), JsonPropertyName("nextState"), YamlMember(Alias = "nextState")]
    public virtual string NextState { get; set; } = null!;

    /// <summary>
    /// Gets/sets an <see cref="IEnumerable{T}"/> containing the events to be produced before the transition happens
    /// </summary>
    [DataMember(Order = 2, Name = "produceEvents"), JsonPropertyName("produceEvents"), YamlMember(Alias = "produceEvents")]
    public virtual List<ProduceEventDefinition>? ProduceEvents { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to trigger workflow compensation before the transition is taken. Default is false
    /// </summary>
    [DataMember(Order = 3, Name = "compensate"), JsonPropertyName("compensate"), YamlMember(Alias = "compensate")]
    public virtual bool Compensate { get; set; } = false;

}
