using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an extension state
/// </summary>
[DataContract]
[DiscriminatedByDefault]
public class ExtensionStateDefinition
    : StateDefinition
{

    /// <inheritdoc/>
    public ExtensionStateDefinition() { }

    /// <inheritdoc/>
    public ExtensionStateDefinition(string type): base(type) { }

}