namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IExtensionStateBuilder"/> interface
/// </summary>
public class ExtensionStateBuilder
    : StateBuilder<ExtensionStateDefinition>, IExtensionStateBuilder
{

    /// <inheritdoc/>
    public ExtensionStateBuilder(IPipelineBuilder pipeline, string type) : base(pipeline) { this.State = new ExtensionStateDefinition(type); }

}
