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
/// Defines the fundamentals of a service used to build <see cref="OAuth2AuthenticationSchemeDefinition"/>s
/// </summary>
public interface IOAuth2AuthenticationSchemeDefinitionBuilder
    : IAuthenticationSchemeDefinitionBuilder<OAuth2AuthenticationSchemeDefinition>
{

    /// <summary>
    /// Sets the uri of the OAUTH2 authority to use
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> to the OAUTH2 authority to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithAuthority(Uri uri);

    /// <summary>
    /// Sets the grant type to use
    /// </summary>
    /// <param name="grantType">The grant type to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithGrantType(string grantType);

    /// <summary>
    /// Sets the definition of the client to use
    /// </summary>
    /// <param name="client">The <see cref="OAuth2AuthenticationClientDefinition"/> to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithClient(OAuth2AuthenticationClientDefinition client);

    /// <summary>
    /// Sets the definition of the client to use
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OAuth2AuthenticationClientDefinition"/> to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithClient(Action<IOAuth2AuthenticationClientDefinitionBuilder> setup);

    /// <summary>
    /// Sets the scopes to request the token for
    /// </summary>
    /// <param name="scopes">The scopes to request the token for</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithScopes(params string[] scopes);

    /// <summary>
    /// Sets the audiences to request the token for
    /// </summary>
    /// <param name="audiences">The audiences to request the token for</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithAudiences(params string[] audiences);

    /// <summary>
    /// Sets the username to use. Used only if grant type is <see cref="OAuth2GrantType.Password"/>
    /// </summary>
    /// <param name="username">The username to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithUsername(string username);

    /// <summary>
    /// Sets the password to use. Used only if grant type is <see cref="OAuth2GrantType.Password"/>
    /// </summary>
    /// <param name="password">The password to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithPassword(string password);

    /// <summary>
    /// Sets the security token that represents the identity of the party on behalf of whom the request is being made. Used only if grant type is <see cref="OAuth2GrantType.TokenExchange"/>, in which case it is required
    /// </summary>
    /// <param name="subject">The <see cref="OAuth2TokenDefinition"/> representing the identity of the party</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithSubject(OAuth2TokenDefinition subject);

    /// <summary>
    /// Sets the security token that represents the identity of the acting party. Typically, this will be the party that is authorized to use the requested security token and act on behalf of the subject.
    /// Used only if grant type is <see cref="OAuth2GrantType.TokenExchange"/>, in which case it is required
    /// </summary>
    /// <param name="actor">The <see cref="OAuth2TokenDefinition"/> representing the identity of the acting party</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/></returns>
    IOAuth2AuthenticationSchemeDefinitionBuilder WithActor(OAuth2TokenDefinition actor);

}
