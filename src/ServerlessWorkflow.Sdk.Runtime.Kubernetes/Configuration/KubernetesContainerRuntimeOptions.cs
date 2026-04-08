// Copyright © 2024-Present The Synapse Authors
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

using ServerlessWorkflow.Sdk.Runtime.Services;

namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents the options used to configure the <see cref="KubernetesContainerRuntime"/>
/// </summary>
public sealed class KubernetesContainerRuntimeOptions
{

    /// <summary>
    /// Gets/sets the path to the Kubeconfig file to use, if any. If not set, defaults to 'InCluster' configuration
    /// </summary>
    public string? Kubeconfig { get; set; }

    /// <summary>
    /// Gets/sets the Kubernetes image pull policy. Supported values are 'Always', 'IfNotPresent' and 'Never'. Defaults to 'Always'.
    /// </summary>
    public string ImagePullPolicy { get; set; } = "Always";

    /// <summary>
    /// Gets/sets the Kubernetes namespace to use for the created pods.
    /// </summary>
    public string? Namespace { get; set; }

}
