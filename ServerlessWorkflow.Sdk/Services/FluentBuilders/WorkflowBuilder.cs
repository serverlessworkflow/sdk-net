namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowBuilder"/> interface
/// </summary>
public class WorkflowBuilder
    : MetadataContainerBuilder<IWorkflowBuilder>, IWorkflowBuilder
{

    /// <summary>
    /// Initializes a new <see cref="WorkflowBuilder"/>
    /// </summary>
    public WorkflowBuilder()
    {
        this.Pipeline = new PipelineBuilder(this);
    }

    /// <summary>
    /// Gets the workflow definition to configure
    /// </summary>
    protected WorkflowDefinition Workflow { get; } = new WorkflowDefinition();

    /// <summary>
    /// Gets the service used to build the workflow definition's <see cref="StartDefinition"/> chart
    /// </summary>
    protected IPipelineBuilder Pipeline { get; }

    /// <inheritdoc/>
    public override IDictionary<string, object>? Metadata
    {
        get
        {
            return this.Workflow.Metadata;
        }
        protected set
        {
            this.Workflow.Metadata = value;
        }
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
        this.Workflow.Key = key;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithId(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        this.Workflow.Id = id;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Workflow.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithDescription(string description)
    {
        this.Workflow.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentNullException(nameof(version));
        this.Workflow.Version = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithSpecVersion(string specVersion)
    {
        if (string.IsNullOrWhiteSpace(specVersion))
            throw new ArgumentNullException(nameof(specVersion));
        this.Workflow.SpecVersion = specVersion;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithDataInputSchema(Uri uri)
    {
        this.Workflow.DataInputSchemaUri = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithDataInputSchema(JsonSchema schema)
    {
        this.Workflow.DataInputSchema = new DataInputSchemaDefinition() { Schema = schema } ?? throw new ArgumentNullException(nameof(schema));
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithAnnotation(string annotation)
    {
        if (string.IsNullOrWhiteSpace(annotation)) throw new ArgumentNullException(nameof(annotation));
        if (this.Workflow.Annotations == null) this.Workflow.Annotations = new();
        this.Workflow.Annotations.Add(annotation);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder UseExpressionLanguage(string language)
    {
        if (string.IsNullOrWhiteSpace(language)) throw new ArgumentNullException(nameof(language));
        this.Workflow.ExpressionLanguage = language;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder UseJq() => this.UseExpressionLanguage("jq");

    /// <inheritdoc/>
    public virtual IWorkflowBuilder WithExecutionTimeout(Action<IWorkflowExecutionTimeoutBuilder> timeoutSetup)
    {
        var builder = new WorkflowExecutionTimeoutBuilder(this.Pipeline);
        timeoutSetup(builder);
        //todo: this.Workflow.ExecutionTimeout = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder KeepActive(bool keepActive = true)
    {
        this.Workflow.KeepActive = keepActive;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder ImportConstantsFrom(Uri uri)
    {
        this.Workflow.ConstantsUri = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder UseConstants(object constants)
    {
        if (constants == null) throw new ArgumentNullException(nameof(constants));
        this.Workflow.Constants = constants is IDictionary<string, object> dico ? dico.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) : Serialization.Serializer.Json.Deserialize<Dictionary<string, object>>(Serialization.Serializer.Json.Serialize(constants))!;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddConstant(string name, object value)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (this.Workflow.Constants == null) this.Workflow.Constants = new Dictionary<string, object>();
        this.Workflow.Constants[name] = value ?? throw new ArgumentNullException(nameof(value));
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder UseSecrets(IEnumerable<string> secrets)
    {
        this.Workflow.Secrets = secrets?.ToList()!;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddSecret(string secret)
    {
        if (this.Workflow.Secrets == null) this.Workflow.Secrets = new();
        this.Workflow.Secrets.Add(secret);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder ImportEventsFrom(Uri uri)
    {
        this.Workflow.EventsUri = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddEvent(EventDefinition e)
    {
        if (e == null) throw new ArgumentNullException(nameof(e));
        if (this.Workflow.Events == null) this.Workflow.Events = new();
        if (this.Workflow.Events.Any(ed => ed.Name == e.Name)) throw new ArgumentException($"The workflow already defines an event with the specified name '{e.Name}'", nameof(e));
        this.Workflow.Events.Add(e);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddEvent(Action<IEventBuilder> eventSetup)
    {
        if (eventSetup == null) throw new ArgumentNullException(nameof(eventSetup));
        var builder = new EventBuilder();
        eventSetup(builder);
        return this.AddEvent(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder ImportFunctionsFrom(Uri uri)
    {
        this.Workflow.FunctionsUri = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddFunction(FunctionDefinition function)
    {
        if (function == null) throw new ArgumentNullException(nameof(function));
        if (this.Workflow.Functions == null) this.Workflow.Functions = new();
        if (this.Workflow.Functions.Any(fd => fd.Name == function.Name)) throw new ArgumentException($"The workflow already defines a function with the specified name '{function.Name}'", nameof(function));
        this.Workflow.Functions.Add(function);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddFunction(Action<IFunctionBuilder> functionSetup)
    {
        if (functionSetup == null) throw new ArgumentNullException(nameof(functionSetup));
        var builder = new FunctionBuilder(this);
        functionSetup(builder);
        return this.AddFunction(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder ImportRetryStrategiesFrom(Uri uri)
    {
        this.Workflow.RetriesUri = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddRetryStrategy(RetryDefinition strategy)
    {
        if (strategy == null) throw new ArgumentNullException(nameof(strategy));
        if (this.Workflow.Retries == null) this.Workflow.Retries = new();
        if (this.Workflow.Retries.Any(rs => rs.Name == strategy.Name)) throw new ArgumentException($"The workflow already defines a function with the specified name '{strategy.Name}'", nameof(strategy));
        this.Workflow.Retries.Add(strategy);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddRetryStrategy(Action<IRetryStrategyBuilder> retryStrategySetup)
    {
        if (retryStrategySetup == null) throw new ArgumentNullException(nameof(retryStrategySetup));
        var builder = new RetryStrategyBuilder();
        retryStrategySetup(builder);
        return this.AddRetryStrategy(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder ImportAuthenticationDefinitionsFrom(Uri uri)
    {
        this.Workflow.AuthUri = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder UseAuthenticationDefinitions(params AuthenticationDefinition[] authenticationDefinitions)
    {
        if (authenticationDefinitions == null) throw new ArgumentNullException(nameof(authenticationDefinitions));
        this.Workflow.Auth = authenticationDefinitions.ToList();
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddAuthentication(AuthenticationDefinition authenticationDefinition)
    {
        if (authenticationDefinition == null) throw new ArgumentNullException(nameof(authenticationDefinition));
        if (this.Workflow.Auth == null) this.Workflow.Auth = new();
        this.Workflow.Auth.Add(authenticationDefinition);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddBasicAuthentication(string name, Action<IBasicAuthenticationBuilder> configurationAction)
    {
        var builder = new BasicAuthenticationBuilder();
        builder.WithName(name);
        configurationAction(builder);
        return AddAuthentication(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddBearerAuthentication(string name, Action<IBearerAuthenticationBuilder> configurationAction)
    {
        var builder = new BearerAuthenticationBuilder();
        builder.WithName(name);
        configurationAction(builder);
        return AddAuthentication(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowBuilder AddOAuth2Authentication(string name, Action<IOAuth2AuthenticationBuilder> configurationAction)
    {
        var builder = new OAuth2AuthenticationBuilder();
        builder.WithName(name);
        configurationAction(builder);
        return AddAuthentication(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IPipelineBuilder StartsWith(StateDefinition state)
    {
        if (state == null) throw new ArgumentNullException(nameof(state));
        this.Pipeline.AddState(state);
        this.Workflow.StartStateName = state.Name;
        return this.Pipeline;
    }

    /// <inheritdoc/>
    public virtual IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
    {
        if (stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
        var state = this.Pipeline.AddState(stateSetup);
        this.Workflow.StartStateName = state.Name;
        return this.Pipeline;
    }

    /// <inheritdoc/>
    public virtual IPipelineBuilder StartsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
        return this.StartsWith(flow => stateSetup(flow).WithName(name));
    }

    /// <inheritdoc/>
    public virtual IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup, Action<IScheduleBuilder> scheduleSetup)
    {
        if (stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
        if (scheduleSetup == null) throw new ArgumentNullException(nameof(scheduleSetup));
        var state = this.Pipeline.AddState(stateSetup);
        var schedule = new ScheduleBuilder();
        scheduleSetup(schedule);
        this.Workflow.Start = new() { StateName = state.Name, Schedule = schedule.Build() };
        return this.Pipeline;
    }

    /// <inheritdoc/>
    public virtual IPipelineBuilder StartsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup, Action<IScheduleBuilder> scheduleSetup)
    {
        if (stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
        if (scheduleSetup == null) throw new ArgumentNullException(nameof(scheduleSetup));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
        return this.StartsWith(flow => stateSetup(flow).WithName(name), scheduleSetup);
    }

    /// <inheritdoc/>
    public virtual WorkflowDefinition Build()
    {
        this.Workflow.States = this.Pipeline.Build().ToList();
        return this.Workflow;
    }

}
