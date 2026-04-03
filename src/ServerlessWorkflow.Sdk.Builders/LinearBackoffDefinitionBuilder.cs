namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ILinearBackoffDefinitionBuilder"/> interface
/// </summary>
public sealed class LinearBackoffDefinitionBuilder(Duration? increment = null)
    : ILinearBackoffDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the linear incrementation to the delay between retry attempts
    /// </summary>
    protected Duration? LinearIncrement { get; set; } = increment;

    /// <inheritdoc/>
    public ILinearBackoffDefinitionBuilder WithIncrement(Duration increment)
    {
        ArgumentNullException.ThrowIfNull(increment);
        LinearIncrement = increment;
        ILinearBackoffDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public LinearBackoffDefinition Build() => new()
    {
        Increment = LinearIncrement
    };

    BackoffDefinition IBackoffDefinitionBuilder.Build() => Build();

}
