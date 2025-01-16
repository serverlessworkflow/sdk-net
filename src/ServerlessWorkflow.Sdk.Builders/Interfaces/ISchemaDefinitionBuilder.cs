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
/// Defines the fundamentals of a service used to build <see cref="SchemaDefinition"/>s
/// </summary>
public interface ISchemaDefinitionBuilder
{

    /// <summary>
    /// Sets the schema format
    /// </summary>
    /// <param name="format">The schema format</param>
    /// <returns>The configured <see cref="ISchemaDefinitionBuilder"/></returns>
    ISchemaDefinitionBuilder WithFormat(string format);

    /// <summary>
    /// Sets the schema's <see cref="ExternalResourceDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the schema's <see cref="ExternalResourceDefinition"/></param>
    /// <returns>The configured <see cref="ISchemaDefinitionBuilder"/></returns>
    ISchemaDefinitionBuilder WithResource(Action<IExternalResourceDefinitionBuilder> setup);

    /// <summary>
    /// Sets the schema document
    /// </summary>
    /// <param name="document">The schema document</param>
    /// <returns>The configured <see cref="ISchemaDefinitionBuilder"/></returns>
    ISchemaDefinitionBuilder WithDocument(object document);

    /// <summary>
    /// Builds the configured <see cref="SchemaDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="SchemaDefinition"/></returns>
    SchemaDefinition Build();

}
