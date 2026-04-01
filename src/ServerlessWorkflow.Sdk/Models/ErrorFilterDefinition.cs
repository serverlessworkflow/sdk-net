namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition an an error filter
/// </summary>
[Description("Represents the definition an an error filter")]
[DataContract]
public sealed record ErrorFilterDefinition
{

    /// <summary>
    /// Gets/sets a key/value mapping of the properties errors to filter must define
    /// </summary>
    [Description("A key/value mapping of the properties errors to filter must define")]
    [DataMember(Order = 1, Name = "with"), JsonPropertyOrder(1), JsonPropertyName("with")]
    public JsonObject? With { get; init; }

}
