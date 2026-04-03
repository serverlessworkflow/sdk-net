#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IHostEnvironment"/>s
/// </summary>
public static class IHostEnvironmentExtensions
{

    /// <summary>
    /// Determines whether or not the <see cref="IHostEnvironment"/> runs in Docker
    /// </summary>
    /// <param name="env">The <see cref="IHostEnvironment"/> to check</param>
    /// <returns>A boolean indicating whether or not the <see cref="IHostEnvironment"/> runs in Docker</returns>
    public static bool RunsInDocker(this IHostEnvironment env) => File.Exists("/.dockerenv");

    /// <summary>
    /// Determines whether or not the <see cref="IHostEnvironment"/> runs in Kubernetes
    /// </summary>
    /// <param name="env">The <see cref="IHostEnvironment"/> to check</param>
    /// <returns>A boolean indicating whether or not the <see cref="IHostEnvironment"/> runs in Kubernetes</returns>
    public static bool RunsInKubernetes(this IHostEnvironment env) => !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("KUBERNETES_SERVICE_HOST"));

}
