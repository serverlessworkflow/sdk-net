namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a reference to an <see cref="EventDefinition"/>
/// </summary>
[DataContract]
public class EventReference
    : IExtensible
{

    /// <summary>
    /// Gets the name of the event to produce
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "triggerEventRef", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("triggerEventRef"), YamlMember(Alias = "triggerEventRef", Order = 1)]
    public virtual string TriggerEventRef { get; set; } = null!;

    /// <summary>
    /// Gets the name of the event to consume
    /// </summary>
    [DataMember(Order = 2, Name = "resultEventRef"), JsonPropertyOrder(2), JsonPropertyName("resultEventRef"), YamlMember(Alias = "resultEventRef", Order = 2)]
    public virtual string? ResultEventRef { get; set; }

    /// <summary>
    /// Gets/sets the data to become the cloud event's payload. 
    /// If string type, an expression which selects parts of the states data output to become the data (payload) of the event referenced by '<see cref="ProduceEvent"/>'. 
    /// If object type, a custom object to become the data (payload) of the event referenced by '<see cref="ProduceEvent"/>'.
    /// </summary>
    [DataMember(Order = 3, Name = "data"), JsonPropertyOrder(3), JsonPropertyName("data"), YamlMember(Alias = "data", Order = 3)]
    [JsonConverter(typeof(OneOfConverter<IDictionary<string, object>, string>))]
    protected virtual OneOf<IDictionary<string, object>, string>? DataValue { get; set; }

    /// <summary>
    /// Gets/sets a custom object to become the data (payload) of the event referenced by the 'triggerEventRef'
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual IDictionary<string, object>? Data
    {
        get
        {
            return this.DataValue?.T1Value;
        }
        set
        {
            if (value == null)
                this.DataValue = null;
            else
                this.DataValue = new(value);
        }
    }

    /// <summary>
    /// Gets/sets an expression which selects parts of the states data output to become the data (payload) of the event referenced by 'triggerEventRef'
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? DataExpression
    {
        get
        {
            return this.DataValue?.T2Value;
        }
        set
        {
            if (value == null)
                this.DataValue = null;
            else
                this.DataValue = value;
        }
    }

    /// <summary>
    /// Gets/sets additional extension context attributes to the produced event
    /// </summary>
    [DataMember(Order = 4, Name = "contextAttributes"), JsonPropertyOrder(4), JsonPropertyName("contextAttributes"), YamlMember(Alias = "contextAttributes", Order = 4)]
    public virtual IDictionary<string, object>? ContextAttributes { get; set; }

    /// <summary>
    /// Gets the maximum amount of time to wait for the result event. If not defined it be set to the actionExecutionTimeout
    /// </summary>
    [DataMember(Order = 5, Name = "consumeEventTimeout"), JsonPropertyOrder(5), JsonPropertyName("consumeEventTimeout"), YamlMember(Alias = "consumeEventTimeout", Order = 5)]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))]
    public virtual TimeSpan? ConsumeEventTimeout { get; set; }

    /// <summary>
    /// Gets/sets the reference event's 'InvocationMode'. Default is 'InvocationMode.Synchronous'.
    /// </summary>
    /// <remarks>
    /// Default value of this property is sync, meaning that workflow execution should wait until the function completes (the result event is received).<para></para>
    /// If set to async, workflow execution should just produce the trigger event and should not wait for the result event
    /// </remarks>
    [DefaultValue(Sdk.InvocationMode.Synchronous)]
    [DataMember(Order = 6, Name = "invoke"), JsonPropertyOrder(6), JsonPropertyName("invoke"), YamlMember(Alias = "invoke", Order = 6)]
    public virtual string InvocationMode { get; set; } = Sdk.InvocationMode.Synchronous;

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="ActionDefinition"/>'s extension properties
    /// </summary>
    [DataMember(Order = 7, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => $"[{this.TriggerEventRef}] => [{this.ResultEventRef}]";

}