using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a <see href="https://github.com/serverlessworkflow/specification/blob/main/specification.md#workflow-states">workflow state</see>
/// </summary>
[Discriminator(nameof(Type))]
[DataContract, KnownType(typeof(ExtensionStateDefinition)), KnownType(typeof(InjectStateDefinition))]
[JsonConverter(typeof(AbstractClassConverterFactory))]
public abstract class StateDefinition
    : IMetadata, IExtensible
{

    /// <summary>
    /// Initializes a new state definition
    /// </summary>
    protected StateDefinition() { }

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
    [DataMember(Order = 1, Name = "id"), JsonPropertyOrder(1), JsonPropertyName("id"), YamlMember(Alias = "id", Order = 1)]
    public virtual string? Id { get; set; }

    /// <summary>
    /// Gets/sets the state definition's id
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 2, Name = "name", IsRequired = true), JsonPropertyOrder(2), JsonPropertyName("name"), YamlMember(Alias = "name", Order = 2)]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets the state definition's type
    /// </summary>
    [DataMember(Order = 3, Name = "type"), JsonPropertyOrder(3), JsonPropertyName("type"), YamlMember(Alias = "type", Order = 3)]
    public virtual string Type { get; protected set; } = null!;

    /// <summary>
    /// Gets/sets the filter to apply to the state definition's input and output data
    /// </summary>
    [DataMember(Order = 4, Name = "stateDataFilter"), JsonPropertyOrder(4), JsonPropertyName("stateDataFilter"), YamlMember(Alias = "stateDataFilter", Order = 4)]
    public virtual StateDataFilterDefinition? DataFilter { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's data input <see cref="JSchema"/>
    /// </summary>
    [DataMember(Order = 5, Name = "dataInputSchema"), JsonPropertyOrder(5), JsonPropertyName("dataInputSchema"), YamlMember(Alias = "dataInputSchema", Order = 5)]
    [JsonConverter(typeof(OneOfConverter<DataInputSchemaDefinition, Uri>))]
    protected virtual OneOf<DataInputSchemaDefinition, Uri>? DataInputSchemaValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the workflow definition's data input schema
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual DataInputSchemaDefinition? DataInputSchema
    {
        get
        {
            return this.DataInputSchemaValue?.T1Value;
        }
        set
        {
            if (value == null) this.DataInputSchemaValue = null;
            else this.DataInputSchemaValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an uri pointing at the workflow definition's input data schema
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
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
    [DataMember(Order = 90, Name = "onErrors"), JsonPropertyOrder(90), JsonPropertyName("onErrors"), YamlMember(Alias = "onErrors", Order = 90)]
    public virtual List<ErrorHandlerDefinition>? Errors { get; set; }

    /// <summary>
    /// Gets/sets the id of the state definition used to compensate the state definition
    /// </summary>
    [DataMember(Order = 91, Name = "compensatedBy"), JsonPropertyOrder(91), JsonPropertyName("compensatedBy"), YamlMember(Alias = "compensatedBy", Order = 91)]
    public virtual string? CompensatedBy { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the state definition is used for compensating another state definition
    /// </summary>
    [DataMember(Order = 92, Name = "usedForCompensation"), JsonPropertyOrder(92), JsonPropertyName("usedForCompensation"), YamlMember(Alias = "usedForCompensation", Order = 92)]
    public virtual bool UsedForCompensation { get; set; }

    /// <summary>
    /// Gets/sets the state definition's metadata, if any
    /// </summary>
    [DataMember(Order = 93, Name = "metadata"), JsonPropertyOrder(93), JsonPropertyName("metadata"), YamlMember(Alias = "metadata", Order = 93)]
    public virtual DynamicMapping? Metadata { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the state definition's <see cref="TransitionDefinition"/>
    /// </summary>
    [DataMember(Order = 94, Name = "transition"), JsonPropertyOrder(94), JsonPropertyName("transition"), YamlMember(Alias = "transition", Order = 94)]
    [JsonConverter(typeof(OneOfConverter<TransitionDefinition, string>))]
    protected virtual OneOf<TransitionDefinition, string>? TransitionValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the state definition's transition to another state definition upon completion
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual TransitionDefinition? Transition
    {
        get
        {
            if (this.TransitionValue?.T1Value == null && !string.IsNullOrWhiteSpace(this.TransitionValue?.T2Value)) return new() { NextState = this.TransitionValue.T2Value };
            else return this.TransitionValue?.T1Value;
        }
        set
        {
            if (value == null) this.TransitionValue = null;
            else this.TransitionValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the name of the state definition to transition to upon completion
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? TransitionToStateName
    {
        get
        {
            return this.TransitionValue?.T2Value;
        }
        set
        {
            if (value == null) this.TransitionValue = null;
            else this.TransitionValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an object used to configure the state definition's end, if any
    /// </summary>
    [DataMember(Order = 95, Name = "end"), JsonPropertyOrder(95), JsonPropertyName("end"), YamlMember(Alias = "end", Order = 95)]
    [JsonConverter(typeof(OneOfConverter<EndDefinition, bool>))]
    protected virtual OneOf<EndDefinition, bool>? EndValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the state definition's transition to another state definition upon completion
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual EndDefinition? End
    {
        get
        {
            if (this.EndValue?.T1Value == null && (this.EndValue != null && this.EndValue.T2Value)) return new() { };
            else return this.EndValue?.T1Value;
        }
        set
        {
            if (value == null) this.EndValue = null;
            else this.EndValue = value;
        }
    }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the state definition is the end of a logicial workflow path
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual bool IsEnd
    {
        get
        {
            if (this.EndValue == null) return false;
            else return this.EndValue.T2Value;
        }
        set
        {
            this.EndValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the state definition's extension properties
    /// </summary>
    [DataMember(Order = 999, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => this.Name;

}
