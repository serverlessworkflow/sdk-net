namespace ServerlessWorkflow.Sdk.Models.Calls;

/// <summary>
/// Represents the definition of an OpenAPI call
/// </summary>
[Description("Represents the definition of an OpenAPI call")]
[DataContract]
public sealed record OpenApiCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets/sets the document that defines the OpenAPI operation to call
    /// </summary>
    [Description("The document that defines the OpenAPI operation to call")]
    [Required]
    [DataMember(Order = 1, Name = "document"), JsonPropertyOrder(1), JsonPropertyName("document")]
    public required ExternalResourceDefinition Document { get; set; }

    /// <summary>
    /// Gets/sets the id of the OpenAPI operation to call
    /// </summary>
    [Description("The id of the OpenAPI operation to call")]
    [Required]
    [DataMember(Order = 2, Name = "operationId"), JsonPropertyOrder(2), JsonPropertyName("operationId")]
    public required string OperationId { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the parameters of the OpenAPI operation to call
    /// </summary>
    [Description("A name/value mapping of the parameters of the OpenAPI operation to call")]
    [DataMember(Order = 3, Name = "parameters"), JsonPropertyOrder(3), JsonPropertyName("parameters")]
    public JsonObject? Parameters { get; set; }

    /// <summary>
    /// Gets/sets the authentication policy, if any, to use when calling the OpenAPI operation
    /// </summary>
    [Description("The authentication policy, if any, to use when calling the OpenAPI operation")]
    [DataMember(Order = 4, Name = "authentication"), JsonPropertyOrder(4), JsonPropertyName("authentication")]
    public AuthenticationPolicyDefinition? Authentication { get; set; }

    /// <summary>
    /// Gets/sets the http output format. Defaults to <see cref="HttpOutputFormat.Content"/>.
    /// </summary>
    [Description("The http output format. Defaults to HttpOutputFormat.Content.")]
    [DataMember(Order = 5, Name = "output"), JsonPropertyOrder(5), JsonPropertyName("output")]
    public string? Output { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether redirection status codes (300–399) should be treated as errors.<para></para>
    /// If set to 'false', runtimes must raise an error for response status codes outside the 200–299 range.<para></para>
    /// If set to 'true', they must raise an error for status codes outside the 200–399 range.
    /// </summary>
    [Description("A boolean indicating whether redirection status codes (300–399) should be treated as errors. If set to 'false', runtimes must raise an error for response status codes outside the 200–299 range. If set to 'true', they must raise an error for status codes outside the 200–399 range.")]
    [DataMember(Order = 6, Name = "redirect"), JsonPropertyOrder(6), JsonPropertyName("redirect")]
    public bool Redirect { get; set; }

}