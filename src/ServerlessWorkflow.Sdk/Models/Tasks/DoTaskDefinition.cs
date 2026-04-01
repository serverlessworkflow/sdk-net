namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the configuration of a task that is composed of multiple subtasks to run sequentially
/// </summary>
[Description("Represents the configuration of a task that is composed of multiple subtasks to run sequentially")]
[DataContract]
public sealed record DoTaskDefinition
    : TaskDefinition
{

    /// <summary>
    /// Gets/sets a name/definition mapping of the subtasks to perform sequentially
    /// </summary>
    [Required, MinLength(1)]
    [Description("A name/definition mapping of the subtasks to perform sequentially")]
    [DataMember(Order = 1, Name = "do"), JsonPropertyOrder(1), JsonPropertyName("do")]
    public required Map<string, TaskDefinition> Do { get; init; }

}
