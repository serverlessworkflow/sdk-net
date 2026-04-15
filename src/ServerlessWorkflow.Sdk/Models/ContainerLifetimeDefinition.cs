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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure the lifetime of a container
/// </summary>
[Description("Represents an object used to configure the lifetime of a container")]
[DataContract]
public sealed record ContainerLifetimeDefinition
{

    /// <summary>
    /// Gets/sets the cleanup policy to use.<para></para>
    /// See <see cref="ContainerCleanupPolicy"/><para></para>
    /// Defaults to <see cref="ContainerCleanupPolicy.Never"/>
    /// </summary>
    [Description("The cleanup policy to use. See ContainerCleanupPolicy. Defaults to ContainerCleanupPolicy.Never")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "cleanup"), JsonPropertyOrder(1), JsonPropertyName("cleanup")]
    public required string Cleanup { get; init; }

    /// <summary>
    /// Gets/sets the duration, if any, after which to delete the container once executed.<para></para>
    /// Required if <see cref="Cleanup"/> has been set to <see cref="ContainerCleanupPolicy.Eventually"/>, otherwise ignored.
    /// </summary>
    [Description("The duration, if any, after which to delete the container once executed. Required if Cleanup has been set to ContainerCleanupPolicy.Eventually, otherwise ignored.")]
    [DataMember(Order = 2, Name = "duration"), JsonPropertyOrder(2), JsonPropertyName("duration")]
    public Duration? Duration { get; init; }
}
