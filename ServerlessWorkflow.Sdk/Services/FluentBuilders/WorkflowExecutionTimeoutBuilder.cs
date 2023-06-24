namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="WorkflowExecutionTimeoutBuilder"/> interface
/// </summary>
public class WorkflowExecutionTimeoutBuilder
    : IWorkflowExecutionTimeoutBuilder
{

    /// <summary>
    /// Initializes a new <see cref="WorkflowExecutionTimeoutBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="WorkflowExecutionTimeoutBuilder"/> belongs to</param>
    public WorkflowExecutionTimeoutBuilder(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="WorkflowExecutionTimeoutBuilder"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; }

    /// <summary>
    /// Gets the <see cref="WorkflowExecutionTimeoutDefinition"/> to configure
    /// </summary>
    protected WorkflowExecutionTimeoutDefinition Timeout { get; } = new WorkflowExecutionTimeoutDefinition();

    /// <inheritdoc/>
    public virtual IWorkflowExecutionTimeoutBuilder After(TimeSpan duration)
    {
        this.Timeout.Duration = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowExecutionTimeoutBuilder InterruptExecution(bool interrupts = true)
    {
        this.Timeout.Interrupt = interrupts;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowExecutionTimeoutBuilder Run(string state)
    {
        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentNullException(nameof(state));
        this.Timeout.RunBefore = state;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowExecutionTimeoutBuilder Run(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
    {
        if(stateSetup == null)
            throw new ArgumentNullException(nameof(stateSetup));
        return this.Run(this.Pipeline.AddState(stateSetup).Name);
    }

    /// <inheritdoc/>
    public virtual IWorkflowExecutionTimeoutBuilder Run(StateDefinition state)
    {
        if (state == null)
            throw new ArgumentNullException(nameof(state));
        return this.Run(this.Pipeline.AddState(state).Name);
    }

    /// <inheritdoc/>
    public virtual WorkflowExecutionTimeoutDefinition Build()
    {
        return this.Timeout;
    }

}
