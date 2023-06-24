﻿namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IParallelStateBuilder"/> interface
/// </summary>
public class ParallelStateBuilder
    : StateBuilder<ParallelStateDefinition>, IParallelStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="ParallelStateBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    public ParallelStateBuilder(IPipelineBuilder pipeline)
        : base(pipeline)
    {

    }

    /// <inheritdoc/>
    public virtual IParallelStateBuilder Branch(Action<IBranchBuilder> branchSetup)
    {
        IBranchBuilder branch = new BranchBuilder(this.Pipeline);
        branchSetup(branch);
        this.State.Branches.Add(branch.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IParallelStateBuilder WaitFor(uint amount)
    {
        this.State.CompletionType = ParallelCompletionType.AtLeastN;
        this.State.N = amount;
        return this;
    }

    /// <inheritdoc/>
    public virtual IParallelStateBuilder WaitForAll()
    {
        this.State.CompletionType = ParallelCompletionType.AllOf;
        return this;
    }

}