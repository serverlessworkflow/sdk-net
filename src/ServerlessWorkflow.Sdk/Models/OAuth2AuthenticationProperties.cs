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
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual OAuth2GrantType GrantType { get; set; }

        /// <summary>
        /// Gets/sets the uri of the OAuth2 authority to use to generate an access token
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual Uri Authority { get; set; }

        /// <summary>
        /// Gets/sets the id of the OAuth2 client to use
        /// </summary>
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual string ClientId { get; set; }

        /// <summary>
        /// Gets/sets the secret of the non-public OAuth2 client to use. Required when <see cref="GrantType"/> has been set to <see cref="OAuth2GrantType.TokenExchange"/>
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        public virtual string ClientSecret { get; set; }

        /// <summary>
        /// Gets/sets the username to use when authenticating
        /// </summary>
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        public virtual string Username { get; set; }

        /// <summary>
        /// Gets/sets the password to use when authenticating
        /// </summary>
        [ProtoMember(6)]
        [DataMember(Order = 6)]
        public virtual string Password { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the authorized scopes of the resulting token
        /// </summary>
        [ProtoMember(7)]
        [DataMember(Order = 7)]
        public virtual List<string> Scopes { get; set; } = new List<string>();

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the authorized audiences of the resulting token
        /// </summary>
        [ProtoMember(8)]
        [DataMember(Order = 8)]
        public virtual List<string> Audiences { get; set; } = new List<string>();

        /// <summary>
        /// Gets/sets the token to exchange for Impersonation
        /// </summary>
        [ProtoMember(9)]
        [DataMember(Order = 9)]
        public virtual string SubjectToken { get; set; }

        /// <summary>
        /// Gets/sets the requested subject for Impersonation and Direct Naked Impersonation, which can be the id or the username of the user to impersonate
        /// </summary>
        [ProtoMember(10)]
        [DataMember(Order = 10)]
        public virtual string RequestedSubject { get; set; }

        /// <summary>
        /// Gets/sets the issuer that must generate a new token in return for the exchanged one
        /// </summary>
        [ProtoMember(11)]
        [DataMember(Order = 11)]
        public virtual string RequestedIssuer { get; set; }

    }

}
