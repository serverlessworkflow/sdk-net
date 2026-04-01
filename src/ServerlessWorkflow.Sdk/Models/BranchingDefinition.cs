namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure branches to perform concurrently
/// </summary>
[Description("Represents an object used to configure branches to perform concurrently")]
[DataContract]
public sealed record BranchingDefinition
{

    /// <summary>
    /// Gets/sets a name/definition mapping of the subtasks to perform concurrently
    /// </summary>
    [Description("A name/definition mapping of the subtasks to perform concurrently")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "branches"), JsonPropertyOrder(1), JsonPropertyName("branches")]
    public required Map<string, TaskDefinition> Branches { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the branches should compete each other. If `true` and if a branch completes, it will cancel all other branches then it will return its output as the task's output
    /// </summary>
    [Description("A boolean indicating whether or not the branches should compete each other. If `true` and if a branch completes, it will cancel all other branches then it will return its output as the task's output")]
    [DataMember(Order = 2, Name = "compete"), JsonPropertyOrder(2), JsonPropertyName("compete")]
    public bool Compete { get; init; }

}