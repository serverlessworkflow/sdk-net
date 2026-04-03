namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IExponentialBackoffDefinitionBuilder"/> interface
/// </summary>
public sealed class ExponentialBackoffDefinitionBuilder
    : IExponentialBackoffDefinitionBuilder
{

    /// <inheritdoc/>
    public ExponentialBackoffDefinition Build() => new() { };

    BackoffDefinition IBackoffDefinitionBuilder.Build() => Build();

}
