namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to configure <see cref="BranchDefinition"/>s
/// </summary>
public interface IBranchBuilder
    : IActionCollectionBuilder<IBranchBuilder>, IExtensibleBuilder<IBranchBuilder>
{

    /// <summary>
    /// Sets the <see cref="BranchDefinition"/>'s name
    /// </summary>
    /// <param name="name">The <see cref="BranchDefinition"/>'s name</param>
    /// <returns>The configured <see cref="IBranchBuilder"/></returns>
    IBranchBuilder WithName(string name);

    /// <summary>
    /// Builds the <see cref="BranchDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="BranchDefinition"/></returns>
    BranchDefinition Build();

}
