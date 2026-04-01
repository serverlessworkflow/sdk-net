namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of the error to raise
/// </summary>
[Description("Represents the definition of the error to raise")]
[DataContract]
public sealed record RaiseErrorDefinition
{

    /// <summary>
    /// Gets/sets the error to raise
    /// </summary>
    [Description("The error to raise")]
    [Required]
    [DataMember(Order = 1, Name = "error"), JsonPropertyOrder(1), JsonPropertyName("error"), JsonConverter(typeof(OneOfJsonConverter<ErrorDefinition, string>))]
    public required OneOf<ErrorDefinition, string> Error { get; init; }

}
