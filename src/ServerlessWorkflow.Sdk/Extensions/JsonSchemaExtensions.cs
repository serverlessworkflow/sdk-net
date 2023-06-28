// Copyright © 2023-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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