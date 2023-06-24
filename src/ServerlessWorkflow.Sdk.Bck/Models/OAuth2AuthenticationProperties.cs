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
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to configure an 'OAuth2' authentication scheme
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class OAuth2AuthenticationProperties
        : AuthenticationProperties
    {

        /// <summary>
        /// Gets/sets the OAuth2 grant type to use
        /// </summary>
        [Newtonsoft.Json.JsonProperty("grant_type")]
        [System.Text.Json.Serialization.JsonPropertyName("grant_type")]
        [ProtoMember(1)]
        [DataMember(Name = "grant_type", Order = 1)]
        public virtual string GrantType { get; set; } = null!;

        /// <summary>
        /// Gets/sets the uri of the OAuth2 authority to use to generate an access token
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty("authority")]
        [System.Text.Json.Serialization.JsonPropertyName("authority")]
        [ProtoMember(2, IsRequired = true)]
        [DataMember(Name = "authority", Order = 2, IsRequired = true)]
        public virtual Uri Authority { get; set; } = null!;

        /// <summary>
        /// Gets/sets the id of the OAuth2 client to use
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty("client_id")]
        [System.Text.Json.Serialization.JsonPropertyName("client_id")]
        [ProtoMember(3, IsRequired = true)]
        [DataMember(Name = "client_id", Order = 3, IsRequired = true)]
        public virtual string ClientId { get; set; } = null!;

        /// <summary>
        /// Gets/sets the secret of the non-public OAuth2 client to use. Required when <see cref="GrantType"/> has been set to <see cref="OAuth2GrantType.TokenExchange"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("client_secret")]
        [System.Text.Json.Serialization.JsonPropertyName("client_secret")]
        [ProtoMember(4)]
        [DataMember(Name = "client_secret", Order = 4)]
        public virtual string? ClientSecret { get; set; }

        /// <summary>
        /// Gets/sets the username to use when authenticating
        /// </summary>
        [Newtonsoft.Json.JsonProperty("username")]
        [System.Text.Json.Serialization.JsonPropertyName("username")]
        [ProtoMember(5)]
        [DataMember(Name = "username", Order = 5)]
        public virtual string? Username { get; set; }

        /// <summary>
        /// Gets/sets the password to use when authenticating
        /// </summary>
        [Newtonsoft.Json.JsonProperty("password")]
        [System.Text.Json.Serialization.JsonPropertyName("password")]
        [ProtoMember(6)]
        [DataMember(Name = "password", Order = 6)]
        public virtual string? Password { get; set; }

        /// <summary>
        /// Gets/sets a space-separated list containing the authorized scopes to request
        /// </summary>
        [Newtonsoft.Json.JsonProperty("scope")]
        [System.Text.Json.Serialization.JsonPropertyName("scope")]
        [ProtoMember(7)]
        [DataMember(Name = "scope", Order = 7)]
        public virtual string? Scope { get; set; }

        /// <summary>
        /// Gets/sets a space-separated list containing the authorized audiences of the resulting token
        /// </summary>
        [Newtonsoft.Json.JsonProperty("audience")]
        [System.Text.Json.Serialization.JsonPropertyName("audience")]
        [ProtoMember(8)]
        [DataMember(Name = "audience", Order = 8)]
        public virtual string? Audience { get; set; }

        /// <summary>
        /// Gets/sets the token that represents the identity of the party on behalf of whom the request is being made.Typically, the subject of this token will be the subject of the security token issued in response to the request.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("subject_token")]
        [System.Text.Json.Serialization.JsonPropertyName("subject_token")]
        [ProtoMember(9)]
        [DataMember(Name = "subject_token", Order = 9)]
        public virtual string? SubjectToken { get; set; }

        /// <summary>
        /// Gets/sets an identifie that indicates the type of the security token in the "subject_token" parameter.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("subject_token_type")]
        [System.Text.Json.Serialization.JsonPropertyName("subject_token_type")]
        [ProtoMember(10)]
        [DataMember(Name = "subject_token_type", Order = 10)]
        public virtual string? SubjectTokenType { get; set; }

        /// <summary>
        /// Gets/sets a token that represents the identity of the acting party.Typically, this will be the party that is authorized to use the requested security token and act on behalf of the subject.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("actor_token")]
        [System.Text.Json.Serialization.JsonPropertyName("actor_token")]
        [ProtoMember(11)]
        [DataMember(Name = "actor_token", Order = 11)]
        public virtual string? ActorToken { get; set; }

        /// <summary>
        /// Gets/sets an identifier, as described in Section 3, that indicates the type of the security token in the "actor_token" parameter. This is REQUIRED when the "actor_token" parameter is present in the request but MUST NOT be included otherwise.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("actor_token_type")]
        [System.Text.Json.Serialization.JsonPropertyName("actor_token_type")]
        [ProtoMember(11)]
        [DataMember(Name = "actor_token_type", Order = 11)]
        public virtual string? ActorTokenType { get; set; }

    }

}
