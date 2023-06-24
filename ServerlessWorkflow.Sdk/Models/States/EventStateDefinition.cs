using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a workflow state that awaits one or more events and perform actions when they are received
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.Event)]
public class EventStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="EventStateDefinition"/>
    /// </summary>
    public EventStateDefinition() : base(StateType.Event) { }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the <see cref="EventStateDefinition"/> awaits one or all of defined events.
    /// If 'true', consuming one of the defined events causes its associated actions to be performed. If 'false', all of the defined events must be consumed in order for actions to be performed. Defaults to 'true'.
    /// </summary>
    [DefaultValue(true)]
    [DataMember(Order = 5, Name = "exclusive"), JsonPropertyOrder(5), JsonPropertyName("exclusive"), YamlMember(Alias = "exclusive", Order = 5)]
    public virtual bool Exclusive { get; set; } = true;

    /// <summary>
    /// Gets/sets an object used to configure the <see cref="EventStateDefinition"/>'s triggers and actions
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 6, Name = "onEvents", IsRequired = true), JsonPropertyOrder(6), JsonPropertyName("onEvents"), YamlMember(Alias = "onEvents", Order = 6)]
    public virtual List<EventStateTriggerDefinition> OnEvents { get; set; } = new List<EventStateTriggerDefinition>();

    /// <summary>
    /// Gets/sets the duration to wait for incoming events
    /// </summary>
    [DataMember(Order = 7, Name = "timeout", IsRequired = true), JsonPropertyOrder(7), JsonPropertyName("timeout"), YamlMember(Alias = "timeout", Order = 7)]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))]
    public virtual TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Gets the <see cref="EventStateTriggerDefinition"/> with the specified id
    /// </summary>
    /// <param name="id">The id of the <see cref="EventStateTriggerDefinition"/> to get</param>
    /// <returns>The <see cref="EventStateTriggerDefinition"/> with the specified id</returns>
    public virtual EventStateTriggerDefinition GetTrigger(int id) => this.OnEvents.ElementAt(id);

    /// <summary>
    /// Attempts to get the <see cref="EventStateTriggerDefinition"/> with the specified id
    /// </summary>
    /// <param name="id">The name of the <see cref="EventStateTriggerDefinition"/> to get</param>
    /// <param name="trigger">The <see cref="EventStateTriggerDefinition"/> with the specified id</param>
    /// <returns>A boolean indicating whether or not a <see cref="EventStateTriggerDefinition"/> with the specified id could be found</returns>
    public virtual bool TryGetTrigger(int id, out EventStateTriggerDefinition? trigger)
    {
        trigger = null!;
        try
        {
            trigger = this.GetTrigger(id);
        }
        catch
        {
            return false;
        }
        return trigger != null;
    }

}
