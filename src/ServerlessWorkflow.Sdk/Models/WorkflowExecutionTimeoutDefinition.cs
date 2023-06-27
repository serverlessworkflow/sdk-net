using ServerlessWorkflow.Sdk.Serialization.Json;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to define the execution timeout for a workflow instance
/// </summary>
[DataContract]
public class WorkflowExecutionTimeoutDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the duration after which the workflow's execution will time out
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "duration", IsRequired = true), JsonPropertyName("duration"), YamlMember(Alias = "duration")]
    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public virtual TimeSpan Duration { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to terminate the workflow execution. Defaults to true.
    /// </summary>
    [DefaultValue(true)]
    [DataMember(Order = 2, Name = "interrupt", IsRequired = true), JsonPropertyName("interrupt"), YamlMember(Alias = "interrupt")]
    public virtual bool Interrupt { get; set; } = true;

    /// <summary>
    /// Gets/sets the name of a workflow state to be executed before workflow instance is terminated
    /// </summary>
    [DataMember(Order = 3, Name = "runBefore", IsRequired = true), JsonPropertyName("runBefore"), YamlMember(Alias = "runBefore")]
    public virtual string? RunBefore { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 4, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

}