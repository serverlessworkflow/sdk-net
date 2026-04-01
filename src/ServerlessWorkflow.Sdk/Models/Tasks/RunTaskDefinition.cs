namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the configuration of a task used to run a given process
/// </summary>
[Description("Represents the configuration of a task used to run a given process")]
[DataContract]
public sealed record RunTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Run;

    /// <summary>
    /// Gets/sets the configuration of the process to execute
    /// </summary>
    [Description("The configuration of the process to execute")]
    [Required]
    [DataMember(Order = 1, Name = "run"), JsonPropertyOrder(1), JsonPropertyName("run")]
    public required ProcessTypeDefinition Run { get; init; }

}
