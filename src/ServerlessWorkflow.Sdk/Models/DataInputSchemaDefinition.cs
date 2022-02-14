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
using Newtonsoft.Json.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the object used to configure a <see cref="WorkflowDefinition"/>'s data input schema
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class DataInputSchemaDefinition
    {

        /// <summary>
        /// Gets/sets the url of the <see cref="WorkflowDefinition"/>'s input data schema
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "schema")]
        [System.Text.Json.Serialization.JsonPropertyName("schema")]
        [YamlMember(Alias = "schema")]
        [ProtoMember(1, Name = "schema")]
        [DataMember(Order = 1, Name = "schema")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<JSchema, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<JSchema, Uri>))]

        protected virtual OneOf<JSchema, Uri> SchemaValue { get; set; } = null!;

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s data input schema
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual JSchema? Schema
        {
            get
            {
                return this.SchemaValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.SchemaValue = null;
                else
                    this.SchemaValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at the <see cref="WorkflowDefinition"/>'s input data schema
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? SchemaUri
        {
            get
            {
                return this.SchemaValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.SchemaValue = null;
                else
                    this.SchemaValue = value;
            }
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to terminate the <see cref="WorkflowDefinition"/>'s execution whenever the validation of the input data fails. Defaults to true.
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual bool FailOnValidationErrors { get; set; } = true;

    }

}
