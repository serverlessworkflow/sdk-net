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
public sealed class OAuth2AuthenticationClientDefinitionBuilder
    : IOAuth2AuthenticationClientDefinitionBuilder
{

    string? id;
    string? secret;
    string? assertion;
    string? authentication;

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithId(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        this.id = id;
        return this;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithSecret(string secret)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret);
        this.secret = secret;
        return this;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithAssertion(string assertion)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(assertion);
        this.assertion = assertion;
        return this;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithAuthenticationMethod(string method)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(method);
        authentication = method;
        return this;
    }

    /// <inheritdoc/>
    public OAuth2AuthenticationClientDefinition Build() => new()
    {
        Id = id,
        Secret = secret,
        Assertion = assertion,
        Authentication = authentication
    };

}
