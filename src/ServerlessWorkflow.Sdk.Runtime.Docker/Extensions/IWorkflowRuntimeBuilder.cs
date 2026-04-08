#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IWorkflowRuntimeBuilder"/>s.
/// </summary>
public static class IWorkflowRuntimeBuilderExtensions
{

    /// <summary>
    /// Configures the <see cref="IWorkflowRuntime"/> to use the Docker container runtime.
    /// </summary>
    /// <param name="builder">The <see cref="IWorkflowRuntimeBuilder"/> to configure.</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="DockerContainerRuntime"/>.</param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/>.</returns>
    public static IWorkflowRuntimeBuilder UseDockerContainerRuntime(this IWorkflowRuntimeBuilder builder, Action<DockerContainerRuntimeOptions>? setup = null)
    {
        if (setup is not null) builder.Services.Configure(setup);
        builder.Services.TryAddSingleton<DockerContainerRuntime>();
        builder.Services.AddSingleton<IContainerRuntime>(provider => provider.GetRequiredService<DockerContainerRuntime>());
        builder.Services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<DockerContainerRuntime>());
        return builder;
    }

}
