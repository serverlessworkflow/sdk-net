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
/// Defines the fundamentals of a service used to build <see cref="OAuth2AuthenticationClientDefinition"/>s
/// </summary>
public interface IOAuth2AuthenticationClientDefinitionBuilder
{

    /// <summary>
    /// Sets the OAUTH2 client's id
    /// </summary>
    /// <param name="id">The client's id</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationClientDefinitionBuilder"/></returns>
    IOAuth2AuthenticationClientDefinitionBuilder WithId(string id);

    /// <summary>
    /// Sets the OAUTH2 client's secret
    /// </summary>
    /// <param name="secret">The client's secret</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationClientDefinitionBuilder"/></returns>
    IOAuth2AuthenticationClientDefinitionBuilder WithSecret(string secret);

    /// <summary>
    /// Builds the configured <see cref="OAuth2AuthenticationClientDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="OAuth2AuthenticationClientDefinition"/></returns>
    OAuth2AuthenticationClientDefinition Build();

}
