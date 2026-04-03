using Newtonsoft.Json;
using System.Xml;
using System.Xml.Schema;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the <see cref="ISchemaHandler"/> implementation used to handle XML schemas
/// </summary>
/// <param name="externalResourceReader">The service used to reader external resources</param>
public sealed class XmlSchemaHandler(IExternalResourceReader externalResourceReader)
    : ISchemaHandler
{

    /// <inheritdoc/>
    public bool Supports(string format) => format.Equals(SchemaFormat.Xml, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public async Task<ISchemaValidationResult> ValidateAsync(JsonObject graph, SchemaDefinition schema, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(schema);
        if (!Supports(schema.Format)) throw new NotSupportedException($"The specified schema format '{schema.Format}' is not supported in this context");
        var xml = string.Empty;
        XmlSchema xmlSchema;
        if (schema.Resource is null)
        {
            xml = schema.Document?.Match
            (
                jsonObject => throw new InvalidOperationException("Document-based schemas are not supported for XML schema validation"),
                str => str
            );
            if (string.IsNullOrWhiteSpace(xml)) throw new  NullReferenceException("The specified schema does not contain a valid XML schema definition");
            using var reader = new StringReader(xml);
            xmlSchema = XmlSchema.Read(reader, OnValidationError)!;
        }
        else
        {
            using var stream = await externalResourceReader.ReadAsync(schema.Resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            xmlSchema = XmlSchema.Read(stream, OnValidationError)!;
        }
        var settings = new XmlReaderSettings();
        settings.Schemas.Add(xmlSchema);
        settings.ValidationType = ValidationType.Schema;
        var json = System.Text.Json.JsonSerializer.Serialize(graph, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonObject);
        var xmlDocument = JsonConvert.DeserializeXmlNode(json)!;
        xml = xmlDocument.OuterXml;
        using var stringReader = new StringReader(xml);
        using var xmlReader = XmlReader.Create(stringReader, settings);
        var validationErrors = new List<string>();
        settings.ValidationEventHandler += (sender, args) => validationErrors.Add(args.Message);
        try { while (xmlReader.Read()) { } }
        catch (XmlException ex) { validationErrors.Add(ex.Message); }
        if (validationErrors.Count != 0) return SchemaValidationResult.Failed(validationErrors.GroupBy(e => e).Select(e => new KeyValuePair<string, IEnumerable<string>>(e.Key, [e.Key])));
        return SchemaValidationResult.Succeeded();
    }


    void OnValidationError(object? sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error) throw new XmlSchemaValidationException(e.Message, e.Exception, e.Exception.LineNumber, e.Exception.LinePosition);
    }

}