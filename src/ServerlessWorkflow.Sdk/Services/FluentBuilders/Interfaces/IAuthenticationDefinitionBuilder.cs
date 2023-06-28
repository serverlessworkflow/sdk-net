// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build an authentication definition
/// </summary>
public interface IAuthenticationDefinitionBuilder
    : IExtensibleBuilder<IAuthenticationDefinitionBuilder>
{

    /// <summary>
    /// Sets the name of the authentication definition to build
    /// </summary>
    /// <param name="name">The name of the authentication definition to build</param>
    /// <returns>The configured <see cref="IAuthenticationDefinitionBuilder"/></returns>
    IAuthenticationDefinitionBuilder WithName(string name);

    /// <summary>
    /// Loads the authentication definition from a secret
    /// </summary>
    /// <param name="secret">The name of the secret to load the authentication definition from</param>
    void LoadFromSecret(string secret);

    /// <summary>
    /// Builds the authentication definition
    /// </summary>
    /// <returns>A new authentication definition</returns>
    AuthenticationDefinition Build();

}
