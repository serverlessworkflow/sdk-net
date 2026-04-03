namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the base class for all implementation of the <see cref="IProcessDefinitionBuilder{TDefinition}"/> interface
/// </summary>
/// <typeparam name="TDefinition">The type of <see cref="ProcessDefinition"/> to build</typeparam>
public abstract class ProcessDefinitionBuilder<TDefinition>
    : IProcessDefinitionBuilder<TDefinition>
    where TDefinition : ProcessDefinition
{

    /// <inheritdoc/>
    public abstract TDefinition Build();

    ProcessDefinition IProcessDefinitionBuilder.Build() => Build();

}
