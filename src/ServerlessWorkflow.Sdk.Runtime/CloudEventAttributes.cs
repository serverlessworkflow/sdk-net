namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Exposes constants about <see cref="ICloudEvent"/> attributes
/// </summary>
public static class CloudEventAttributes
{

    /// <summary>
    /// Identifies the event
    /// </summary>
    public const string Id = "id";
    /// <summary>
    /// Identifies the context in which an event happened
    /// </summary>
    public const string SpecVersion = "specversion";
    /// <summary>
    /// Timestamp of when the occurrence happened
    /// </summary>
    public const string Time = "time";
    /// <summary>
    /// Identifies the context in which an event happened
    /// </summary>
    public const string Source = "source";
    /// <summary>
    /// Describes the type of event related to the originating occurrence
    /// </summary>
    public const string Type = "type";
    /// <summary>
    /// Describes the subject of the event in the context of the event producer (identified by source)
    /// </summary>
    public const string Subject = "subject";
    /// <summary>
    /// Content type of data value
    /// </summary>
    public const string DataContentType = "datacontenttype";
    /// <summary>
    /// Identifies the schema that data adheres to
    /// </summary>
    public const string DataSchema = "dataschema";
    /// <summary>
    /// The event payload. Only used when events are formatted using the structured mode
    /// </summary>
    public const string Data = "data";
    /// <summary>
    /// The event payload, encoded in base 64. Only used when events are formatted using the binary mode
    /// </summary>
    public const string DataBase64 = "data_base64";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> that contains all required <see cref="CloudEvent"/> attributes
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> that contains all required <see cref="CloudEvent"/> attributes</returns>
    public static IEnumerable<string> GetRequiredAttributes()
    {
        yield return Id;
        yield return SpecVersion;
        yield return Source;
        yield return Type;
    }

}