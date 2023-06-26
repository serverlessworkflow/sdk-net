using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a workflow state that performs an action, then waits for the callback event that denotes completion of the action
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.Callback)]
public class CallbackStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="CallbackStateDefinition"/>
    /// </summary>
    public CallbackStateDefinition() : base(StateType.Callback) { }

    /// <summary>
    /// Gets/sets the action to be executed
    /// </summary>
    [DataMember(Order = 6, Name = "action"), JsonPropertyOrder(6), JsonPropertyName("action"), YamlMember(Alias = "action", Order = 6)]
    public virtual ActionDefinition? Action { get; set; }

    /// <summary>
    /// Gets/sets a reference to the callback event to await
    /// </summary>
    [DataMember(Order = 7, Name = "eventRef"), JsonPropertyOrder(7), JsonPropertyName("eventRef"), YamlMember(Alias = "eventRef", Order = 7)]
    public virtual string? EventRef { get; set; }

    /// <summary>
    /// Gets/sets the time period to wait for incoming events
    /// </summary>
    [DataMember(Order = 8, Name = "timeout"), JsonPropertyOrder(8), JsonPropertyName("timeout"), YamlMember(Alias = "timeout", Order = 8)]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))]
    public virtual TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Gets/sets the callback event data filter definition
    /// </summary>
    [DataMember(Order = 9, Name = "eventDataFilter"), JsonPropertyOrder(9), JsonPropertyName("eventDataFilter"), YamlMember(Alias = "eventDataFilter", Order = 9)]
    public virtual EventDataFilterDefinition EventDataFilter { get; set; } = new();

}
