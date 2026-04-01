namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an output data model
/// </summary>
[Description("Represents the definition of an output data model")]
[DataContract]
public sealed record OutputDataModelDefinition
{

    /// <summary>
    /// Gets/sets the schema, if any, that defines and describes the output data of a workflow or task
    /// </summary>
    [Description("The schema, if any, that defines and describes the output data of a workflow or task")]
    [DataMember(Order = 1, Name = "schema"), JsonPropertyOrder(1), JsonPropertyName("schema")]
    public SchemaDefinition? Schema { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to output specific data to the scope data
    /// </summary>
    [Description("A runtime expression, if any, used to output specific data to the scope data")]
    [DataMember(Order = 2, Name = "as"), JsonPropertyOrder(2), JsonPropertyName("as"), JsonConverter(typeof(OneOfJsonConverter<JsonObject, string>))]
    public OneOf<JsonObject, string>? As { get; init; }

}