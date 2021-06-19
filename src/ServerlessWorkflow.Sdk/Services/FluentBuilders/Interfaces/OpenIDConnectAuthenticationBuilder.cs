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
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IOpenIDConnectAuthenticationBuilder"/>
    /// </summary>
    public class OpenIDConnectAuthenticationBuilder
        : AuthenticationDefinitionBuilder, IOpenIDConnectAuthenticationBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="OpenIDConnectAuthenticationBuilder"/>
        /// </summary>
        public OpenIDConnectAuthenticationBuilder()
            : base(new AuthenticationDefinition() { Properties = new OpenIDConnectAuthenticationProperties() })
        {

        }

        /// <summary>
        /// Gets the <see cref="OpenIDConnectAuthenticationProperties"/> of the <see cref="AuthenticationDefinition"/> to build
        /// </summary>
        protected OpenIDConnectAuthenticationProperties Properties
        {
            get
            {
                return (OpenIDConnectAuthenticationProperties)this.AuthenticationDefinition.Properties;
            }
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder UseAudiences(params string[] audiences)
        {
            if (audiences == null)
                throw new ArgumentNullException(nameof(audiences));
            this.Properties.Audiences = audiences.ToList();
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder UseGranType(OpenIDConnectGrantType grantType)
        {
            this.Properties.GrantType = grantType;
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder UseScopes(params string[] scopes)
        {
            if (scopes == null)
                throw new ArgumentNullException(nameof(scopes));
            this.Properties.Audiences = scopes.ToList();
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder WithClientId(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));
            this.Properties.ClientId = clientId;
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder WithClientSecret(string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret));
            this.Properties.ClientSecret = clientSecret;
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder WithPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));
            this.Properties.Password = password;
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder WithRequestedIssuer(string issuer)
        {
            if (string.IsNullOrWhiteSpace(issuer))
                throw new ArgumentNullException(nameof(issuer));
            this.Properties.RequestedIssuer = issuer;
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder WithRequestedSubject(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentNullException(nameof(subject));
            this.Properties.RequestedSubject = subject;
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder WithSubjectToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            this.Properties.SubjectToken = token;
            return this;
        }

        /// <inheritdoc/>
        public virtual IOpenIDConnectAuthenticationBuilder WithUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            this.Properties.Username = username;
            return this;
        }

    }

}
