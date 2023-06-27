using System.Reflection;

namespace ServerlessWorkflow.Sdk;

public static class JsonSchemaExtensions
{

    static readonly MethodInfo JsonSchemaInitializeMethod = typeof(JsonSchema).GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).Single(m => m.Name == "Initialize");
    static readonly MethodInfo JsonSchemaGetSubschemasMethod = typeof(JsonSchema).GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).Single(m => m.Name == "GetSubschemas");

    public static JsonSchema Bundle(this JsonSchema jsonSchema)
    {
        var options = EvaluationOptions.From(EvaluationOptions.Default);
        JsonSchemaInitializeMethod.Invoke(null, new object[] { jsonSchema, options.SchemaRegistry, null! });
        var schemasToSearch = new List<JsonSchema>();
        var externalSchemas = new Dictionary<string, JsonSchema>();
        var bundledReferences = new List<Uri>();
        var referencesToCheck = new List<Uri> { jsonSchema.BaseUri };

        while (referencesToCheck.Count != 0)
        {
            var nextReference = referencesToCheck[0];
            referencesToCheck.RemoveAt(0);
            var resolved = options.SchemaRegistry.Get(nextReference) ?? throw new JsonSchemaException($"Cannot resolve reference: '{nextReference}'");
            if (resolved is not JsonSchema resolvedSchema) throw new NotSupportedException("Bundling is not supported for non-schema root documents");
            JsonSchemaInitializeMethod.Invoke(null, new object[] { resolvedSchema, options.SchemaRegistry, null! });
            var schemaId = new FileInfo(resolvedSchema.GetId()!.AbsolutePath).Name.Split('.').First();
            if (!bundledReferences.Contains(nextReference) && !externalSchemas.ContainsKey(schemaId)) externalSchemas.Add(schemaId, resolvedSchema);
            schemasToSearch.Add(resolvedSchema);
            while (schemasToSearch.Count != 0)
            {
                var schema = schemasToSearch[0];
                schemasToSearch.RemoveAt(0);

                if (schema.Keywords == null) continue;

                schemasToSearch.AddRange(schema.Keywords.SelectMany(k => (IEnumerable<JsonSchema>)JsonSchemaGetSubschemasMethod.Invoke(null, new object[] { k })!));

                if (schema.BaseUri != nextReference && !bundledReferences.Contains(schema.BaseUri))
                    bundledReferences.Add(schema.BaseUri);

                var reference = schema.GetRef();
                if (reference != null)
                {
                    var newUri = new Uri(schema.BaseUri, reference);
                    if (newUri == schema.BaseUri) continue; // same document

                    referencesToCheck.Add(newUri);
                }
            }
        }

        return new JsonSchemaBuilder()
            .Id(jsonSchema.BaseUri.OriginalString + "(bundled)")
            .Defs(externalSchemas)
            .Ref(jsonSchema.BaseUri);
    }

}