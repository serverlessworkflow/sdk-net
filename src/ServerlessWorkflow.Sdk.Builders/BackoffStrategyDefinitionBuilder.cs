namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IBackoffStrategyDefinitionBuilder"/> interface
/// </summary>
public sealed class BackoffStrategyDefinitionBuilder
    : IBackoffStrategyDefinitionBuilder
{

    /// <summary>
    /// Gets the underlying service used to build the <see cref="BackoffDefinition"/> to use
    /// </summary>
    protected IBackoffDefinitionBuilder? Backoff { get; set; }

    /// <inheritdoc/>
    public IConstantBackoffDefinitionBuilder Constant()
    {
        var builder = new ConstantBackoffDefinitionBuilder();
        Backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IExponentialBackoffDefinitionBuilder Exponential()
    {
        var builder = new ExponentialBackoffDefinitionBuilder();
        Backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public ILinearBackoffDefinitionBuilder Linear(Duration? increment = null)
    {
        var builder = new LinearBackoffDefinitionBuilder(increment);
        Backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public BackoffStrategyDefinition Build()
    {
        if (Backoff == null) throw new NullReferenceException("The backoff strategy must be set");
        var definition = Backoff.Build();
        return new()
        {
            Constant = definition is ConstantBackoffDefinition constant ? constant : null,
            Exponential = definition is ExponentialBackoffDefinition exponential ? exponential : null,
            Linear = definition is LinearBackoffDefinition linear ? linear : null,
        };
    }

}
