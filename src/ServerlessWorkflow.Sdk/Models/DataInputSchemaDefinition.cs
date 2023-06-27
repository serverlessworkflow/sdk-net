namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the object used to configure a workflow definition's data input schema
/// </summary>
[DataContract]
public class DataInputSchemaDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the url of the workflow definition's input data schema
    /// </summary>
    [DataMember(Order = 1, Name = "schema"), JsonPropertyName("schema"), YamlMember(Alias = "schema")]
    [JsonConverter(typeof(OneOfConverter<JsonSchema, Uri>))]
    public virtual OneOf<JsonSchema, Uri>? Schema { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to terminate the workflow definition's execution whenever the validation of the input data fails. Defaults to true.
    /// </summary>
    [DefaultValue(true)]
    [DataMember(Order = 2, Name = "failOnValidationErrors"), JsonPropertyName("failOnValidationErrors"), YamlMember(Alias = "failOnValidationErrors")]
    public virtual bool FailOnValidationErrors { get; set; } = true;

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

}