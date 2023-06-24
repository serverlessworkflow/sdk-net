namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ParallelStateDefinition"/>s
/// </summary>
public interface IParallelStateBuilder
    : IStateBuilder<ParallelStateDefinition>
{

    /// <summary>
    /// Creates and configures a new <see cref="BranchDefinition"/>
    /// </summary>
    /// <param name="branchSetup">The <see cref="Action{T}"/> used to setup the <see cref="BranchDefinition"/></param>
    /// <returns>The configured <see cref="IParallelStateBuilder"/></returns>
    IParallelStateBuilder Branch(Action<IBranchBuilder> branchSetup);

    /// <summary>
    /// Configures the <see cref="ParallelStateDefinition"/> to wait for all branches to complete before resuming the workflow's execution
    /// </summary>
    /// <returns>The configured <see cref="IParallelStateBuilder"/></returns>
    IParallelStateBuilder WaitForAll();

    /// <summary>
    /// Configures the <see cref="ParallelStateDefinition"/> to wait for the specified amount of branches to complete before resuming the workflow's execution
    /// </summary>
    /// <param name="amount">The amount of branches to wait for the execution of</param>
    /// <returns>The configured <see cref="IParallelStateBuilder"/></returns>
    IParallelStateBuilder WaitFor(uint amount);

}
