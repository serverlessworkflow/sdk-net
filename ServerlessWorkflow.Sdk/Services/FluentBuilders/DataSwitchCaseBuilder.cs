namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IDataSwitchCaseBuilder"/> interface
/// </summary>
public class DataSwitchCaseBuilder
    : SwitchCaseBuilder<IDataSwitchCaseBuilder, DataCaseDefinition>, IDataSwitchCaseBuilder
{

    /// <summary>
    /// Initializes a new <see cref="DataSwitchCaseBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="DataSwitchCaseBuilder"/> belongs to</param>
    public DataSwitchCaseBuilder(IPipelineBuilder pipeline)
        : base(pipeline)
    {
        
    }

    /// <inheritdoc/>
    public virtual IDataSwitchCaseBuilder WithExpression(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentNullException(nameof(expression));
        this.Case.Condition = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual new DataCaseDefinition Build()
    {
        StateOutcomeDefinition outcome = base.Build();
        switch (outcome)
        {
            case EndDefinition end:
                this.Case.End = end;
                break;
            case TransitionDefinition transition:
                this.Case.Transition = transition;
                break;
            default:
                throw new NotSupportedException($"The specified outcome type '{outcome.GetType().Name}' is not supported");
        }
        return this.Case;
    }

}
