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

namespace ServerlessWorkflow.Sdk.Models.Processes;

/// <summary>
/// Represents an object used to configure the lifetime of a container
/// </summary>
[DataContract]
public record ContainerLifetimeDefinition
{

    /// <summary>
    /// Gets/sets the cleanup policy to use.<para></para>
    /// See <see cref="ContainerCleanupPolicy"/><para></para>
    /// Defaults to <see cref="ContainerCleanupPolicy.Never"/>
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "cleanup", Order = 1), JsonPropertyName("cleanup"), JsonPropertyOrder(1), YamlMember(Alias = "cleanup", Order = 1)]
    public required virtual string Cleanup { get; set; }

    /// <summary>
    /// Gets/sets the duration, if any, after which to delete the container once executed.<para></para>
    /// Required if <see cref="Cleanup"/> has been set to <see cref="ContainerCleanupPolicy.Eventually"/>, otherwise ignored.
    /// </summary>
    [DataMember(Name = "duration", Order = 2), JsonPropertyName("duration"), JsonPropertyOrder(2), YamlMember(Alias = "duration", Order = 2)]
    public virtual Duration? Duration { get; set; }
}
