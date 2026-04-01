namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of a task used to set data
/// </summary>
[Description("Represents the definition of a task used to set data")]
[DataContract]
public sealed record SetTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Set;

    /// <summary>
    /// Gets/sets the data to set
    /// </summary>
    [Description("The data to set")]
    [Required]
    [DataMember(Order = 1, Name = "set"), JsonPropertyOrder(1), JsonPropertyName("set")]
    public required JsonObject Set { get; init; }

}