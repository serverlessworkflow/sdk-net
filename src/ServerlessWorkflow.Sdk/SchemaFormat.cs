namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes all schema formats supported by default by ServerlessWorkflow
/// </summary>
public static class SchemaFormat
{

    /// <summary>
    /// Gets the Avro schema format
    /// </summary>
    public const string Avro = "avro";
    /// <summary>
    /// Gets the JSON schema format
    /// </summary>
    public const string Json = "json";
    /// <summary>
    /// Gets the XML schema format
    /// </summary>
    public const string Xml = "xml";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all default schema format
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all default schema format
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all default schema format</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Avro;
        yield return Json;
        yield return Xml;
    }

}
