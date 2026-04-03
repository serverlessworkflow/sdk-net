namespace ServerlessWorkflow.Sdk.Models.Processes;

/// <summary>
/// Represents the definition of a (sub)workflow process
/// </summary>
[DataContract]
public sealed record WorkflowProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the namespace the workflow to run belongs to
    /// </summary>
    [Description("The namespace the workflow to run belongs to")]
    [Required, StringLength(63, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "namespace"), JsonPropertyOrder(1), JsonPropertyName("namespace")]
    public required string Namespace { get; init; }

    /// <summary>
    /// Gets/sets the name of the workflow to run
    /// </summary>
    [Description("The name of the workflow to run")]
    [Required, StringLength(63, MinimumLength = 1)]
    [DataMember(Order = 2, Name = "name"), JsonPropertyOrder(2), JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets/sets the version of the workflow to run. Defaults to `latest`
    /// </summary>
    [Description("The version of the workflow to run. Defaults to `latest`")]
    [SemanticVersion]
    [DataMember(Order = 3, Name = "version"), JsonPropertyOrder(3), JsonPropertyName("version")]
    public string Version { get; init; } = "latest";

    /// <summary>
    /// Gets/sets the data, if any, to pass as input to the workflow to execute. The value should be validated against the target workflow's input schema, if specified
    /// </summary>
    [Description("The data, if any, to pass as input to the workflow to execute. The value should be validated against the target workflow's input schema, if specified")]
    [DataMember(Order = 4, Name = "input"), JsonPropertyOrder(4), JsonPropertyName("input")]
    public JsonObject? Input { get; init; }

}