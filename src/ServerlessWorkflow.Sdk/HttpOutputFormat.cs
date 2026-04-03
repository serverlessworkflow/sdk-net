namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes all supported HTTP call output formats
/// </summary>
public static class HttpOutputFormat
{

    /// <summary>
    /// Indicates that the HTTP call should output the HTTP response's raw content
    /// </summary>
    public const string Raw = "raw";
    /// <summary>
    /// Indicates that the HTTP call should output the HTTP response's content, possibly deserialized
    /// </summary>
    public const string Content = "content";
    /// <summary>
    /// Indicates that the HTTP call should output an <see cref="HttpResponse"/>
    /// </summary>
    public const string Response = "response";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Raw;
        yield return Content;
        yield return Response;
    }

}
