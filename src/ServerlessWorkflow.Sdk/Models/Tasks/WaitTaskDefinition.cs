namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of a task used to wait a certain amount of time
/// </summary>
[Description("Represents the definition of a task used to wait a certain amount of time")]
[DataContract]
public sealed record WaitTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Wait;

    /// <summary>
    /// Gets/sets the amount of time to wait before resuming workflow
    /// </summary>
    [Description("The amount of time to wait before resuming workflow")]
    [Required]
    [DataMember(Order = 1, Name = "wait"), JsonPropertyOrder(1), JsonPropertyName("wait")]
    public required Duration Wait { get; init; }

}
