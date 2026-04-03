namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IConstantBackoffDefinitionBuilder"/> interface
/// </summary>
public sealed class ConstantBackoffDefinitionBuilder
    : IConstantBackoffDefinitionBuilder
{

    /// <inheritdoc/>
    public ConstantBackoffDefinition Build() => new() { };

    BackoffDefinition IBackoffDefinitionBuilder.Build() => Build();

}
