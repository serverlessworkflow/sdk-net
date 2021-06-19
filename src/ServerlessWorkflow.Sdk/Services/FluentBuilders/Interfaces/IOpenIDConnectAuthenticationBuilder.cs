/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build a <see cref="AuthenticationDefinition"/> with scheme <see cref="AuthenticationScheme.OIDC"/>
    /// </summary>
    public interface IOpenIDConnectAuthenticationBuilder
        : IAuthenticationDefinitionBuilder
    {

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to use the specified <see cref="OpenIDConnectGrantType"/> when requesting an access token
        /// </summary>
        /// <param name="grantType">The <see cref="OpenIDConnectGrantType"/> to use</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder UseGranType(OpenIDConnectGrantType grantType);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to use the specified client ID when requesting an access token
        /// </summary>
        /// <param name="clientId">The client ID to use</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder WithClientId(string clientId);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to use the specified client secret when requesting an access token
        /// </summary>
        /// <param name="clientSecret">The username to use</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder WithClientSecret(string clientSecret);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to use the specified username to authenticate
        /// </summary>
        /// <param name="username">The username to use</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder WithUserName(string username);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to use the specified password to authenticate
        /// </summary>
        /// <param name="password">The password to use</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder WithPassword(string password);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to use the specified scopes when requesting an access token
        /// </summary>
        /// <param name="scopes">An array containing the scopes to use</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder UseScopes(params string[] scopes);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to use the specified audiences when requesting an access token
        /// </summary>
        /// <param name="audiences">An array containing the audiences to use</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder UseAudiences(params string[] audiences);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to exchange the specified subject token when requesting an access token
        /// </summary>
        /// <param name="token">The token to exchange</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder WithSubjectToken(string token);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to impersonate the specified user
        /// </summary>
        /// <param name="subject">The subject of the user to impersonate</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder WithRequestedSubject(string subject);

        /// <summary>
        /// Configures the <see cref="AuthenticationDefinition"/> to exchange an access token with a new one from the specified issuer
        /// </summary>
        /// <param name="issuer">The issuer to exchnage the token with</param>
        /// <returns>The configured <see cref="IOpenIDConnectAuthenticationBuilder"/></returns>
        IOpenIDConnectAuthenticationBuilder WithRequestedIssuer(string issuer);

    }

}
