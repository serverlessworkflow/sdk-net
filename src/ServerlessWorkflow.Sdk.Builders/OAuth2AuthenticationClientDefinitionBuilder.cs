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
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationClientDefinitionBuilder"/> interface
/// </summary>
public class OAuth2AuthenticationClientDefinitionBuilder
    : IOAuth2AuthenticationClientDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the OAUTH2 `client_id` to use
    /// </summary>
    protected string? Id { get; set; }

    /// <summary>
    /// Gets/sets the OAUTH2 `client_secret` to use, if any
    /// </summary>
    protected string? Secret { get; set; }

    /// <summary>
    /// Gets/sets a JWT containing a signed assertion with the application credentials
    /// </summary>
    protected string? Assertion { get; set; }

    /// <summary>
    /// Gets/sets the authentication method to use to authenticate the client
    /// </summary>
    protected string? Authentication { get; set; }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationClientDefinitionBuilder WithId(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        this.Id = id;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationClientDefinitionBuilder WithSecret(string secret)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret);
        this.Secret = secret;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationClientDefinitionBuilder WithAssertion(string assertion)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(assertion);
        this.Assertion = assertion;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationClientDefinitionBuilder WithAuthenticationMethod(string method)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(method);
        this.Authentication = method;
        return this;
    }

    /// <inheritdoc/>
    public virtual OAuth2AuthenticationClientDefinition Build()
    {
        return new()
        {
            Id = this.Id,
            Secret = this.Secret,
            Assertion = this.Assertion,
            Authentication = this.Authentication
        };
    }

}
