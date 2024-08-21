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
/// Represents the default implementation of the <see cref="IExternalResourceDefinitionBuilder"/> interface
/// </summary>
public class ExternalResourceDefinitionBuilder
    : IExternalResourceDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the external resource's name
    /// </summary>
    protected virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets the endpoint at which to get the defined resource
    /// </summary>
    protected virtual OneOf<EndpointDefinition, Uri>? Endpoint { get; set; }

    /// <inheritdoc/>
    public virtual IExternalResourceDefinitionBuilder WithName(string name)
    {
        this.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IExternalResourceDefinitionBuilder WithEndpoint(OneOf<EndpointDefinition, Uri> endpoint)
    {
        ArgumentNullException.ThrowIfNull(endpoint);
        this.Endpoint = endpoint;
        return this;
    }

    /// <inheritdoc/>
    public virtual IExternalResourceDefinitionBuilder WithEndpoint(Action<IEndpointDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new EndpointDefinitionBuilder();
        setup(builder);
        this.Endpoint = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual ExternalResourceDefinition Build()
    {
        if (this.Endpoint == null) throw new NullReferenceException("The endpoint at which to get the defined resource must be set");
        var externalResource = new ExternalResourceDefinition()
        {
            Name = this.Name,
            Endpoint = this.Endpoint
        };
        return externalResource;
    }

}
