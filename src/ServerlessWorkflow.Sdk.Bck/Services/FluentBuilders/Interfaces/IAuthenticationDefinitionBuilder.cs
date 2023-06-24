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
    /// Defines the fundamentals of a service used to build an <see cref="AuthenticationDefinition"/>
    /// </summary>
    public interface IAuthenticationDefinitionBuilder
    {

        /// <summary>
        /// Sets the name of the <see cref="AuthenticationDefinition"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to build</param>
        /// <returns>The configured <see cref="IAuthenticationDefinitionBuilder"/></returns>
        IAuthenticationDefinitionBuilder WithName(string name);

        /// <summary>
        /// Loads the <see cref="AuthenticationDefinition"/> from a secret
        /// </summary>
        /// <param name="secret">The name of the secret to load the <see cref="AuthenticationDefinition"/> from</param>
        void LoadFromSecret(string secret);

        /// <summary>
        /// Builds the <see cref="AuthenticationDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="AuthenticationDefinition"/></returns>
        AuthenticationDefinition Build();

    }

}
