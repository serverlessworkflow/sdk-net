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
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationEndpointsDefinitionBuilder"/> interface
/// </summary>
public class OAuth2AuthenticationEndpointsDefinitionBuilder
    : IOAuth2AuthenticationEndpointsDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the relative path to the token endpoint. Defaults to `/oauth2/token`
    /// </summary>
    protected Uri Token { get; set; } = new("/oauth2/token");

    /// <summary>
    /// Gets/sets the relative path to the revocation endpoint. Defaults to `/oauth2/revoke`
    /// </summary>
    protected Uri Revocation { get; set; } = new("/oauth2/revoke");

    /// <summary>
    /// Gets/sets the relative path to the introspection endpoint. Defaults to `/oauth2/introspect`
    /// </summary>
    protected Uri Introspection { get; set; } = new("/oauth2/introspect");

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationEndpointsDefinitionBuilder WithTokenEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        this.Token = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationEndpointsDefinitionBuilder WithRevocationEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        this.Revocation = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationEndpointsDefinitionBuilder WithIntrospectionEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        this.Introspection = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual OAuth2AuthenticationEndpointsDefinition Build()
    {
        if (this.Token == null) throw new NullReferenceException("The token endpoint must be configured");
        if (this.Revocation == null) throw new NullReferenceException("The revocation endpoint must be configured");
        if (this.Introspection == null) throw new NullReferenceException("The introspection endpoint must be configured");
        return new()
        {
            Token = this.Token,
            Revocation = this.Revocation,
            Introspection = this.Introspection
        };
    }

}