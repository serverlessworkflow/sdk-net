namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an runtime expression argument used to describe the workflow being executed
/// </summary>
[Description("Represents an runtime expression argument used to describe the workflow being executed.")]
[DataContract]
public sealed record WorkflowDescriptor
{

    /// <summary>
    /// Gets/sets the workflow's id
    /// </summary>
    [Description("The workflow's id.")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "id"), JsonPropertyOrder(1), JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets/sets the workflow's definition
    /// </summary>
    [Description("The workflow's definition.")]
    [Required]
    [DataMember(Order = 2, Name = "definition"), JsonPropertyOrder(2), JsonPropertyName("definition")]
    public required WorkflowDefinition Definition { get; init; }

    /// <summary>
    /// Gets/sets the workflow's raw, untransformed input
    /// </summary>
    [Description("The workflow's raw, untransformed input.")]
    [DataMember(Order = 3, Name = "input"), JsonPropertyOrder(3), JsonPropertyName("input")]
    public JsonObject? Input { get; init; }

    /// <summary>
    /// Gets/sets the date and time at which the workflow has started
    /// </summary>
    [Description("The date and time at which the workflow has started.")]
    [Required]
    [DataMember(Order = 4, Name = "startedAt"), JsonPropertyOrder(4), JsonPropertyName("startedAt")]
    public DateTimeDescriptor? StartedAt { get; init; }

}
