namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="OperationStateDefinition"/>s
/// </summary>
public interface IOperationStateBuilder
    : IStateBuilder<OperationStateDefinition>,
    IActionCollectionBuilder<IOperationStateBuilder>
{

}
