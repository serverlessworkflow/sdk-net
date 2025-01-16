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
public class OutputDataModelDefinitionBuilder
    : IOutputDataModelDefinitionBuilder
{

    /// <summary>
    /// Gets the <see cref="OutputDataModelDefinition"/> to configure
    /// </summary>
    protected OutputDataModelDefinition Output { get; } = new();

    /// <inheritdoc/>
    public virtual IOutputDataModelDefinitionBuilder As(object expression)
    {
        ArgumentNullException.ThrowIfNull(expression);
        this.Output.As = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOutputDataModelDefinitionBuilder WithSchema(Action<ISchemaDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new SchemaDefinitionBuilder();
        setup(builder);
        this.Output.Schema = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual OutputDataModelDefinition Build() => this.Output;

}