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
/// Represents the default implementation of the <see cref="IOutputDataModelDefinitionBuilder"/> interface
/// </summary>
public sealed class OutputDataModelDefinitionBuilder
    : IOutputDataModelDefinitionBuilder
{

    OutputDataModelDefinition output = new();

    /// <inheritdoc/>
    public IOutputDataModelDefinitionBuilder As(OneOf<JsonObject, string> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);
        output = output with
        {
            As = expression
        };
        return this;
    }

    /// <inheritdoc/>
    public IOutputDataModelDefinitionBuilder WithSchema(Action<ISchemaDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new SchemaDefinitionBuilder();
        setup(builder);
        output = output with
        {
            Schema = builder.Build()
        };
        return this;
    }

    /// <inheritdoc/>
    public OutputDataModelDefinition Build() => output;

}