namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of a task that executes a set of subtasks iteratively for each element in a collection
/// </summary>
[Description("Represents the definition of a task that executes a set of subtasks iteratively for each element in a collection")]
[DataContract]
public sealed record ForTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.For;

    /// <summary>
    /// Gets/sets the definition of the loop that iterates over a range of values
    /// </summary>
    [Description("The definition of the loop that iterates over a range of values")]
    [Required]
    [DataMember(Order = 1, Name = "for"), JsonPropertyOrder(1), JsonPropertyName("for")]
    public required ForLoopDefinition For { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression that represents the condition, if any, that must be met for the iteration to continue
    /// </summary>
    [Description("A runtime expression that represents the condition, if any, that must be met for the iteration to continue")]
    [DataMember(Order = 2, Name = "while"), JsonPropertyOrder(2), JsonPropertyName("while")]
    public string? While { get; init; }

    /// <summary>
    /// Gets/sets the tasks to perform for each item in the collection
    /// </summary>
    [Description("The tasks to perform for each item in the collection")]
    [Required]
    [DataMember(Order = 3, Name = "do"), JsonPropertyOrder(3), JsonPropertyName("do")]
    public required Map<string, TaskDefinition> Do { get; init; }

}
