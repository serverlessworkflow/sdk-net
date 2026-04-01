namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of a task used to raise an error
/// </summary>
[Description("Represents the definition of a task used to raise an error")]
[DataContract]
public sealed record RaiseTaskDefinition
    : TaskDefinition
{

    /// <summary>
    /// Gets/sets the definition of the error to raise
    /// </summary>
    [Description("The definition of the error to raise")]
    [Required]
    [DataMember(Order = 1, Name = "raise"), JsonPropertyOrder(1), JsonPropertyName("raise")]
    public required RaiseErrorDefinition Raise { get; init; }

}
