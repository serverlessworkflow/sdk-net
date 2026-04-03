namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to describe an HTTP response
/// </summary>
[Description("Represents an object used to describe an HTTP response.")]
[DataContract]
public sealed record HttpResponse
{

    /// <summary>
    /// Gets/sets the HTTP request associated with the HTTP response
    /// </summary>
    [Description("The HTTP request associated with the HTTP response.")]
    [Required]
    [DataMember(Order = 1, Name = "request"), JsonPropertyOrder(1), JsonPropertyName("request")]
    public required HttpRequest Request { get; init; }

    /// <summary>
    /// Gets/sets the HTTP response's status code
    /// </summary>
    [Description("The HTTP response's status code.")]
    [Required]
    [DataMember(Order = 2, Name = "statusCode"), JsonPropertyOrder(2), JsonPropertyName("statusCode")]
    public required int StatusCode { get; init; }

    /// <summary>
    /// Gets/sets the response headers, if any
    /// </summary>
    [Description("The response headers, if any.")]
    [DataMember(Order = 3, Name = "headers"), JsonPropertyOrder(3), JsonPropertyName("headers")]
    public EquatableDictionary<string, string>? Headers { get; init; }

    /// <summary>
    /// Gets/sets the HTTP response's content, if any
    /// </summary>
    [Description("The HTTP response's content, if any.")]
    [DataMember(Order = 4, Name = "content"), JsonPropertyOrder(4), JsonPropertyName("content")]
    public JsonNode? Content { get; init; }

}
