namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IBackoffStrategyDefinitionBuilder"/> interface
/// </summary>
public sealed class BackoffStrategyDefinitionBuilder
    : IBackoffStrategyDefinitionBuilder
{

    IBackoffDefinitionBuilder? backoff;

    /// <inheritdoc/>
    public IConstantBackoffDefinitionBuilder Constant()
    {
        var builder = new ConstantBackoffDefinitionBuilder();
        backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IExponentialBackoffDefinitionBuilder Exponential()
    {
        var builder = new ExponentialBackoffDefinitionBuilder();
        backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public ILinearBackoffDefinitionBuilder Linear(Duration? increment = null)
    {
        var builder = new LinearBackoffDefinitionBuilder(increment);
        backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public BackoffStrategyDefinition Build()
    {
        if (backoff == null) throw new NullReferenceException("The backoff strategy must be set");
        var definition = backoff.Build();
        return new()
        {
            Constant = definition is ConstantBackoffDefinition constant ? constant : null,
            Exponential = definition is ExponentialBackoffDefinition exponential ? exponential : null,
            Linear = definition is LinearBackoffDefinition linear ? linear : null,
        };
    }

}
