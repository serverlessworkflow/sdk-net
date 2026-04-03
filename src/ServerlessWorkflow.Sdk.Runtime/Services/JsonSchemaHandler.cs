namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the <see cref="ISchemaHandler"/> implementation used to handle JSON schemas
/// </summary>
/// <param name="externalResourceReader">The service used to read external resources</param>
public sealed class JsonSchemaHandler(IExternalResourceReader externalResourceReader)
    : ISchemaHandler
{

    /// <inheritdoc/>
    public bool Supports(string format) => format.Equals(SchemaFormat.Json, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public async Task<ISchemaValidationResult> ValidateAsync(JsonNode graph, SchemaDefinition schema, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(schema);
        if (!Supports(schema.Format)) throw new NotSupportedException($"The specified schema format '{schema.Format}' is not supported in this context");
        var json = string.Empty;
        if (schema.Resource is null)
        {
            if (schema.Document is null) throw new InvalidOperationException("The specified schema definition does not contain a valid resource reference or an embedded document");
            json = schema.Document.Match
            (
                jsonObject => JsonSerializer.Serialize(schema.Document, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonObject),
                str => str
            );
        }
        else
        {
            using var stream = await externalResourceReader.ReadAsync(schema.Resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            using var streamReader = new StreamReader(stream);
            json = await streamReader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
        }
        var jsonSchema = JsonSchema.FromText(json);
        var jsonDocument = JsonSerializer.SerializeToElement(graph, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonObject)!;
        var options = new EvaluationOptions()
        {
            OutputFormat = OutputFormat.List
        };
        var results = jsonSchema.Evaluate(jsonDocument, options);
        if (results.IsValid) return SchemaValidationResult.Succeeded();
        return SchemaValidationResult.Failed(results.Details?.Where(d => d.Errors is not null).SelectMany(d => d.Errors!).GroupBy(e => e.Key).Select(e => new KeyValuePair<string, IEnumerable<string>>(e.Key, e.Select(e => e.Value))) ?? []);
    }

}
