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
[Description("Represents the configuration of a container process")]
[DataContract]
public sealed record ContainerProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the name of the container image to run
    /// </summary>
    [Description("The name of the container image to run")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "image"), JsonPropertyOrder(1), JsonPropertyName("image")]
    public required string Image { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to give specific name to the container
    /// </summary>
    [Description("A runtime expression, if any, used to give specific name to the container")]
    [DataMember(Order = 2, Name = "name"), JsonPropertyOrder(2), JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Gets/sets the command, if any, to execute on the container
    /// </summary>
    [Description("The command, if any, to execute on the container")]
    [DataMember(Order = 3, Name = "command"), JsonPropertyOrder(3), JsonPropertyName("command")]
    public string? Command { get; init; }

    /// <summary>
    /// Gets/sets a list containing the container's port mappings, if any
    /// </summary>
    [Description("A list containing the container's port mappings, if any")]
    [DataMember(Order = 4, Name = "ports"), JsonPropertyOrder(4), JsonPropertyName("ports")]
    public EquatableDictionary<ushort, ushort>? Ports { get; init; }

    /// <summary>
    /// Gets/sets the volume mapping for the container, if any
    /// </summary>
    [Description("The volume mapping for the container, if any")]
    [DataMember(Order = 5, Name = "volumes"), JsonPropertyOrder(5), JsonPropertyName("volumes")]
    public EquatableDictionary<string, string>? Volumes { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the environment variables, if any, to use when running the configured process
    /// </summary>
    [Description("A key/value mapping of the environment variables, if any, to use when running the configured process")]
    [DataMember(Order = 6, Name = "environment"), JsonPropertyOrder(6), JsonPropertyName("environment")]
    public EquatableDictionary<string, string>? Environment { get; init; }

    /// <summary>
    /// Gets/sets an object object used to configure the container's lifetime
    /// </summary>
    [Description("An object object used to configure the container's lifetime")]
    [DataMember(Order = 7, Name = "lifetime"), JsonPropertyOrder(7), JsonPropertyName("lifetime")]
    public ContainerLifetimeDefinition? Lifetime { get; init; }

}
