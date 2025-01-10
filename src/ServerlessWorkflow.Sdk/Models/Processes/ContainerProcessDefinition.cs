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
/// Represents the configuration of a container process
/// </summary>
[DataContract]
public record ContainerProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the name of the container image to run
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "image", Order = 1), JsonPropertyName("image"), JsonPropertyOrder(1), YamlMember(Alias = "image", Order = 1)]
    public required virtual string Image { get; set; } = null!;

    /// <summary>
    /// Gets/sets the command, if any, to execute on the container
    /// </summary>
    [DataMember(Name = "command", Order = 2), JsonPropertyName("command"), JsonPropertyOrder(2), YamlMember(Alias = "command", Order = 2)]
    public virtual string? Command { get; set; }

    /// <summary>
    /// Gets/sets a list containing the container's port mappings, if any
    /// </summary>
    [DataMember(Name = "ports", Order = 3), JsonPropertyName("ports"), JsonPropertyOrder(3), YamlMember(Alias = "ports", Order = 3)]
    public virtual EquatableDictionary<ushort, ushort>? Ports { get; set; }

    /// <summary>
    /// Gets/sets the volume mapping for the container, if any
    /// </summary>
    [DataMember(Name = "volumes", Order = 4), JsonPropertyName("volumes"), JsonPropertyOrder(4), YamlMember(Alias = "volumes", Order = 4)]
    public virtual EquatableDictionary<string, string>? Volumes { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping of the environment variables, if any, to use when running the configured process
    /// </summary>
    [DataMember(Name = "environment", Order = 5), JsonPropertyName("environment"), JsonPropertyOrder(5), YamlMember(Alias = "environment", Order = 5)]
    public virtual EquatableDictionary<string, string>? Environment { get; set; }

    /// <summary>
    /// Gets/sets an object object used to configure the container's lifetime
    /// </summary>
    [DataMember(Name = "lifetime", Order = 6), JsonPropertyName("lifetime"), JsonPropertyOrder(6), YamlMember(Alias = "lifetime", Order = 6)]
    public virtual ContainerLifetimeDefinition? Lifetime { get; set; }

}
