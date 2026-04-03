namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an runtime expression argument used to describe the task being executed
/// </summary>
[Description("Represents an runtime expression argument used to describe the task being executed.")]
[DataContract]
public sealed record TaskDescriptor
{

    /// <summary>
    /// Gets/sets the task's name
    /// </summary>
    [Description("The task's name.")]
    [DataMember(Order = 0, Name = "name"), JsonPropertyOrder(0), JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Gets/sets the task's reference
    /// </summary>
    [Description("The task's reference.")]
    [Required]
    [DataMember(Order = 1, Name = "reference"), JsonPropertyOrder(1), JsonPropertyName("reference")]
    public required JsonPointer Reference { get; init; }

    /// <summary>
    /// Gets/sets the task's definition
    /// </summary>
    [Description("The task's definition.")]
    [Required]
    [DataMember(Order = 2, Name = "definition"), JsonPropertyOrder(2), JsonPropertyName("definition")]
    public required TaskDefinition Definition { get; init; }

    /// <summary>
    /// Gets/sets the task's raw, untransformed input
    /// </summary>
    [Description("The task's raw, untransformed input.")]
    [DataMember(Order = 3, Name = "input"), JsonPropertyOrder(3), JsonPropertyName("input")]
    public JsonNode? Input { get; init; }

    /// <summary>
    /// Gets/sets the task's raw, untransformed output
    /// </summary>
    [Description("The task's raw, untransformed output.")]
    [DataMember(Order = 4, Name = "output"), JsonPropertyOrder(4), JsonPropertyName("output")]
    public JsonNode? Output { get; init; }

    /// <summary>
    /// Gets/sets the date and time at which the task has started
    /// </summary>
    [Description("The date and time at which the task has started.")]
    [DataMember(Order = 5, Name = "startedAt"), JsonPropertyOrder(5), JsonPropertyName("startedAt")]
    public DateTimeDescriptor? StartedAt { get; init; }

}
