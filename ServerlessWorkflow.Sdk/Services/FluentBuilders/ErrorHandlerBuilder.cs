namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IErrorHandlerBuilder"/> interface
/// </summary>
public class ErrorHandlerBuilder
    : IErrorHandlerBuilder
{

    /// <summary>
    /// Initializes a new <see cref="ErrorHandlerBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="ErrorHandlerBuilder"/> belongs to</param>
    public ErrorHandlerBuilder(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
        this.Outcome = new StateOutcomeBuilder(this.Pipeline);
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="ErrorHandlerBuilder"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; }

    /// <summary>
    /// Gets the <see cref="ErrorHandlerDefinition"/> to configure
    /// </summary>
    protected ErrorHandlerDefinition ErrorHandler { get; } = new ErrorHandlerDefinition();

    /// <summary>
    /// Gets the service used to build the <see cref="ErrorHandlerDefinition"/>'s outcome
    /// </summary>
    protected IStateOutcomeBuilder Outcome { get; }

    /// <inheritdoc/>
    public virtual IErrorHandlerBuilder WithExtensionProperty(string name, object value)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.ErrorHandler.ExtensionData ??= new Dictionary<string, object>();
        this.ErrorHandler.ExtensionData[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorHandlerBuilder WithExtensionProperties(IDictionary<string, object> properties)
    {
        this.ErrorHandler.ExtensionData = properties ?? throw new ArgumentNullException(nameof(properties));
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorHandlerBuilder Catch(string error, string errorCode)
    {
        this.ErrorHandler.Error = error;
        this.ErrorHandler.Code = errorCode;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorHandlerBuilder Catch(string error)
    {
        this.ErrorHandler.Error = error;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorHandlerBuilder CatchAll() => this.Catch("*");

    /// <inheritdoc/>
    public virtual IErrorHandlerBuilder Retry(string policy)
    {
        this.ErrorHandler.RetryRef = policy;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorHandlerBuilder Then(Action<IStateOutcomeBuilder> outcomeSetup)
    {
        if (outcomeSetup == null) throw new ArgumentNullException(nameof(outcomeSetup));
        outcomeSetup(this.Outcome);
        return this;
    }

    /// <inheritdoc/>
    public virtual ErrorHandlerDefinition Build()
    {
        var outcome = this.Outcome.Build();
        switch (outcome)
        {
            case TransitionDefinition transition:
                this.ErrorHandler.Transition = transition;
                break;
            case EndDefinition end:
                this.ErrorHandler.End = end;
                break;
            default:
                throw new NotSupportedException($"the specified {nameof(StateOutcomeDefinition)} type '{outcome.GetType().Name}' is not supported");
        }
        return this.ErrorHandler;
    }

}
