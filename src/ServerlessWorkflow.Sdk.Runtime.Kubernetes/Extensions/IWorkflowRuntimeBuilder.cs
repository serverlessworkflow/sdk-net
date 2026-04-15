// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="KubernetesContainerRuntime"/>.</param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/>.</returns>
    public static IWorkflowRuntimeBuilder UseKubernetesContainerRuntime(this IWorkflowRuntimeBuilder builder, Action<KubernetesContainerRuntimeOptions>? setup = null)
    {
        if (setup is not null) builder.Services.Configure(setup);
        builder.Services.TryAddSingleton<KubernetesContainerRuntime>();
        builder.Services.AddSingleton<IContainerRuntime>(provider => provider.GetRequiredService<KubernetesContainerRuntime>());
        builder.Services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<KubernetesContainerRuntime>());
        return builder;
    }

}
