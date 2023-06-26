namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IActionBuilder"/> interface
/// </summary>
public class ActionBuilder
    : IActionBuilder, IEventTriggerActionBuilder, IFunctionActionBuilder, ISubflowActionBuilder
{

    /// <summary>
    /// Initializes a new <see cref="ActionBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="ActionBuilder"/> belongs to</param>
    public ActionBuilder(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="ActionBuilder"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; set; }

    /// <summary>
    /// Gets the <see cref="ActionDefinition"/> to configure
    /// </summary>
    protected ActionDefinition Action { get; } = new ActionDefinition();

    /// <inheritdoc/>
    public virtual IActionBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Action.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IActionBuilder WithCondition(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression)) throw new ArgumentNullException(nameof(expression));
        this.Action.Condition = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IActionBuilder FromStateData(string expression)
    {
        this.Action.ActionDataFilter!.FromStateData = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IActionBuilder FilterResults(string expression)
    {
        this.Action.ActionDataFilter!.Results = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IActionBuilder ToStateData(string expression)
    {
        this.Action.ActionDataFilter!.ToStateData = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionActionBuilder Invoke(string function)
    {
        if (string.IsNullOrWhiteSpace(function)) throw new ArgumentNullException(nameof(function));
        this.Action.Function = new FunctionReference() { RefName = function };
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionActionBuilder Invoke(Action<IFunctionBuilder> functionSetup)
    {
        if (functionSetup == null) throw new ArgumentNullException(nameof(functionSetup));
        var function = this.Pipeline.AddFunction(functionSetup);
        this.Action.Function = new FunctionReference() { RefName = function.Name };
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionActionBuilder Invoke(FunctionDefinition function)
    {
        if (function == null) throw new ArgumentNullException(nameof(function));
        this.Pipeline.AddFunction(function);
        this.Action.Function = new FunctionReference() { RefName = function.Name };
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionActionBuilder WithArgument(string name, object value)
    {
        if (this.Action.Function!.Arguments == null) this.Action.Function.Arguments = new Dictionary<string, object>();
        this.Action.Function.Arguments[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionActionBuilder WithArguments(IDictionary<string, object> args)
    {
        this.Action.Function!.Arguments = args.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFunctionActionBuilder WithSelectionSet(string selectionSet)
    {
        if (string.IsNullOrWhiteSpace(selectionSet)) throw new ArgumentNullException(nameof(selectionSet));
        this.Action.Function!.SelectionSet = selectionSet;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventTriggerActionBuilder Consume(string e)
    {
        if (string.IsNullOrWhiteSpace(e)) throw new ArgumentNullException(nameof(e));
        this.Action.Event = new EventReference() { TriggerEventRef = e, ResultEventRef = string.Empty };
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventTriggerActionBuilder Consume(Action<IEventBuilder> eventSetup)
    {
        if (eventSetup == null) throw new ArgumentNullException(nameof(eventSetup));
        var e = this.Pipeline.AddEvent(eventSetup);
        this.Action.Event = new EventReference() { TriggerEventRef = e.Name, ResultEventRef = string.Empty };
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventTriggerActionBuilder Consume(EventDefinition e)
    {
        if (e == null)
            throw new ArgumentNullException(nameof(e));
        this.Pipeline.AddEvent(e);
        this.Action.Event = new EventReference() { TriggerEventRef = e.Name, ResultEventRef = string.Empty };
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventTriggerActionBuilder ThenProduce(string e)
    {
        this.Action.Event!.ResultEventRef = e;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventTriggerActionBuilder ThenProduce(Action<IEventBuilder> eventSetup)
    {
        var e = this.Pipeline.AddEvent(eventSetup);
        this.Action.Event!.TriggerEventRef = e.Name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventTriggerActionBuilder WithContextAttribute(string name, string value)
    {
        if (this.Action.Event!.ContextAttributes == null) this.Action.Event.ContextAttributes = new Dictionary<string, object>();
        this.Action.Event.ContextAttributes[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventTriggerActionBuilder WithContextAttributes(IDictionary<string, string> contextAttributes)
    {
        this.Action.Event!.ContextAttributes = contextAttributes.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
        return this;
    }

    /// <inheritdoc/>
    public virtual ISubflowActionBuilder Run(string workflowId, string version, string invocationMode = InvocationMode.Synchronous)
    {
        if (string.IsNullOrWhiteSpace(workflowId))  throw new ArgumentNullException(nameof(workflowId));
        this.Action.Subflow = new SubflowReference(workflowId, version, invocationMode);
        return this;
    }

    /// <inheritdoc/>
    public virtual ISubflowActionBuilder Run(string workflowId, string invocationMode = InvocationMode.Synchronous)
    {
        if (string.IsNullOrWhiteSpace(workflowId)) throw new ArgumentNullException(nameof(workflowId));
        return this.Run(workflowId, null!, invocationMode);
    }

    /// <inheritdoc/>
    public virtual ISubflowActionBuilder Synchronously()
    {
        this.Action.Subflow!.InvocationMode = InvocationMode.Synchronous;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISubflowActionBuilder Asynchronously()
    {
        this.Action.Subflow!.InvocationMode = InvocationMode.Asynchronous;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISubflowActionBuilder LatestVersion() => this.Version("latest");

    /// <inheritdoc/>
    public virtual ISubflowActionBuilder Version(string version)
    {
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentNullException(nameof(version));
        this.Action.Subflow!.Version = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual ActionDefinition Build() => this.Action;

}
