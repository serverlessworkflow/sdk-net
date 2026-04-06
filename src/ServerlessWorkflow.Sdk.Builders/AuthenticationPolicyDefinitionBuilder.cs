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
/// Represents the default implementation of the <see cref="IAuthenticationPolicyDefinitionBuilder"/> interface
/// </summary>
public sealed class AuthenticationPolicyDefinitionBuilder
    : IAuthenticationPolicyDefinitionBuilder
{

    string? policy;
    IAuthenticationSchemeDefinitionBuilder? schemeBuilder;

    /// <inheritdoc/>
    public void Use(string policy)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(policy);
        this.policy = policy;
    }

    /// <inheritdoc/>
    public IBasicAuthenticationSchemeDefinitionBuilder Basic()
    {
        var builder = new BasicAuthenticationSchemeDefinitionBuilder();
        schemeBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IBearerAuthenticationSchemeDefinitionBuilder Bearer()
    {
        var builder = new BearerAuthenticationSchemeDefinitionBuilder();
        schemeBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public ICertificateAuthenticationSchemeDefinitionBuilder Certificate()
    {
        var builder = new CertificateAuthenticationSchemeDefinitionBuilder();
        schemeBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IDigestAuthenticationSchemeDefinitionBuilder Digest()
    {
        var builder = new DigestAuthenticationSchemeDefinitionBuilder();
        schemeBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationSchemeDefinitionBuilder OAuth2()
    {
        var builder = new OAuth2AuthenticationSchemeDefinitionBuilder();
        schemeBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IOpenIDConnectAuthenticationSchemeDefinitionBuilder OpenIDConnect()
    {
        var builder = new OpenIDConnectAuthenticationSchemeDefinitionBuilder();
        schemeBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public AuthenticationPolicyDefinition Build()
    {
        if (schemeBuilder == null) throw new NullReferenceException("The authentication scheme must be set");
        var scheme = schemeBuilder.Build();
        return new()
        {
            Use = policy,
            Basic = scheme is BasicAuthenticationSchemeDefinition basic ? basic : null,
            Bearer = scheme is BearerAuthenticationSchemeDefinition bearer ? bearer : null,
            OAuth2 = scheme is OAuth2AuthenticationSchemeDefinition oauth2 ? oauth2 : null,
            Oidc = scheme is OpenIDConnectSchemeDefinition oidc ? oidc : null
        };
    }

}
