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

using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IContainerProcessDefinitionBuilder"/> interface
/// </summary>
public sealed class ContainerProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ContainerProcessDefinition>, IContainerProcessDefinitionBuilder
{

    string? image;
    string? name;
    string? command;
    EquatableDictionary<ushort, ushort>? ports;
    EquatableDictionary<string, string>? volumes;
    EquatableDictionary<string, string>? environment;

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithImage(string image)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(image);
        this.image = image;
        return this;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.name = name;
        return this;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithCommand(string command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);
        this.command = command;
        return this;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithPort(ushort hostPort, ushort containerPort)
    {
        ports ??= [];
        ports[hostPort] = containerPort;
        return this;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithPorts(IDictionary<ushort, ushort> portMapping)
    {
        ArgumentNullException.ThrowIfNull(portMapping);
        ports = [.. portMapping];
        return this;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithVolume(string key, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        volumes ??= [];
        volumes[key] = value;
        return this;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithVolumes(IDictionary<string, string> volumes)
    {
        ArgumentNullException.ThrowIfNull(volumes);
        this.volumes = [.. volumes];
        return this;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        environment ??= [];
        environment[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        this.environment = [.. environment];
        return this;
    }

    /// <inheritdoc/>
    public override ContainerProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(image)) throw new NullReferenceException("The image of the container to run must be set");
        return new()
        {
            Image = image,
            Name = name,
            Command = command,
            Ports = ports,
            Volumes = volumes,
            Environment = environment
        };
    }

}
