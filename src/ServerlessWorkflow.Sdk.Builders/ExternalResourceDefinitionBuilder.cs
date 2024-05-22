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
    /// Gets/sets the uri that references the external resource
    /// </summary>
    protected virtual Uri? Uri { get; set; }

    /// <summary>
    /// Gets/sets a reference to the authentication policy to use
    /// </summary>
    protected virtual Uri? AuthenticationReference { get; set; }

    /// <summary>
    /// Gets/sets the authentication policy to use
    /// </summary>
    protected virtual AuthenticationPolicyDefinition? Authentication { get; set; }

    /// <inheritdoc/>
    public virtual IExternalResourceDefinitionBuilder WithName(string name)
    {
        this.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IExternalResourceDefinitionBuilder WithUri(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        this.Uri = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IExternalResourceDefinitionBuilder UseAuthentication(Uri reference)
    {
        ArgumentNullException.ThrowIfNull(reference);
        this.AuthenticationReference = reference;
        return this;
    }

    /// <inheritdoc/>
    public virtual IExternalResourceDefinitionBuilder UseAuthentication(AuthenticationPolicyDefinition authentication)
    {
        ArgumentNullException.ThrowIfNull(authentication);
        this.Authentication = authentication;
        return this;
    }

    /// <inheritdoc/>
    public virtual IExternalResourceDefinitionBuilder UseAuthentication(Action<IAuthenticationPolicyDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new AuthenticationPolicyDefinitionBuilder();
        setup(builder);
        this.Authentication = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual ExternalResourceDefinition Build()
    {
        if (this.Uri == null) throw new NullReferenceException("The uri that references the external resource must be set");
        var reference = new ExternalResourceDefinition()
        {
            Name = this.Name,
            Uri = this.Uri
        };
        if (this.AuthenticationReference == null) reference.Authentication = new() { Ref = this.AuthenticationReference };
        else if (this.Authentication != null) reference.Authentication = this.Authentication;
        return reference;
    }

    EndpointDefinition IEndpointDefinitionBuilder<IExternalResourceDefinitionBuilder>.Build() => this.Build();

}
