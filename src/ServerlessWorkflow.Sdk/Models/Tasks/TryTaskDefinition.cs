namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of a task used to try one or more subtasks, and to catch/handle the errors that can potentially be raised during execution
/// </summary>
[Description("Represents the definition of a task used to try one or more subtasks, and to catch/handle the errors that can potentially be raised during execution")]
[DataContract]
public sealed record TryTaskDefinition
    : TaskDefinition
{

    /// <summary>
    /// Gets/sets a name/definition map of the tasks to try running
    /// </summary>
    [Description("A name/definition map of the tasks to try running")]
    [Required]
    [DataMember(Order = 1, Name = "try"), JsonPropertyOrder(1), JsonPropertyName("try")]
    public required Map<string, TaskDefinition> Try { get; init; }

    /// <summary>
    /// Gets/sets the object used to define the errors to catch
    /// </summary>
    [Description("The object used to define the errors to catch")]
    [Required]
    [DataMember(Order = 2, Name = "catch"), JsonPropertyOrder(2), JsonPropertyName("catch")]
    public required ErrorCatcherDefinition Catch { get; init; }

}
