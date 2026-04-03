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
/// Defines the fundamentals of a service used to build <see cref="AuthenticationPolicyDefinition"/>s
/// </summary>
public interface IAuthenticationPolicyDefinitionBuilder
{

    /// <summary>
    /// Gets the name of the top-level authentication policy to use
    /// </summary>
    /// <param name="policy">The name of the top-level authentication to use</param>
    void Use(string policy);

    /// <summary>
    /// Configures the policy to use 'Basic' authentication
    /// </summary>
    /// <returns>A new <see cref="IBasicAuthenticationSchemeDefinitionBuilder"/></returns>
    IBasicAuthenticationSchemeDefinitionBuilder Basic();

    /// <summary>
    /// Configures the policy to use 'Bearer' authentication
    /// </summary>
    /// <returns>A new <see cref="IBasicAuthenticationSchemeDefinitionBuilder"/></returns>
    IBearerAuthenticationSchemeDefinitionBuilder Bearer();

    /// <summary>
    /// Configures the policy to use 'Certificate' authentication
    /// </summary>
    /// <returns>A new <see cref="ICertificateAuthenticationSchemeDefinitionBuilder"/></returns>
    ICertificateAuthenticationSchemeDefinitionBuilder Certificate();

    /// <summary>
    /// Configures the policy to use 'Digest' authentication
    /// </summary>
    /// <returns>A new <see cref="IDigestAuthenticationSchemeDefinitionBuilder"/></returns>
    IDigestAuthenticationSchemeDefinitionBuilder Digest();

    /// <summary>
    /// Configures the policy to use 'OAuth2' authentication
    /// </summary>
    /// <returns>A new <see cref="IBasicAuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder OAuth2();

    /// <summary>
    /// Configures the policy to use 'OpenIDConnect' authentication
    /// </summary>
    /// <returns>A new <see cref="IOpenIDConnectAuthenticationSchemeDefinitionBuilder"/></returns>
    IOpenIDConnectAuthenticationSchemeDefinitionBuilder OpenIDConnect();

    /// <summary>
    /// Builds the configured <see cref="AuthenticationPolicyDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="AuthenticationPolicyDefinition"/></returns>
    AuthenticationPolicyDefinition Build();

}