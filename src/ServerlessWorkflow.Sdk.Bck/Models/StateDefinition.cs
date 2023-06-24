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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a <see href="https://github.com/serverlessworkflow/specification/blob/master/specification.md#State-Definition">serverless workflow state definition</see>
/// </summary>
[Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.AbstractClassConverterFactory))]
[System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.AbstractClassConverterFactory))]
[Discriminator(nameof(Type))]
[ProtoContract]
[ProtoInclude(100, typeof(CallbackStateDefinition))]
[ProtoInclude(200, typeof(SleepStateDefinition))]
[ProtoInclude(300, typeof(EventStateDefinition))]
[ProtoInclude(400, typeof(ForEachStateDefinition))]
[ProtoInclude(500, typeof(InjectStateDefinition))]
[ProtoInclude(600, typeof(OperationStateDefinition))]
[ProtoInclude(700, typeof(ParallelStateDefinition))]
[ProtoInclude(800, typeof(SwitchStateDefinition))]
[DataContract]
public abstract class StateDefinition
{

    /// <summary>
    /// Initializes a new state definition
    /// </summary>
    /// <param name="type">The state definition's type</param>
    protected StateDefinition(string type)
    {
        this.Type = type;
    }

    /// <summary>
    /// Gets/sets the state definition's id
    /// </summary>
    [ProtoMember(1)]
    [DataMember(Order = 1)]
    public virtual string? Id { get; set; }

    /// <summary>
    /// Gets/sets the state definition's id
    /// </summary>
    [Required]
    [Newtonsoft.Json.JsonRequired]
    [ProtoMember(2)]
    [DataMember(Order = 2)]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets the state definition's type
    /// </summary>
    [YamlMember]
    [ProtoMember(3)]
    [DataMember(Order = 3)]
    public virtual string Type { get; protected set; } = null!;

    /// <summary>
    /// Gets/sets the filter to apply to the state definition's input and output data
    /// </summary>
    [Newtonsoft.Json.JsonProperty(PropertyName = "stateDataFilter")]
    [System.Text.Json.Serialization.JsonPropertyName("stateDataFilter")]
    [YamlMember(Alias = "stateDataFilter")]
    [ProtoMember(5, Name = "stateDataFilter")]
    [DataMember(Order = 5, Name = "stateDataFilter")]
    public virtual StateDataFilterDefinition? DataFilter { get; set; }

