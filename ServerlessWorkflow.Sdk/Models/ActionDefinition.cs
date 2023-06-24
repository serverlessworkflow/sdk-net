namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the object used to define a workflow action
/// </summary>
[DataContract]
public class ActionDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the unique action definition name
    /// </summary>
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Alias = "name", Order = 1)]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets the <see cref="ActionDefinition"/>'s type
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string Type
    {
        get
        {
            if (this.Function != null)
                return ActionType.Function;
            else if (this.Event != null)
                return ActionType.Trigger;
            else if (this.Subflow != null)
                return ActionType.Subflow;
            else
                return string.Empty;
        }
    }

    /// <summary>
    /// Gets/sets a <see cref="OneOf{T1, T2}"/> that represents the function to invoke
    /// </summary>
    [DataMember(Order = 2, Name = "functionRef"), JsonPropertyOrder(2), JsonPropertyName("functionRef"), YamlMember(Alias = "functionRef", Order = 2)]
    [JsonConverter(typeof(OneOfConverter<FunctionReference, string>))]
    protected virtual OneOf<FunctionReference, string>? FunctionValue { get; set; }

    /// <summary>
    /// Gets the object used to configure the reference of the function to invoke
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual FunctionReference? Function
    {
        get
        {
            if (this.FunctionValue?.T1Value == null
                && !string.IsNullOrWhiteSpace(this.FunctionValue?.T2Value))
                    return new FunctionReference() { RefName = this.FunctionValue.T2Value };
                else
                    return this.FunctionValue?.T1Value;
        }
        set
        {
            if (value == null)
                this.FunctionValue = null;
            else
                this.FunctionValue = value;
        }
    }

    /// <summary>
    /// Gets the object used to configure the reference of the event to produce or consume
    /// </summary>
    [DataMember(Order = 3, Name = "eventRef"), JsonPropertyOrder(3), JsonPropertyName("eventRef"), YamlMember(Alias = "eventRef", Order = 3)]
    public virtual EventReference? Event { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="OneOf{T1, T2}"/> that references a subflow to run
    /// </summary>
    [DataMember(Order = 4, Name = "eventRef"), JsonPropertyOrder(4), JsonPropertyName("eventRef"), YamlMember(Alias = "eventRef", Order = 4)]
    [JsonConverter(typeof(OneOfConverter<SubflowReference, string>))]
    protected virtual OneOf<SubflowReference, string>? SubflowValue { get; set; }

    /// <summary>
    /// Gets the object used to configure the reference of the subflow to run
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual SubflowReference? Subflow
    {
        get
        {
            if (this.SubflowValue?.T1Value == null
                && !string.IsNullOrWhiteSpace(this.SubflowValue?.T2Value))
            {
                var components = this.SubflowValue.T2Value.Split(':', StringSplitOptions.RemoveEmptyEntries);
                var id = components.First();
                var version = null as string;
                if (components.Length > 1)
                {
                    version = components.Last();
                    id = this.SubflowValue.T2Value[..^(version.Length + 1)];
                }
                return new() { WorkflowId = id, Version = version };
            }
            return this.SubflowValue?.T1Value;
        }
        set
        {
            if (value == null)
                this.SubflowValue = null;
            else
                this.SubflowValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the name of the workflow retry definition to use. If not defined uses the default runtime retry definition
    /// </summary>
    [DataMember(Order = 5, Name = "retryRef"), JsonPropertyOrder(5), JsonPropertyName("retryRef"), YamlMember(Alias = "retryRef", Order = 5)]
    public virtual string? RetryRef { get; set; }

    /// <summary>
    /// Gets/sets alist containing references to defined <see cref="ErrorHandlerDefinition"/>s for which the action should not be retried. Used only when `<see cref="WorkflowDefinition.AutoRetries"/>` is set to `true`
    /// </summary>
    [DataMember(Order = 6, Name = "nonRetryableErrors"), JsonPropertyOrder(6), JsonPropertyName("nonRetryableErrors"), YamlMember(Alias = "nonRetryableErrors", Order = 6)]
    public virtual List<string>? NonRetryableErrors { get; set; }

    /// <summary>
    /// Gets/sets alist containing references to defined <see cref="ErrorHandlerDefinition"/>s for which the action should be retried. Used only when `<see cref="WorkflowDefinition.AutoRetries"/>` is set to `false`
    /// </summary>
    [DataMember(Order = 7, Name = "retryableErrors"), JsonPropertyOrder(7), JsonPropertyName("retryableErrors"), YamlMember(Alias = "retryableErrors", Order = 7)]
    public virtual List<string>? RetryableErrors { get; set; }

    /// <summary>
    /// Gets/sets an object used to define the way to filter the action's data
    /// </summary>
    [DataMember(Order = 8, Name = "actionDataFilter"), JsonPropertyOrder(8), JsonPropertyName("actionDataFilter"), YamlMember(Alias = "actionDataFilter", Order = 8)]
    public ActionDataFilterDefinition? ActionDataFilter { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="ActionDefinition"/>'s execution delay configuration
    /// </summary>
    [DataMember(Order = 9, Name = "sleep"), JsonPropertyOrder(9), JsonPropertyName("sleep"), YamlMember(Alias = "sleep", Order = 9)]
    public virtual ActionExecutionDelayDefinition? Sleep { get; set; }

    /// <summary>
    /// Gets/sets an expression to be evaluated positively as a condition for the <see cref="ActionDefinition"/> to execute.
    /// </summary>
    [DataMember(Order = 10, Name = "condition"), JsonPropertyOrder(10), JsonPropertyName("condition"), YamlMember(Alias = "condition", Order = 10)]
    public virtual string? Condition { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="ActionDefinition"/>'s extension properties
    /// </summary>
    [DataMember(Order = 11, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string? ToString()
    {
        if (string.IsNullOrWhiteSpace(this.Name))
            return base.ToString();
        else
            return this.Name;
    }

}