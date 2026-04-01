namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the configuration of a task that is composed of multiple subtasks to run concurrently
/// </summary>
[Description("Represents the configuration of a task that is composed of multiple subtasks to run concurrently")]
[DataContract]
public sealed record ForkTaskDefinition
    : TaskDefinition
{

    /// <summary>
    /// Gets/sets the configuration of the branches to perform concurrently
    /// </summary>
    [Description("The configuration of the branches to perform concurrently")]
    [Required]
    [DataMember(Order = 1, Name = "fork"), JsonPropertyOrder(1), JsonPropertyName("fork")]
    public required BranchingDefinition Fork { get; init; }

}
