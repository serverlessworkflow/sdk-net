// Copyright © 2024-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ISchemaDefinitionBuilder"/> interface
/// </summary>
public sealed class SchemaDefinitionBuilder
    : ISchemaDefinitionBuilder
{

    SchemaDefinition schema = new();

    /// <inheritdoc/>
    public ISchemaDefinitionBuilder WithFormat(string format)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(format);
        schema = schema with
        {
            Format = format
        };
        return this;
    }

    /// <inheritdoc/>
    public ISchemaDefinitionBuilder WithResource(Action<IExternalResourceDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ExternalResourceDefinitionBuilder();
        setup(builder);
        schema = schema with
        {
            Resource = builder.Build()
        };
        return this;
    }

    /// <inheritdoc/>
    public ISchemaDefinitionBuilder WithDocument(JsonObject document)
    {
        ArgumentNullException.ThrowIfNull(document);
        schema = schema with
        {
            Document = document
        };
        return this;
    }

    /// <inheritdoc/>
    public SchemaDefinition Build() => schema;

}
