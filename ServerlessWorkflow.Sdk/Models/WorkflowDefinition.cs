using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using System.Dynamic;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a Serverless Workflow
/// </summary>
[DataContract]
public class WorkflowDefinition
    : IExtensible, IMetadata
{

    /// <summary>
    /// Gets/sets the workflow definition's unique identifier
    /// </summary>
    [DataMember(Order = 1, Name = "id"), JsonPropertyName("id"), YamlMember(Alias = "id")]
    public virtual string? Id { get; set; }

    /// <summary>
    /// Gets/sets the workflow definition's domain-specific workflow identifier
    /// </summary>
    [DataMember(Order = 2, Name = "key"), JsonPropertyName("key"), YamlMember(Alias = "key")]
    public virtual string? Key { get; set; }

    /// <summary>
    /// Gets/sets the workflow definition's name
    /// </summary>
    [DataMember(Order = 3, Name = "name"), JsonPropertyName("name"), YamlMember(Alias = "name")]
    [Required, MinLength(1)]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets the workflow definition's description
    /// </summary>
    [DataMember(Order = 4, Name = "description"), JsonPropertyName("description"), YamlMember(Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets the workflow definition's version
    /// </summary>
    [DataMember(Order = 5, Name = "version", IsRequired = true), JsonPropertyName("version"), YamlMember(Alias = "version", ScalarStyle = ScalarStyle.SingleQuoted), Required, MinLength(1)]
    public virtual string Version { get; set; } = null!;

    /// <summary>
    /// Gets/sets the <see cref="System.Version"/> of the Serverless Workflow schema to use
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 6, Name = "specVersion", IsRequired = true), JsonPropertyName("specVersion"), YamlMember(Alias = "specVersion", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string SpecVersion { get; set; } = null!;

    /// <summary>
    /// Gets/sets the language the workflow definition's expressions are expressed in
    /// </summary>
    [Required, DefaultValue(RuntimeExpressionLanguage.JQ)]
    [DataMember(Order = 7, Name = "expressionLang"), JsonPropertyName("expressionLang"), YamlMember(Alias = "expressionLang")]
    public virtual string ExpressionLanguage { get; set; } = RuntimeExpressionLanguage.JQ;

    /// <summary>
    /// Gets/sets alist containing the workflow definition's annotations
    /// </summary>
    [DataMember(Order = 8, Name = "annotations"), JsonPropertyName("annotations"), YamlMember(Alias = "annotations")]
    public virtual List<string>? Annotations { get; set; }

    /// <summary>
    /// Gets/sets the workflow definition's metadata
    /// </summary>
    [DataMember(Order = 9, Name = "metadata"), JsonPropertyName("metadata"), YamlMember(Alias = "metadata")]
    public virtual DynamicMapping? Metadata { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's data input <see cref="JSchema"/>
    /// </summary>
    [DataMember(Order = 10, Name = "dataInputSchema"), JsonPropertyName("dataInputSchema"), YamlMember(Alias = "dataInputSchema")]
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
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's secrets
    /// </summary>
    [DataMember(Order = 11, Name = "secrets"), JsonPropertyName("secrets"), YamlMember(Alias = "secrets")]
    [JsonConverter(typeof(OneOfConverter<List<string>, Uri>))]
    protected virtual OneOf<List<string>, Uri>? SecretsValue { get; set; }

    /// <summary>
    /// Gets/sets alist containing the workflow definition's secrets
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual List<string>? Secrets
    {
        get
        {
            return this.SecretsValue?.T1Value;
        }
        set
        {
            if (value == null) this.SecretsValue = null;
            else this.SecretsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an uri pointing at a file containing the workflow definition's secrets
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri? SecretsUri
    {
        get
        {
            return this.SecretsValue?.T2Value;
        }
        set
        {
            if (value == null) this.SecretsValue = null;
            else this.SecretsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's authentication definitions
    /// </summary>
    [DataMember(Order = 12, Name = "auth"), JsonPropertyName("auth"), YamlMember(Alias = "auth")]
    [JsonConverter(typeof(OneOfConverter<List<AuthenticationDefinition>, Uri>))]
    protected virtual OneOf<List<AuthenticationDefinition>, Uri>? AuthValue { get; set; }

    /// <summary>
    /// Gets/sets a list containing the workflow definition's authentication definition collection
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual List<AuthenticationDefinition>? Auth
    {
        get
        {
            return this.AuthValue?.T1Value;
        }
        set
        {
            if (value == null)
                this.AuthValue = null;
            else
                this.AuthValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an uri pointing at a file containing the workflow definition's authentication definition collection
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri? AuthUri
    {
        get
        {
            return this.AuthValue?.T2Value;
        }
        set
        {
            if (value == null)
                this.AuthValue = null;
            else
                this.AuthValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's constants
    /// </summary>
    [DataMember(Order = 13, Name = "constants"), JsonPropertyName("constants"), YamlMember(Alias = "constants")]
    [JsonConverter(typeof(OneOfConverter<DynamicMapping, Uri>))]
    protected virtual OneOf<DynamicMapping, Uri>? ConstantsValue { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s constants
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual DynamicMapping? Constants
    {
        get
        {
            return this.ConstantsValue?.T1Value;
        }
        set
        {
            if (value == null) this.ConstantsValue = null;
            else this.ConstantsValue = new(value);
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s constants
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri? ConstantsUri
    {
        get
        {
            return this.ConstantsValue?.T2Value;
        }
        set
        {
            if (value == null) this.ConstantsValue = null;
            else this.ConstantsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's <see cref="EventDefinition"/> collection
    /// </summary>
    [DataMember(Order = 14, Name = "events"), JsonPropertyName("events"), YamlMember(Alias = "events")]
    [JsonConverter(typeof(OneOfConverter<List<EventDefinition>, Uri>))]
    protected virtual OneOf<List<EventDefinition>, Uri>? EventsValue { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/>s
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual List<EventDefinition>? Events
    {
        get
        {
            return this.EventsValue?.T1Value;
        }
        set
        {
            if (value == null) this.EventsValue = null;
            else this.EventsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/> collection
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri? EventsUri
    {
        get
        {
            return this.EventsValue?.T2Value;
        }
        set
        {
            if (value == null) this.EventsValue = null;
            else this.EventsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's <see cref="FunctionDefinition"/> collection
    /// </summary>
    [DataMember(Order = 15, Name = "functions"), JsonPropertyName("functions"), YamlMember(Alias = "functions")]
    [JsonConverter(typeof(OneOfConverter<List<FunctionDefinition>, Uri>))]
    protected virtual OneOf<List<FunctionDefinition>, Uri>? FunctionsValue { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="FunctionDefinition"/>s
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual List<FunctionDefinition>? Functions
    {
        get
        {
            return this.FunctionsValue?.T1Value;
        }
        set
        {
            if (value == null) this.FunctionsValue = null;
            else this.FunctionsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="FunctionDefinition"/> collection
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri? FunctionsUri
    {
        get
        {
            return this.FunctionsValue?.T2Value;
        }
        set
        {
            if (value == null) this.FunctionsValue = null;
            else this.FunctionsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the object used to configure the workflow definition's execution timeouts
    /// </summary>
    [DataMember(Order = 16, Name = "timeouts"), JsonPropertyName("timeouts"), YamlMember(Alias = "timeouts")]
    [JsonConverter(typeof(OneOfConverter<WorkflowTimeoutDefinition, Uri>))]
    public virtual OneOf<WorkflowTimeoutDefinition, Uri>? TimeoutsValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s execution timeouts
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual WorkflowTimeoutDefinition? Timeouts
    {
        get
        {
            return this.TimeoutsValue?.T1Value;
        }
        set
        {
            if (value == null) this.TimeoutsValue = null;
            else this.TimeoutsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="Uri"/> pointing at the <see cref="WorkflowDefinition"/>'s <see cref="WorkflowTimeoutDefinition"/>
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri? TimeoutsUri
    {
        get
        {
            return this.TimeoutsValue?.T2Value;
        }
        set
        {
            if (value == null) this.TimeoutsValue = null;
            else this.TimeoutsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's <see cref="RetryDefinition"/> collection
    /// </summary>
    [DataMember(Order = 17, Name = "retries"), JsonPropertyName("retries"), YamlMember(Alias = "retries")]
    [JsonConverter(typeof(OneOfConverter<List<RetryDefinition>, Uri>))]
    protected virtual OneOf<List<RetryDefinition>, Uri>? RetriesValue { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="RetryDefinition"/>s
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual List<RetryDefinition>? Retries
    {
        get
        {
            return this.RetriesValue?.T1Value;
        }
        set
        {
            if (value == null) this.RetriesValue = null;
            else this.RetriesValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="RetryDefinition"/> collection
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri? RetriesUri
    {
        get
        {
            return this.RetriesValue?.T2Value;
        }
        set
        {
            if (value == null) this.RetriesValue = null;
            else this.RetriesValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that defines the workflow definition's start
    /// </summary>
    [DataMember(Order = 18, Name = "start"), JsonPropertyName("start"), YamlMember(Alias = "start")]
    [JsonConverter(typeof(OneOfConverter<StartDefinition, string>))]
    protected virtual OneOf<StartDefinition, string>? StartValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s <see cref="StartDefinition"/>
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual StartDefinition? Start
    {
        get
        {
            return this.StartValue?.T1Value;
        }
        set
        {
            if (value == null) this.StartValue = null;
            else this.StartValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the name of the <see cref="WorkflowDefinition"/>'s start state definition
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? StartStateName
    {
        get
        {
            return this.StartValue?.T2Value;
        }
        set
        {
            if (value == null) this.StartValue = null;
            else this.StartValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="IEnumerable{T}"/> containing the workflow definition's state definitions
    /// </summary>
    [DataMember(Order = 19, Name = "states"), JsonPropertyName("states"), YamlMember(Alias = "states")]
    public virtual List<StateDefinition> States { get; set; } = new();

    /// <summary>
    /// Gets/sets a boolean indicating whether or not actions should automatically be retried on unchecked errors. Defaults to false.
    /// </summary>
    [DataMember(Order = 20, Name = "autoRetries"), JsonPropertyName("autoRetries"), YamlMember(Alias = "autoRetries")]
    public virtual bool AutoRetries { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to keep instances of the workflow definition active even if there are no active execution paths. Instance can be terminated via 'terminate end definition' or reaching defined 'execTimeout'
    /// </summary>
    [DataMember(Order = 21, Name = "keepActive"), JsonPropertyName("keepActive"), YamlMember(Alias = "keepActive")]
    public virtual bool KeepActive { get; set; } = false;

    /// <summary>
    /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the workflow definition's <see cref="ExtensionDefinition"/>s
    /// </summary>
    [DataMember(Order = 22, Name = "extensions"), JsonPropertyName("extensions"), YamlMember(Alias = "extensions")]
    public virtual OneOf<List<ExtensionDefinition>, Uri>? ExtensionsValue { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="ExtensionDefinition"/> collection
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual List<ExtensionDefinition>? Extensions
    {
        get
        {
            return this.ExtensionsValue?.T1Value;
        }
        set
        {
            if (value == null) this.ExtensionsValue = null;
            else this.ExtensionsValue = value;
        }
    }

    /// <summary>
    /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="ExtensionDefinition"/> collection
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri? ExtensionsUri
    {
        get
        {
            return this.ExtensionsValue?.T2Value;
        }
        set
        {
            if (value == null) this.ExtensionsValue = null;
            else this.ExtensionsValue = value;
        }
    }

    /// <inheritdoc/>
    [DataMember(Order = 23, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => $"{this.Name} {this.Version}";

}
