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
/// Represents the default implementation of the <see cref="IInputDataModelDefinitionBuilder"/> interface
/// </summary>
public class InputDataModelDefinitionBuilder
    : IInputDataModelDefinitionBuilder
{

    /// <summary>
    /// Gets the <see cref="InputDataModelDefinition"/> to configure
    /// </summary>
    protected InputDataModelDefinition Input { get; } = new();

    /// <inheritdoc/>
    public virtual IInputDataModelDefinitionBuilder From(object expression)
    {
        ArgumentNullException.ThrowIfNull(expression);
        this.Input.From = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IInputDataModelDefinitionBuilder WithSchema(Action<ISchemaDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new SchemaDefinitionBuilder();
        setup(builder);
        this.Input.Schema = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual InputDataModelDefinition Build() => this.Input;

}
