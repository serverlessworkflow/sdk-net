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

    /// <summary>
    /// Gets the start state definition
    /// </summary>
    /// <returns>The state definition the <see cref="WorkflowDefinition"/> starts with</returns>
    public virtual StateDefinition GetStartState()
    {
        var stateName = this.StartStateName;
        if (this.Start != null) stateName = this.Start.StateName;
        if (string.IsNullOrWhiteSpace(stateName)) return this.States.First();
        if (!this.TryGetState(stateName, out var state)) throw new NullReferenceException($"Failed to find a state definition with name '{state}'");
        return state;
    }

    /// <summary>
    /// Attempts to the start state definition
    /// </summary>
    /// <param name="state">The start state definition</param>
    /// <returns>A boolean indicating whether or not the <see cref="WorkflowDefinition"/>'s start state definition could be found</returns>
    public virtual bool TryGetStartState(out StateDefinition state)
    {
        state = this.GetStartState()!;
        return state != null;
    }

    /// <summary>
    /// Gets the start state definition
    /// </summary>
    /// <typeparam name="TState">The expected type of the <see cref="WorkflowDefinition"/>'s start state definition</typeparam>
    /// <returns>The start state definition</returns>
    public virtual TState? GetStartState<TState>() where TState : StateDefinition => this.GetStartState() as TState;

    /// <summary>
    /// Attempts to the start state definition
    /// </summary>
    /// <param name="state">The start state definition</param>
    /// <returns>A boolean indicating whether or not the <see cref="WorkflowDefinition"/>'s start state definition could be found</returns>
    public virtual bool TryGetStartState<TState>(out TState state)
        where TState : StateDefinition
    {
        state = this.GetStartState<TState>()!;
        return state != null;
    }

    /// <summary>
    /// Gets the state definition with the specified name
    /// </summary>
    /// <param name="name">The name of the state definition to get</param>
    /// <returns>The state definition with the specified name, if any</returns>
    public virtual StateDefinition? GetState(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        return this.States?.FirstOrDefault(s => s.Name == name);
    }

    /// <summary>
    /// Attempts to retrieve the state definition with the specified name
    /// </summary>
    /// <param name="name">The name of the state definition to retrieve</param>
    /// <param name="state">The state definition with the specified name, if any</param>
    /// <returns>A boolean indicating whether or not a state definition with the specified name could be found</returns>
    public virtual bool TryGetState(string name, out StateDefinition state)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        state = this.GetState(name)!;
        return state != null;
    }

    /// <summary>
    /// Gets the state definition with the specified name
    /// </summary>
    /// <typeparam name="TState">The expected type of the state definition with the specified name</typeparam>
    /// <param name="name">The name of the state definition to get</param>
    /// <returns>The state definition with the specified name, if any</returns>
    public virtual TState? GetState<TState>(string name)
        where TState : StateDefinition
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        return this.GetState(name) as TState;
    }

    /// <summary>
    /// Attempts to retrieve the state definition with the specified name
    /// </summary>
    /// <typeparam name="TState">The expected type of the state definition with the specified name</typeparam>
    /// <param name="name">The name of the state definition to retrieve</param>
    /// <param name="state">The state definition with the specified name, if any</param>
    /// <returns>A boolean indicating whether or not a state definition with the specified name could be found</returns>
    public virtual bool TryGetState<TState>(string name, out TState state)
        where TState : StateDefinition
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        state = this.GetState<TState>(name)!;
        return state != null;
    }

    /// <summary>
    /// Gets the <see cref="EventDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="EventDefinition"/> to get</param>
    /// <returns>The <see cref="EventDefinition"/> with the specified name, if any</returns>
    public virtual EventDefinition? GetEvent(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        return this.Events?.FirstOrDefault(e => e.Name == name);
    }

    /// <summary>
    /// Attempts to retrieve the <see cref="EventDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="EventDefinition"/> to retrieve</param>
    /// <param name="e">The <see cref="EventDefinition"/> with the specified name, if any</param>
    /// <returns>A boolean indicating whether or not a <see cref="EventDefinition"/> with the specified name could be found</returns>
    public virtual bool TryGetEvent(string name, out EventDefinition e)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        e = this.GetEvent(name)!;
        return e != null;
    }

    /// <summary>
    /// Gets the <see cref="FunctionDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="FunctionDefinition"/> to get</param>
    /// <returns>The <see cref="FunctionDefinition"/> with the specified name, if any</returns>
    public virtual FunctionDefinition? GetFunction(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        return this.Functions?.FirstOrDefault(e => e.Name == name);
    }

    /// <summary>
    /// Attempts to retrieve the <see cref="FunctionDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="FunctionDefinition"/> to retrieve</param>
    /// <param name="function">The <see cref="FunctionDefinition"/> with the specified name, if any</param>
    /// <returns>A boolean indicating whether or not a <see cref="FunctionDefinition"/> with the specified name could be found</returns>
    public virtual bool TryGetFunction(string name, out FunctionDefinition function)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        function = this.GetFunction(name)!;
        return function != null;
    }

    /// <summary>
    /// Gets the <see cref="AuthenticationDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to get</param>
    /// <returns>The <see cref="AuthenticationDefinition"/> with the specified name, if any</returns>
    public virtual AuthenticationDefinition? GetAuthentication(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        return this.Auth?.FirstOrDefault(e => e.Name == name);
    }

    /// <summary>
    /// Attempts to retrieve the <see cref="AuthenticationDefinition"/> with the specified name
    /// </summary>
    /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to retrieve</param>
    /// <param name="authentication">The <see cref="AuthenticationDefinition"/> with the specified name, if any</param>
    /// <returns>A boolean indicating whether or not a <see cref="AuthenticationDefinition"/> with the specified name could be found</returns>
    public virtual bool TryGetAuthentication(string name, out AuthenticationDefinition authentication)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        authentication = this.GetAuthentication(name)!;
        return authentication != null;
    }

    /// <inheritdoc/>
    public override string ToString() => $"{this.Id} {this.Version}";

    /// <summary>
    /// Creates a new <see cref="IWorkflowBuilder"/> used to build a new <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="id">The id of the <see cref="WorkflowDefinition"/> to create</param>
    /// <param name="name">The name of the <see cref="WorkflowDefinition"/> to create</param>
    /// <param name="version">The version of the <see cref="WorkflowDefinition"/> to create</param>
    /// <returns>A new <see cref="IWorkflowBuilder"/></returns>
    public static IWorkflowBuilder Create(string id, string name, string version)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentNullException(nameof(version));
        return new WorkflowBuilder()
            .WithId(id)
            .WithName(name)
            .WithVersion(version);
    }

}
