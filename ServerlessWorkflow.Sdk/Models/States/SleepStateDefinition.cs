using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a workflow state that waits for a certain amount of time before transitioning to a next state
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.Sleep)]
public class SleepStateDefinition
   : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="SleepStateDefinition"/>
    /// </summary>
    public SleepStateDefinition() : base(StateType.Sleep) { }

    /// <summary>
    /// Gets/sets the amount of time to delay when in this state
    /// </summary>
    [DataMember(Order = 5, Name = "duration"), JsonPropertyName("duration"), YamlMember(Alias = "duration")]
    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public virtual TimeSpan Duration { get; set; }

}