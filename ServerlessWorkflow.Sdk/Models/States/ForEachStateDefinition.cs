using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a workflow state that executes a set of defined actions or workflows for each element of a data array
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.ForEach)]
public class ForEachStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="ForEachStateDefinition"/>
    /// </summary>
    public ForEachStateDefinition() : base(StateType.ForEach) { }

    /// <summary>
    /// gets/sets an expression selecting an array element of the states data
    /// </summary>
    [DataMember(Order = 5, Name = "inputCollection"), JsonPropertyOrder(5), JsonPropertyName("inputCollection"), YamlMember(Alias = "inputCollection", Order = 5)]
    public virtual string? InputCollection { get; set; }

    /// <summary>
    /// Gets/sets an expression specifying an array element of the states data to add the results of each iteration
    /// </summary>
    [DataMember(Order = 6, Name = "outputCollection"), JsonPropertyOrder(6), JsonPropertyName("outputCollection"), YamlMember(Alias = "outputCollection", Order = 6)]
    public virtual string? OutputCollection { get; set; }

    /// <summary>
    /// Gets/sets the name of the iteration parameter that can be referenced in actions/workflow. For each parallel iteration, this param should contain an unique element of the array referenced by the  <see cref="InputCollection"/> expression
    /// </summary>
    [DataMember(Order = 7, Name = "iterationParam"), JsonPropertyOrder(7), JsonPropertyName("iterationParam"), YamlMember(Alias = "iterationParam", Order = 7)]
    public virtual string? IterationParam { get; set; }

    /// <summary>
    /// Gets/sets a uint that specifies how upper bound on how many iterations may run in parallel
    /// </summary>
    [DataMember(Order = 8, Name = "batchSize"), JsonPropertyOrder(8), JsonPropertyName("batchSize"), YamlMember(Alias = "batchSize", Order = 8)]
    public virtual int? BatchSize { get; set; }

    /// <summary>
    /// Gets/sets a value used to configure the way the actions of each iterations should be executed
    /// </summary>
    [DataMember(Order = 9, Name = "mode"), JsonPropertyOrder(9), JsonPropertyName("mode"), YamlMember(Alias = "mode", Order = 9)]
    public virtual string Mode { get; set; } = ActionExecutionMode.Sequential;

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> of actions to be executed for each of the elements of the <see cref="InputCollection"/>
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 10, Name = "actions"), JsonPropertyOrder(10), JsonPropertyName("actions"), YamlMember(Alias = "actions", Order = 10)]
    public virtual List<ActionDefinition> Actions { get; set; } = new List<ActionDefinition>();

    /// <summary>
    /// Gets the <see cref="ActionDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="ActionDefinition"/> to get</param>
    /// <returns>The <see cref="ActionDefinition"/> with the specified name</returns>
    public virtual ActionDefinition? GetAction(string name)
    {
        return this.Actions.FirstOrDefault(s => s.Name == name);
    }

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
        var previousAction = this.Actions.FirstOrDefault(a => a.Name == previousActionName)!;
        var previousActionIndex = this.Actions.ToList().IndexOf(previousAction);
        var nextIndex = previousActionIndex + 1;
        if (nextIndex >= this.Actions.Count)
            return false;
        action = this.Actions.ElementAt(nextIndex);
        return true;
    }

}
