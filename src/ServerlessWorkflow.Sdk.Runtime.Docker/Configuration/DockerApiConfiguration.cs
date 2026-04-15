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

namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents an object used to configure the Docker API to use
/// </summary>
public sealed record DockerApiConfiguration
{

    /// <summary>
    /// Gets/sets the endpoint of the Docker API to use
    /// </summary>
    public Uri Endpoint { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new("npipe://./pipe/docker_engine") : new("unix:/var/run/docker.sock");

    /// <summary>
    /// Gets/sets the version of the Docker API to use
    /// </summary>
    public string? Version { get; set; }

}