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
/// Defines the fundamentals of a service used to build <see cref="OAuth2AuthenticationEndpointsDefinition"/>s
/// </summary>
public interface IOAuth2AuthenticationEndpointsDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="OAuth2AuthenticationEndpointsDefinition"/> to build to use the specified token endpoint relative uri
    /// </summary>
    /// <param name="uri">The relative uri of the token endpoint to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationEndpointsDefinitionBuilder"/></returns>
    IOAuth2AuthenticationEndpointsDefinitionBuilder WithTokenEndpoint(Uri uri);

    /// <summary>
    /// Configures the <see cref="OAuth2AuthenticationEndpointsDefinition"/> to build to use the specified revocation endpoint relative uri
    /// </summary>
    /// <param name="uri">The relative uri of the revocation endpoint to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationEndpointsDefinitionBuilder"/></returns>
    IOAuth2AuthenticationEndpointsDefinitionBuilder WithRevocationEndpoint(Uri uri);

    /// <summary>
    /// Configures the <see cref="OAuth2AuthenticationEndpointsDefinition"/> to build to use the specified introspection endpoint relative uri
    /// </summary>
    /// <param name="uri">The relative uri of the introspection endpoint to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationEndpointsDefinitionBuilder"/></returns>
    IOAuth2AuthenticationEndpointsDefinitionBuilder WithIntrospectionEndpoint(Uri uri);

    /// <summary>
    /// Builds the configured <see cref="OAuth2AuthenticationEndpointsDefinition"/>
    /// </summary>
    /// <returns>The configured <see cref="OAuth2AuthenticationEndpointsDefinition"/></returns>
    OAuth2AuthenticationEndpointsDefinition Build();

}