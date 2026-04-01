namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a an extension
/// </summary>
[Description("Represents the definition of a an extension.")]
[DataContract]
public sealed record ExtensionDefinition
    : Extendable
{

    /// <summary>
    /// Gets/sets the type of task to extend
    /// </summary>
    [Description("The type of task to extend.")]
    [Required]
    [DataMember(Order = 1, Name = "extend"), JsonPropertyOrder(1), JsonPropertyName("extend")]
    public required string Extend { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to determine whether or not the extension should apply in the specified context
    /// </summary>
    [Description("A runtime expression, if any, used to determine whether or not the extension should apply in the specified context.")]
    [DataMember(Order = 2, Name = "when"), JsonPropertyOrder(2), JsonPropertyName("when")]
    public string? When { get; init; }

    /// <summary>
    /// Gets/sets a name/definition map of the tasks to execute before the extended task, if any
    /// </summary>
    [Description("A name/definition map of the tasks to execute before the extended task, if any.")]
    [DataMember(Order = 3, Name = "before"), JsonPropertyOrder(3), JsonPropertyName("before")]
    public Map<string, TaskDefinition>? Before { get; init; }

    /// <summary>
    /// Gets/sets a name/definition map of the tasks to execute after the extended task, if any
    /// </summary>
    [Description("A name/definition map of the tasks to execute after the extended task, if any.")]
    [DataMember(Order = 4, Name = "after"), JsonPropertyOrder(4), JsonPropertyName("after")]
    public Map<string, TaskDefinition>? After { get; init; }

}