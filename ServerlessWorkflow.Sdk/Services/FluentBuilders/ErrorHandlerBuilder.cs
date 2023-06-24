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
    public virtual IStateOutcomeBuilder When(string error, string errorCode)
    {
        this.ErrorHandler.Error = error;
        this.ErrorHandler.Code = errorCode;
        return this.Outcome;
    }

    /// <inheritdoc/>
    public virtual IStateOutcomeBuilder When(string error)
    {
        this.ErrorHandler.Error = error;
        return this.Outcome;
    }

    /// <inheritdoc/>
    public virtual IStateOutcomeBuilder WhenAny() => this.When("*");

    /// <inheritdoc/>
    public virtual IErrorHandlerBuilder UseRetryStrategy(string policy)
    {
        this.ErrorHandler.RetryRef = policy;
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
