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
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to configure an 'OAuth2' authentication scheme
    /// </summary>
    public class OAuth2AuthenticationProperties
        : AuthenticationProperties
    {

        /// <summary>
        /// Gets/sets the OAuth2 grant type to use
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "grantType")]
        [System.Text.Json.Serialization.JsonPropertyName("grantType")]
        [YamlMember(Alias = "grantType")]
        public virtual OAuth2GrantType GrantType { get; set; }

        /// <summary>
        /// Gets/sets the uri of the OAuth2 authority to use to generate an access token
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "authority")]
        [System.Text.Json.Serialization.JsonPropertyName("authority")]
        [YamlMember(Alias = "authority")]
        public virtual Uri Authority { get; set; }

        /// <summary>
        /// Gets/sets the id of the OAuth2 client to use
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "clientId")]
        [System.Text.Json.Serialization.JsonPropertyName("clientId")]
        [YamlMember(Alias = "clientId")]
        public virtual string ClientId { get; set; }

        /// <summary>
        /// Gets/sets the secret of the non-public OAuth2 client to use. Required when <see cref="GrantType"/> has been set to <see cref="OAuth2GrantType.TokenExchange"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "clientSecret")]
        [System.Text.Json.Serialization.JsonPropertyName("clientSecret")]
        [YamlMember(Alias = "clientSecret")]
        public virtual string ClientSecret { get; set; }

        /// <summary>
        /// Gets/sets the username to use when authenticating
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "username")]
        [System.Text.Json.Serialization.JsonPropertyName("username")]
        [YamlMember(Alias = "username")]
        public virtual string Username { get; set; }

        /// <summary>
        /// Gets/sets the password to use when authenticating
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "password")]
        [System.Text.Json.Serialization.JsonPropertyName("password")]
        [YamlMember(Alias = "password")]
        public virtual string Password { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the authorized scopes of the resulting token
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "scopes")]
        [System.Text.Json.Serialization.JsonPropertyName("scopes")]
        [YamlMember(Alias = "scopes")]
        public virtual List<string> Scopes { get; set; } = new List<string>();

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the authorized audiences of the resulting token
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "audiences")]
        [System.Text.Json.Serialization.JsonPropertyName("audiences")]
        [YamlMember(Alias = "audiences")]
        public virtual List<string> Audiences { get; set; } = new List<string>();

        /// <summary>
        /// Gets/sets the token to exchange for Impersonation
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "subjectToken")]
        [System.Text.Json.Serialization.JsonPropertyName("subjectToken")]
        [YamlMember(Alias = "subjectToken")]
        public virtual string SubjectToken { get; set; }

        /// <summary>
        /// Gets/sets the requested subject for Impersonation and Direct Naked Impersonation, which can be the id or the username of the user to impersonate
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "requestedSubject")]
        [System.Text.Json.Serialization.JsonPropertyName("requestedSubject")]
        [YamlMember(Alias = "requestedSubject")]
        public virtual string RequestedSubject { get; set; }

        /// <summary>
        /// Gets/sets the issuer that must generate a new token in return for the exchanged one
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "requestedIssuer")]
        [System.Text.Json.Serialization.JsonPropertyName("requestedIssuer")]
        [YamlMember(Alias = "requestedIssuer")]
        public virtual string RequestedIssuer { get; set; }

    }

}
