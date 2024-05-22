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
/// Defines the fundamentals of a service used to build <see cref="EndpointDefinition"/>s
/// </summary>
/// <typeparam name="TBuilder">The type of the <see cref="IEndpointDefinitionBuilder{TBuilder}"/> to use</typeparam>
public interface IEndpointDefinitionBuilder<TBuilder>
    where TBuilder : IEndpointDefinitionBuilder<TBuilder>
{

    /// <summary>
    /// Sets the endpoint's <see cref="Uri"/>
    /// </summary>
    /// <param name="uri">The endpoint's <see cref="Uri"/></param>
    /// <returns>The configured <see cref="IEndpointDefinitionBuilder{TBuilder}"/></returns>
    TBuilder WithUri(Uri uri);

    /// <summary>
    /// Configures the authentication policy used to get the external resource
    /// </summary>
    /// <param name="reference">A reference to the authentication policy to use</param>
    /// <returns>The configured <see cref="IEndpointDefinitionBuilder{TBuilder}"/></returns>
    TBuilder UseAuthentication(Uri reference);

    /// <summary>
    /// Configures the authentication policy used to get the external resource
    /// </summary>
    /// <param name="authenticationPolicy">The authentication policy to use</param>
    /// <returns>The configured <see cref="IEndpointDefinitionBuilder{TBuilder}"/></returns>
    TBuilder UseAuthentication(AuthenticationPolicyDefinition authenticationPolicy);

    /// <summary>
    /// Configures the authentication policy used to get the external resource
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the authentication policy to use</param>
    /// <returns>The configured <see cref="IEndpointDefinitionBuilder{TBuilder}"/></returns>
    TBuilder UseAuthentication(Action<IAuthenticationPolicyDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="EndpointDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="EndpointDefinition"/></returns>
    EndpointDefinition Build();

}

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="EndpointDefinition"/>s
/// </summary>
public interface IEndpointDefinitionBuilder
    : IEndpointDefinitionBuilder<IEndpointDefinitionBuilder>
{



}