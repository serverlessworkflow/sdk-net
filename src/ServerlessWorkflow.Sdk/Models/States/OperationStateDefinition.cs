using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a workflow state that defines a set of actions to be performed in sequence or in parallel. Once all actions have been performed, a transition to another state can occur.
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.Operation)]
public class OperationStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="OperationStateDefinition"/>
    /// </summary>
    public OperationStateDefinition() : base(StateType.Operation) { }

    /// <summary>
    /// Gets/sets a value that specifies how actions are to be performed (in sequence of parallel). Defaults to sequential
    /// </summary>
    [DefaultValue(ActionExecutionMode.Sequential)]
    [DataMember(Order = 6, Name = "actionMode"), JsonPropertyOrder(6), JsonPropertyName("actionMode"), YamlMember(Alias = "actionMode", Order = 6)]
    public virtual string ActionMode { get; set; } = ActionExecutionMode.Sequential;

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> of actions to be performed if expression matches
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 7, Name = "actions", IsRequired = true), JsonPropertyOrder(7), JsonPropertyName("actions"), YamlMember(Alias = "actions", Order = 7)]
    public virtual List<ActionDefinition> Actions { get; set; } = new List<ActionDefinition>();

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
    public virtual bool TryGetAction(string name, out ActionDefinition? action)
    {
        action = this.GetAction(name);
        return action != null;
    }

    /// <summary>
    /// Attempts to get the next <see cref="ActionDefinition"/> in the pipeline
    /// </summary>
    /// <param name="previousActionName">The name of the <see cref="ActionDefinition"/> to get the next <see cref="ActionDefinition"/> for</param>
    /// <param name="action">The next <see cref="ActionDefinition"/>, if any</param>
    /// <returns>A boolean indicating whether or not there is a next <see cref="ActionDefinition"/> in the pipeline</returns>
    public virtual bool TryGetNextAction(string previousActionName, out ActionDefinition? action)
    {
        action = null;
        var previousAction = this.Actions.FirstOrDefault(a => a.Name == previousActionName);
        if (previousAction == null) return false;
        var previousActionIndex = this.Actions.ToList().IndexOf(previousAction);
        var nextIndex = previousActionIndex + 1;
        if (nextIndex >= this.Actions.Count) return false;
        action = this.Actions.ElementAt(nextIndex);
        return true;
    }

}
