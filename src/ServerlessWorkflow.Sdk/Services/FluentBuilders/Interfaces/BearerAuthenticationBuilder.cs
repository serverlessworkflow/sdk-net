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
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IBearerAuthenticationBuilder"/>
    /// </summary>
    public class BearerAuthenticationBuilder
        : AuthenticationDefinitionBuilder, IBearerAuthenticationBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="BearerAuthenticationBuilder"/>
        /// </summary>
        public BearerAuthenticationBuilder()
            : base(new AuthenticationDefinition() { Properties = new BearerAuthenticationProperties() })
        {

        }

        /// <summary>
        /// Gets the <see cref="BearerAuthenticationProperties"/> of the <see cref="AuthenticationDefinition"/> to build
        /// </summary>
        protected BearerAuthenticationProperties Properties
        {
            get
            {
                return (BearerAuthenticationProperties)this.AuthenticationDefinition.Properties;
            }
        }

        /// <inheritdoc/>
        public virtual IBearerAuthenticationBuilder WithToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            this.Properties.Token = token;
            return this;
        }

    }

}
