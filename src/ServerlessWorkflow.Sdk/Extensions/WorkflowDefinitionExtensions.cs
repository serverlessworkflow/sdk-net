#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="WorkflowDefinition"/>s.
/// </summary>
public static class WorkflowDefinitionExtensions
{

    /// <summary>
    /// Gets the qualified name of the workflow definition, which is a combination of its namespace, name, and version in the format "namespace.name:version".
    /// </summary>
    /// <param name="definition">The workflow definition for which to get the qualified name.</param>
    /// <returns>The qualified name of the workflow definition.</returns>
    public static string GetQualifiedName(this WorkflowDefinition definition) => $"{definition.Document.Namespace}.{definition.Document.Name}:{definition.Document.Version}";

}
