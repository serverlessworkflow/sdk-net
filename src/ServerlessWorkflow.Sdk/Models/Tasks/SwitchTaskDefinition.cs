namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of a task that evaluates conditions and executes specific branches based on the result
/// </summary>
[Description("Represents the definition of a task that evaluates conditions and executes specific branches based on the result")]
[DataContract]
public sealed record SwitchTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Switch;

    /// <summary>
    /// Gets/sets the definition of the switch to use
    /// </summary>
    [Description("The definition of the switch to use")]
    [Required]
    [DataMember(Order = 1, Name = "switch"), JsonPropertyOrder(1), JsonPropertyName("switch")]
    public required Map<string, SwitchCaseDefinition> Switch { get; init; }

}
