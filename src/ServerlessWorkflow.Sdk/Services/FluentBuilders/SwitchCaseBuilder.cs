namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="ISwitchCaseBuilder{TBuilder}"/> interface
/// </summary>
public abstract class SwitchCaseBuilder<TBuilder, TCase>
    : StateOutcomeBuilder, ISwitchCaseBuilder<TBuilder>
    where TBuilder : class, ISwitchCaseBuilder<TBuilder>
    where TCase : SwitchCaseDefinition, new()
{

    /// <summary>
    /// Initializes a new <see cref="SwitchCaseBuilder{TBuilder, TCase}"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="SwitchCaseBuilder{TBuilder, TCase}"/> belongs to</param>
    public SwitchCaseBuilder(IPipelineBuilder pipeline) : base(pipeline) { }

    /// <summary>
    /// Gets the <see cref="SwitchCaseDefinition"/> to configure
    /// </summary>
    protected TCase Case { get; } = new TCase();

    /// <inheritdoc/>
    public virtual TBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Case.Name = name;
        return (TBuilder)(object)this;
    }

}
