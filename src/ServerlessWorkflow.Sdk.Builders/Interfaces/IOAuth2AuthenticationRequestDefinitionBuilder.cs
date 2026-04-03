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
/// Defines the fundamentals of a service used to build <see cref="OAuth2AuthenticationRequestDefinition"/>s
/// </summary>
public interface IOAuth2AuthenticationRequestDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="OAuth2AuthenticationRequestDefinition"/> to build to use the specified encoding
    /// </summary>
    /// <param name="encoding">The encoding to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationRequestDefinitionBuilder"/></returns>
    IOAuth2AuthenticationRequestDefinitionBuilder WithEncoding(string encoding);

    /// <summary>
    /// Builds the configured <see cref="OAuth2AuthenticationRequestDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="OAuth2AuthenticationRequestDefinition"/></returns>
    OAuth2AuthenticationRequestDefinition Build();

}