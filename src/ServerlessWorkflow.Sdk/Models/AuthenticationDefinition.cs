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
using Newtonsoft.Json.Linq;
using System;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a reusable definition of a workflow authentication mechanism
    /// </summary>
    [DataContract]
    [ProtoContract]
    public class AuthenticationDefinition
    {

        /// <summary>
        /// Gets/sets the <see cref="AuthenticationDefinition"/>'s name
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        [YamlMember(Alias = "name")]
        [ProtoMember(1, Name = "name")]
        [DataMember(Order = 1, Name = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="AuthenticationDefinition"/>'s scheme
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "scheme")]
        [System.Text.Json.Serialization.JsonPropertyName("scheme")]
        [YamlMember(Alias = "scheme")]
        [ProtoMember(2, Name = "scheme")]
        [DataMember(Order = 2, Name = "scheme")]
        public virtual AuthenticationScheme Scheme { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="AuthenticationDefinition"/>'s <see cref="AuthenticationProperties"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties")]
        [System.Text.Json.Serialization.JsonPropertyName("properties")]
        [YamlMember(Alias = "properties")]
        [ProtoMember(3, Name = "properties")]
        [DataMember(Order = 3, Name = "properties")]
        protected virtual JToken PropertiesToken { get; set; }

        private AuthenticationProperties _Properties;
        /// <summary>
        /// Gets/sets the <see cref="AuthenticationDefinition"/>'s info
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual AuthenticationProperties Properties
        {
            get
            {
                if (this._Properties == null
                    && this.PropertiesToken != null)
                {
                    if (this.PropertiesToken.Type == JTokenType.String)
                        this._Properties = new SecretBasedAuthenticationProperties(this.PropertiesToken.ToObject<string>());
                    else
                        this._Properties = this.Scheme switch
                        {
                            AuthenticationScheme.Basic => this.PropertiesToken.ToObject<BasicAuthenticationProperties>(),
                            AuthenticationScheme.Bearer => this.PropertiesToken.ToObject<BearerAuthenticationProperties>(),
                            AuthenticationScheme.OAuth2 => this.PropertiesToken.ToObject<OAuth2AuthenticationProperties>(),
                            _ => throw new NotSupportedException($"The specified authentication scheme '{EnumHelper.Stringify(this.Scheme)}' is not supported"),
                        };
                }
                return this._Properties;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                switch (value)
                {
                    case BasicAuthenticationProperties:
                        this.Scheme = AuthenticationScheme.Basic;
                        break;
                    case BearerAuthenticationProperties:
                        this.Scheme = AuthenticationScheme.Bearer;
                        break;
                    case OAuth2AuthenticationProperties:
                        this.Scheme = AuthenticationScheme.OAuth2;
                        break;
                    case SecretBasedAuthenticationProperties:
                        break;
                    default:
                        throw new NotSupportedException($"The specified authentication info type '{value.GetType()}' is not supported");
                }
                this._Properties = value;
                this.PropertiesToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the reference to the secret that defines the <see cref="AuthenticationDefinition"/>'s properties
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual string SecretRef
        {
            get
            {
                if (this.Properties is SecretBasedAuthenticationProperties secret)
                    return secret.Secret;
                return null;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (this.Properties is SecretBasedAuthenticationProperties secret)
                    secret.Secret = value;
                else
                    this._Properties = new SecretBasedAuthenticationProperties(value);
                this.PropertiesToken = JToken.FromObject(value);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
