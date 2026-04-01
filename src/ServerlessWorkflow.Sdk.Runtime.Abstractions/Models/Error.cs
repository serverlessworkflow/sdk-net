namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents an object used to describe an error or problem, as defined by <see href="https://www.rfc-editor.org/rfc/rfc7807">RFC 7807</see>
/// </summary>
[Description("Represents an object used to describe an error or problem, as defined by RFC 7807")]
[DataContract]
public sealed record Error
{

    /// <summary>
    /// Gets/sets an uri that reference the type of the described problem.
    /// </summary>
    [Description("An uri that reference the type of the described problem.")]
    [DataMember(Order = 1, Name = "type"), JsonPropertyOrder(1), JsonPropertyName("type")]
    public required Uri Type { get; init; }

    /// <summary>
    /// Gets/sets a short, human-readable summary of the problem type.It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.
    /// </summary>
    [Description("A short, human-readable summary of the problem type.It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.")]
    [DataMember(Order = 2, Name = "title"), JsonPropertyOrder(2), JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>
    /// Gets/sets the status code produced by the described problem
    /// </summary>
    [Description("The status code produced by the described problem")]
    [DataMember(Order = 3, Name = "status"), JsonPropertyOrder(3), JsonPropertyName("status")]
    public required ushort Status { get; init; }

    /// <summary>
    /// Gets/sets a human-readable explanation specific to this occurrence of the problem.
    /// </summary>
    [Description("A human-readable explanation specific to this occurrence of the problem.")]
    [DataMember(Order = 4, Name = "detail"), JsonPropertyOrder(4), JsonPropertyName("detail")]
    public string? Detail { get; init; }

    /// <summary>
    /// Gets/sets a <see cref="Uri"/> reference that identifies the specific occurrence of the problem. It may or may not yield further information if dereferenced.
    /// </summary>
    [Description("A Uri reference that identifies the specific occurrence of the problem. It may or may not yield further information if dereferenced.")]
    [DataMember(Order = 5, Name = "instance"), JsonPropertyOrder(5), JsonPropertyName("instance")]
    public Uri? Instance { get; init; }

    /// <summary>
    /// Gets/sets a mapping containing problem details extension data, if any
    /// </summary>
    [Description("A mapping containing problem details extension data, if any")]
    [DataMember(Name = "extensionData", Order = 6), JsonExtensionData]
    public IDictionary<string, JsonElement>? ExtensionData { get; init; }

    /// <summary>
    /// Creates a new communication <see cref="Error"/>
    /// </summary>
    /// <param name="instance">The <see cref="Error"/> source</param>
    /// <param name="status">The <see cref="Error"/>'s status</param>
    /// <param name="detail">The <see cref="Error"/> detail, if any</param>
    /// <returns>A new communication <see cref="Error"/></returns>
    public static Error Communication(Uri instance, ushort status = ErrorStatus.Communication, string? detail = null) => new()
    {
        Status = status,
        Type = ErrorType.Communication,
        Title = ErrorTitle.Communication,
        Detail = detail,
        Instance = instance
    };

    /// <summary>
    /// Creates a new communication <see cref="Error"/>
    /// </summary>
    /// <param name="instance">The <see cref="Error"/> source</param>
    /// <param name="detail">The <see cref="Error"/> detail, if any</param>
    /// <returns>A new communication <see cref="Error"/></returns>
    public static Error Configuration(Uri instance, string? detail = null) => new()
    {
        Status = ErrorStatus.Configuration,
        Type = ErrorType.Configuration,
        Title = ErrorTitle.Configuration,
        Detail = detail,
        Instance = instance
    };

    /// <summary>
    /// Creates a new runtime <see cref="Error"/>
    /// </summary>
    /// <param name="instance">The <see cref="Error"/> source</param>
    /// <param name="detail">The <see cref="Error"/> detail, if any</param>
    /// <returns>A new communication <see cref="Error"/></returns>
    public static Error Runtime(Uri instance, string? detail = null) => new()
    {
        Status = ErrorStatus.Runtime,
        Type = ErrorType.Runtime,
        Title = ErrorTitle.Runtime,
        Detail = detail,
        Instance = instance
    };

    /// <summary>
    /// Creates a new validation <see cref="Error"/>
    /// </summary>
    /// <param name="instance">The <see cref="Error"/> source</param>
    /// <param name="detail">The <see cref="Error"/> detail, if any</param>
    /// <returns>A new communication <see cref="Error"/></returns>
    public static Error Validation(Uri instance, string? detail = null) => new()
    {
        Status = ErrorStatus.Validation,
        Type = ErrorType.Validation,
        Title = ErrorTitle.Validation,
        Detail = detail,
        Instance = instance
    };

}
