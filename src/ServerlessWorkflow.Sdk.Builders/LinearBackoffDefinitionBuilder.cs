namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ILinearBackoffDefinitionBuilder"/> interface
/// </summary>
public sealed class LinearBackoffDefinitionBuilder(Duration? increment = null)
    : ILinearBackoffDefinitionBuilder
{

    Duration? linearIncrement = increment;

    /// <inheritdoc/>
    public ILinearBackoffDefinitionBuilder WithIncrement(Duration increment)
    {
        ArgumentNullException.ThrowIfNull(increment);
        linearIncrement = increment;
        return this;
    }

    /// <inheritdoc/>
    public LinearBackoffDefinition Build() => new()
    {
        Increment = linearIncrement
    };

    BackoffDefinition IBackoffDefinitionBuilder.Build() => Build();

}
