namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an <see cref="EventStateDefinition"/>'s trigger
/// </summary>
[DataContract]
public class EventStateTriggerDefinition
{

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> containing the references one or more unique event names in the defined workflow events
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "eventRefs", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("eventRefs"), YamlMember(Alias = "eventRefs", Order = 1)]
    public virtual List<string> EventRefs { get; set; } = new List<string>();

    /// <summary>
    /// Gets/sets a value that specifies how actions are to be performed (in sequence of parallel)
    /// </summary>
    [DataMember(Order = 2, Name = "actionMode"), JsonPropertyOrder(2), JsonPropertyName("actionMode"), YamlMember(Alias = "actionMode", Order = 2)]
    public virtual string ActionMode { get; set; } = ActionExecutionMode.Sequential;

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> containing the actions to be performed if expression matches
    /// </summary>
    [DataMember(Order = 3, Name = "actions"), JsonPropertyOrder(3), JsonPropertyName("actions"), YamlMember(Alias = "actions", Order = 3)]
    public virtual List<ActionDefinition> Actions { get; set; } = new List<ActionDefinition>();

    /// <summary>
    /// Gets/sets an object used to filter the event data 
    /// </summary>
    [DataMember(Order = 4, Name = "eventDataFilter"), JsonPropertyOrder(4), JsonPropertyName("eventDataFilter"), YamlMember(Alias = "eventDataFilter", Order = 4)]
    public virtual EventDataFilterDefinition EventDataFilter { get; set; } = new EventDataFilterDefinition();

    /// <summary>
    /// Gets the <see cref="ActionDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="ActionDefinition"/> to get</param>
    /// <returns>The <see cref="ActionDefinition"/> with the specified name</returns>
    public virtual ActionDefinition? GetAction(string name) => this.Actions.FirstOrDefault(s => s.Name == name);

    /// <summary>
    /// Attempts to get the <see cref="ActionDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="ActionDefinition"/> to get</param>
    /// <param name="action">The <see cref="ActionDefinition"/> with the specified name</param>
    /// <returns>A boolean indicating whether or not a <see cref="ActionDefinition"/> with the specified name could be found</returns>
    public virtual bool TryGetAction(string name, out ActionDefinition action)
    {
        action = this.GetAction(name)!;
        return action != null;
    }

    /// <summary>
    /// Attempts to get the next <see cref="ActionDefinition"/> in the pipeline
    /// </summary>
    /// <param name="previousActionName">The name of the <see cref="ActionDefinition"/> to get the next <see cref="ActionDefinition"/> for</param>
    /// <param name="action">The next <see cref="ActionDefinition"/>, if any</param>
    /// <returns>A boolean indicating whether or not there is a next <see cref="ActionDefinition"/> in the pipeline</returns>
    public virtual bool TryGetNextAction(string previousActionName, out ActionDefinition action)
    {
        action = null!;
        var previousAction = this.Actions.FirstOrDefault(a => a.Name == previousActionName);
        if (previousAction == null) return false;
        var previousActionIndex = this.Actions.ToList().IndexOf(previousAction);
        var nextIndex = previousActionIndex + 1;
        if (nextIndex >= this.Actions.Count) return false;
        action = this.Actions.ElementAt(nextIndex);
        return true;
    }

}
