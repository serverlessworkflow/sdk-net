namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an event-based <see cref="SwitchStateDefinition"/>
/// </summary>
[DataContract]
public class EventCaseDefinition
    : SwitchCaseDefinition
{

    /// <summary>
    /// Gets/sets the unique event name the condition applies to
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "eventRef", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("eventRef"), YamlMember(Alias = "eventRef", Order = 1)]
    public string EventRef { get; set; } = null!;

}