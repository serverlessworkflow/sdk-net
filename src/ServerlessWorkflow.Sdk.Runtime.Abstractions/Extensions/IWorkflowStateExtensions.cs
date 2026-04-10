#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IWorkflowState"/>s
/// </summary>
public static class IWorkflowStateExtensions
{

    /// <summary>
    /// Gets the qualified name of the <see cref="IWorkflowState"/>'s definition, in the format {namespace}.{name}:{version}
    /// </summary>
    /// <param name="state">The <see cref="IWorkflowState"/> to get the qualified name of</param>
    /// <returns></returns>
    public static string GetQualifiedName(this IWorkflowState state) => $"{state.Definition}-{state.Id}";

}