    /// <summary>
    /// Gets/sets the object that represents the state definition's <see cref="EndDefinition"/>
    /// </summary>
    [YamlMember(Alias = "dataInputSchema")]
    [ProtoMember(8, Name = "dataInputSchema")]
    [DataMember(Order = 8, Name = "dataInputSchema")]
    [Newtonsoft.Json.JsonProperty(PropertyName = "dataInputSchema"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<DataInputSchemaDefinition, Uri>))]
    [System.Text.Json.Serialization.JsonPropertyName("dataInputSchema"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<DataInputSchemaDefinition, Uri>))]
    protected virtual OneOf<DataInputSchemaDefinition, Uri>? DataInputSchemaValue { get; set; }

    /// <summary>
    /// Gets/sets the state definition's <see cref="DataInputSchemaDefinition"/>
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    [YamlIgnore]
    [ProtoIgnore]
    [IgnoreDataMember]
    public virtual DataInputSchemaDefinition? DataInputSchema
    {
        get
        {
            if (this.DataInputSchemaValue?.T1Value == null
                && this.DataInputSchemaValue?.T2Value != null)
                return new() { SchemaUri = this.DataInputSchemaValue.T2Value };
            else
                return this.DataInputSchemaValue?.T1Value;
        }
        set
        {
            if (value == null)
                this.DataInputSchemaValue = null;
            else
                this.DataInputSchemaValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> referencing the state definition's <see cref="DataInputSchemaDefinition"/>
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    [YamlIgnore]
    [ProtoIgnore]
    [IgnoreDataMember]
    public virtual Uri? DataInputSchemaUri
    {
        get
        {
            return this.DataInputSchemaValue?.T2Value;
        }
        set
        {
            if (value == null)
                this.DataInputSchemaValue = null;
            else
                this.DataInputSchemaValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the configuration of the state definition's error handling
    /// </summary>
    [Newtonsoft.Json.JsonProperty(PropertyName = "onErrors")]
    [System.Text.Json.Serialization.JsonPropertyName("onErrors")]
    [YamlMember(Alias = "onErrors")]
    [ProtoMember(6, Name = "onErrors")]
    [DataMember(Order = 6, Name = "onErrors")]
    public virtual List<ErrorHandlerDefinition>? Errors { get; set; }

    /// <summary>
    /// Gets/sets the id of the state definition used to compensate the state definition
    /// </summary>
    [ProtoMember(9)]
    [DataMember(Order = 9)]
    public virtual string? CompensatedBy { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the state definition is used for compensating another state definition
    /// </summary>
    [ProtoMember(10)]
    [DataMember(Order = 10)]
    public virtual bool UsedForCompensation { get; set; }

    /// <summary>
    /// Gets/sets the state definition's metadata
    /// </summary>
    [ProtoMember(11)]
    [DataMember(Order = 11)]
    public virtual DynamicObject? Metadata { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the state definition's <see cref="TransitionDefinition"/>
    /// </summary>
    [ProtoMember(9997, Name = "transition")]
    [DataMember(Order = 9997, Name = "transition")]
    [YamlMember(Alias = "transition")]
    [Newtonsoft.Json.JsonProperty(PropertyName = "transition", Order = 999999998), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<TransitionDefinition, string>))]
    [System.Text.Json.Serialization.JsonPropertyName("transition"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<TransitionDefinition, string>))]
    protected virtual OneOf<TransitionDefinition, string>? TransitionValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the state definition's transition to another state definition upon completion
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    [YamlIgnore]
    [ProtoIgnore]
    [IgnoreDataMember]
    public virtual TransitionDefinition? Transition
    {
        get
        {
            if (this.TransitionValue?.T1Value == null
                && !string.IsNullOrWhiteSpace(this.TransitionValue?.T2Value))
                return new() { NextState = this.TransitionValue.T2Value };
            else
                return this.TransitionValue?.T1Value;
        }
        set
        {
            if (value == null)
                this.TransitionValue = null;
            else
                this.TransitionValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the name of the state definition to transition to upon completion
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    [YamlIgnore]
    [ProtoIgnore]
    [IgnoreDataMember]
    public virtual string? TransitionToStateName
    {
        get
        {
            return this.TransitionValue?.T2Value;
        }
        set
        {
            if (value == null)
                this.TransitionValue = null;
            else
                this.TransitionValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the state definition's <see cref="EndDefinition"/>
    /// </summary>
    [ProtoMember(9998, Name = "end")]
    [DataMember(Order = 9998, Name = "end")]
    [YamlMember(Alias = "end")]
    [Newtonsoft.Json.JsonProperty(PropertyName = "end", Order = 999999999), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<EndDefinition, bool>))]
    [System.Text.Json.Serialization.JsonPropertyName("end"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<EndDefinition, bool>))]
    protected virtual OneOf<EndDefinition, bool>? EndValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the state definition's transition to another state definition upon completion
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    [YamlIgnore]
    [ProtoIgnore]
    [IgnoreDataMember]
    public virtual EndDefinition? End
    {
        get
        {
            if (this.EndValue?.T1Value == null
                && (this.EndValue != null && this.EndValue.T2Value))
                return new() { };
            else
                return this.EndValue?.T1Value;
        }
        set
        {
            if (value == null)
                this.EndValue = null;
            else
                this.EndValue = value;
        }
    }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the state definition is the end of a logicial workflow path
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    [YamlIgnore]
    [ProtoIgnore]
    [IgnoreDataMember]
    public virtual bool IsEnd
    {
        get
        {
            if (this.EndValue == null)
                return false;
            else
                return this.EndValue.T2Value;
        }
        set
        {
            this.EndValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the state definition's extension properties
    /// </summary>
    [ProtoMember(9999)]
    [DataMember(Order = 9999)]
    [Newtonsoft.Json.JsonExtensionData]
    [System.Text.Json.Serialization.JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionProperties { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.Name;
    }

}
