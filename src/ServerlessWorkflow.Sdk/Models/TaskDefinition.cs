namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a task.
/// </summary>
[Description("Represents the definition of a task.")]
[DataContract, JsonConverter(typeof(TaskDefinitionJsonConverter))]
public abstract record TaskDefinition
    : ComponentDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to determine whether or not the execute the task in the current context
    /// </summary>
    [Description("A runtime expression, if any, used to determine whether or not the execute the task in the current context")]
    [DataMember(Order = 0, Name = "if"), JsonPropertyOrder(0), JsonPropertyName("if")]
    public string? If { get; init; }

    /// <summary>
    /// Gets/sets the definition, if any, of the task's input data
    /// </summary>
    [Description("The definition, if any, of the task's input data")]
    [DataMember(Order = 90, Name = "input"), JsonPropertyOrder(90), JsonPropertyName("input")]
    public InputDataModelDefinition? Input { get; init; }

    /// <summary>
    /// Gets/sets the definition, if any, of the task's output data
    /// </summary>
    [Description("The definition, if any, of the task's output data")]
    [DataMember(Order = 91, Name = "output"), JsonPropertyOrder(91), JsonPropertyName("output")]
    public OutputDataModelDefinition? Output { get; init; }

    /// <summary>
    /// Gets/sets the optional configuration for exporting data within the task's context
    /// </summary>
    [Description("The optional configuration for exporting data within the task's context")]
    [DataMember(Order = 92, Name = "export"), JsonPropertyOrder(92), JsonPropertyName("export")]
    public OutputDataModelDefinition? Export { get; init; }

    /// <summary>
    /// Gets/sets the task's timeout, if any
    /// </summary>
    [Description("The task's timeout, if any")]
    [DataMember(Order = 93, Name = "timeout"), JsonPropertyOrder(93), JsonPropertyName("timeout"), JsonConverter(typeof(OneOfJsonConverter<TimeoutDefinition, string>))]
    protected OneOf<TimeoutDefinition, string>? Timeout { get; init; }

    /// <summary>
    /// Gets/sets the flow directive to be performed upon completion of the task
    /// </summary>
    [Description("The flow directive to be performed upon completion of the task")]
    [DataMember(Order = 94, Name = "then"), JsonPropertyOrder(94), JsonPropertyName("then")]
    public string? Then { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of additional information associated with the task
    /// </summary>
    [Description("A key/value mapping of additional information associated with the task")]
    [DataMember(Order = 95, Name = "metadata"), JsonPropertyOrder(95), JsonPropertyName("metadata")]
    public JsonObject? Metadata { get; init; }

}
