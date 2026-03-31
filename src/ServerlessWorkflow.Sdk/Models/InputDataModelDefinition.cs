namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an input data model
/// </summary>
[Description("Represents the definition of an input data model")]
[DataContract]
public sealed record InputDataModelDefinition
{

    /// <summary>
    /// Gets/sets the schema, if any, that defines and describes the input data of a workflow or task
    /// </summary>
    [Description("The schema, if any, that defines and describes the input data of a workflow or task")]
    [DataMember(Order = 1, Name = "schema"), JsonPropertyOrder(1), JsonPropertyName("schema")]
    public SchemaDefinition? Schema { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to build the workflow or task input data based on both input and scope data
    /// </summary>
    [Description("A runtime expression, if any, used to build the workflow or task input data based on both input and scope data")]
    [DataMember(Order = 2, Name = "from"), JsonPropertyOrder(2), JsonPropertyName("from"), JsonConverter(typeof(OneOfJsonConverter<JsonObject, string>))]
    public OneOf<JsonObject, string>? From { get; init; }

}
