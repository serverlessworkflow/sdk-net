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
/// Defines the fundamentals of a service used to build <see cref="InputDataModelDefinition"/>s
/// </summary>
public interface IInputDataModelDefinitionBuilder
{

    /// <summary>
    /// Configures the input data schema
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the input data schema</param>
    /// <returns>The configured <see cref="IInputDataModelDefinitionBuilder"/></returns>
    IInputDataModelDefinitionBuilder WithSchema(Action<ISchemaDefinitionBuilder> setup);

    /// <summary>
    /// Configures the runtime expression used to filter the input data
    /// </summary>
    /// <param name="expression">The runtime expression used to filter the input data</param>
    /// <returns>The configured <see cref="IInputDataModelDefinitionBuilder"/></returns>
    IInputDataModelDefinitionBuilder From(object expression);

    /// <summary>
    /// Builds the configured <see cref="InputDataModelDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="InputDataModelDefinition"/></returns>
    InputDataModelDefinition Build();

}
