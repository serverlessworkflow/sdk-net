namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of an extension's task
/// </summary>
[Description("Represents the definition of an extension's task")]
[DataContract]
public sealed record ExtensionTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Extension;

    /// <summary>
    /// Gets/sets the task definition's extension data, if any
    /// </summary>
    [Description("The task definition's extension data, if any")]
    [DataMember(Order = 1, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, JsonElement>? ExtensionData { get; set; }

}