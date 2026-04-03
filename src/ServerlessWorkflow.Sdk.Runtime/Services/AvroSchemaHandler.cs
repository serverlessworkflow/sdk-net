using Avro;
using Avro.Generic;
using Avro.IO;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the <see cref="ISchemaHandler"/> implementation used to handle Avro schemas
/// </summary>
/// <param name="externalResourceReader">The service used to read external resources</param>
public sealed class AvroSchemaHandler(IExternalResourceReader externalResourceReader)
    : ISchemaHandler
{

    /// <inheritdoc/>
    public bool Supports(string format) => format.Equals(SchemaFormat.Avro, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public async Task<ISchemaValidationResult> ValidateAsync(JsonObject graph, SchemaDefinition schema, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(schema);
        if (!Supports(schema.Format)) throw new NotSupportedException($"The specified schema format '{schema.Format}' is not supported in this context");
        Schema avroSchema;
        try
        {
            var json = string.Empty;
            if (schema.Resource == null)
            {
                if (schema.Document == null) throw new InvalidOperationException("The specified schema definition does not contain a valid resource reference or an embedded document");
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
            avroSchema = Schema.Parse(json);
        }
        catch (Exception ex)
        {
            return SchemaValidationResult.Failed(new Dictionary<string, IReadOnlyList<string>>()
            {
                [string.Empty] = [$"An error occurred while parsing the specified schema: {ex}"]
            });
        }
        byte[] avroData;
        try
        {
            using var memoryStream = new MemoryStream();
            var writer = new BinaryEncoder(memoryStream);
            var datumWriter = new GenericDatumWriter<object>(avroSchema);
            var avroObject = ConvertToAvroCompatible(graph, avroSchema);
            datumWriter.Write(avroObject!, writer);
            avroData = memoryStream.ToArray();
        }
        catch (Exception ex)
        {
            return SchemaValidationResult.Failed(new Dictionary<string, IReadOnlyList<string>>()
            {
                [string.Empty] = [$"An error occurred while serializing the specified data to Avro: {ex}"]
            });
        }
        try
        {
            using var memoryStream = new MemoryStream(avroData);
            var reader = new BinaryDecoder(memoryStream);
            var datumReader = new GenericDatumReader<GenericRecord>(avroSchema, avroSchema);
            datumReader.Read(null!, reader);
            return SchemaValidationResult.Succeeded();
        }
        catch (Exception ex)
        {
            return SchemaValidationResult.Failed(new Dictionary<string, IReadOnlyList<string>>()
            {
                [string.Empty] = [ex.Message]
            });
        }
    }

    static object? ConvertToAvroCompatible(JsonNode? graph, Schema schema)
    {
        if (graph is null) return null;
        return schema switch
        {
            RecordSchema recordSchema => ConvertToGenericRecord(graph, recordSchema),
            ArraySchema arraySchema => ConvertToArray(graph, arraySchema),
            MapSchema mapSchema => ConvertToMap(graph, mapSchema),
            UnionSchema unionSchema => ConvertToUnion(graph, unionSchema),
            FixedSchema or EnumSchema or PrimitiveSchema => JsonSerializer.Deserialize<object>(graph),
            _ => throw new NotSupportedException($"Unsupported schema type: {schema.GetType().Name}")
        };
    }

    static GenericRecord? ConvertToGenericRecord(JsonNode? graph, RecordSchema recordSchema)
    {
        if (graph is null || graph is not JsonObject jsonObject) return null;
        var record = new GenericRecord(recordSchema);
        foreach (var field in recordSchema.Fields) if (jsonObject.TryGetPropertyValue(field.Name, out var value)) record.Add(field.Name, ConvertToAvroCompatible(value, field.Schema));
        return record;
    }

    static List<object?>? ConvertToArray(JsonNode? graph, ArraySchema arraySchema)
    {
        if (graph is null || graph is not JsonArray jsonArray) return null;
        return [.. jsonArray.Select(item => ConvertToAvroCompatible(item, arraySchema.ItemSchema))];
    }

    static Dictionary<string, object?>? ConvertToMap(JsonNode? graph, MapSchema mapSchema)
    {
        if (graph is null || graph is not JsonObject jsonObject) return null;
        return jsonObject.ToDictionary(kvp => kvp.Key, kvp => ConvertToAvroCompatible(kvp.Value, mapSchema.ValueSchema));
    }

    static object? ConvertToUnion(JsonNode? graph, UnionSchema unionSchema)
    {
        foreach (var schema in unionSchema.Schemas)
        {
            try { return ConvertToAvroCompatible(graph, schema); }
            catch { }
        }
        throw new InvalidOperationException("Provided object does not match any schema in the union.");
    }

}
