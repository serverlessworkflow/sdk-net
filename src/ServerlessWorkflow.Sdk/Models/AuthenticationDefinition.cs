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
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(1, IsRequired = true)]
        [DataMember(Order = 1, IsRequired = true)]
        public virtual string Name { get; set; } = null!;

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
        /// Gets/sets a <see cref="OneOf{T1, T2}"/> that represents the <see cref="AuthenticationDefinition"/>'s <see cref="AuthenticationProperties"/>
        /// </summary>
        [Required]
        [YamlMember(Alias = "properties")]
        [ProtoMember(3, IsRequired = true, Name = "properties")]
        [DataMember(Order = 3, IsRequired = true, Name = "properties")]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty(PropertyName = "properties"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<Any, string>))]
        [System.Text.Json.Serialization.JsonPropertyName("properties"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<AuthenticationProperties, string>))]
        protected virtual OneOf<Any, string> PropertiesValue { get; set; } = null!;

        /// <summary>
        /// Gets/sets the <see cref="AuthenticationDefinition"/>'s properties
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
                if (!string.IsNullOrWhiteSpace(this.PropertiesValue.T2Value))
                    return new SecretBasedAuthenticationProperties(this.PropertiesValue.T2Value);
                if (this.PropertiesValue?.T1Value == null)
                    return null!;
                return this.Scheme switch
                {
                    AuthenticationScheme.Basic => this.PropertiesValue.T1Value.ToObject<BasicAuthenticationProperties>(),
                    AuthenticationScheme.Bearer => this.PropertiesValue.T1Value.ToObject<BearerAuthenticationProperties>(),
                    AuthenticationScheme.OAuth2 => this.PropertiesValue.T1Value.ToObject<OAuth2AuthenticationProperties>(),
                    _ => throw new NotSupportedException($"The specified authentication scheme '{EnumHelper.Stringify(this.Scheme)}' is not supported")
                };
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
                this.PropertiesValue = Any.FromObject(value);
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
        public virtual string? SecretRef
        {
            get
            {
                return this.PropertiesValue.T2Value;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.PropertiesValue.T2Value = value;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
