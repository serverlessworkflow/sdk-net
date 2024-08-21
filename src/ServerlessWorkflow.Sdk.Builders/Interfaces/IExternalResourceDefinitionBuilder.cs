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
/// Defines the fundamentals of a service used to build <see cref="ExternalResourceDefinition"/>s
/// </summary>
public interface IExternalResourceDefinitionBuilder
{

    /// <summary>
    /// Configures the name of the referenced external resource
    /// </summary>
    /// <param name="name">The name of the referenced external resource</param>
    /// <returns>The configured <see cref="IExternalResourceDefinitionBuilder"/></returns>
    IExternalResourceDefinitionBuilder WithName(string name);

    /// <summary>
    /// Configures the endpoint at which to get the defined resource
    /// </summary>
    /// <param name="endpoint">The endpoint at which to get the defined resource</param>
    /// <returns>The configured <see cref="IExternalResourceDefinitionBuilder"/></returns>
    IExternalResourceDefinitionBuilder WithEndpoint(OneOf<EndpointDefinition, Uri> endpoint);

    /// <summary>
    /// Configures the endpoint at which to get the defined resource.
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the endpoint at which to get the defined resource.</param>
    /// <returns>The configured <see cref="IExternalResourceDefinitionBuilder"/></returns>
    IExternalResourceDefinitionBuilder WithEndpoint(Action<IEndpointDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="ExternalResourceDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ExternalResourceDefinition"/></returns>
    ExternalResourceDefinition Build();

}
