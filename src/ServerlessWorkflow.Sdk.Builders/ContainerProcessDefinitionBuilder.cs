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
using Neuroglia;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IContainerProcessDefinitionBuilder"/> interface
/// </summary>
public class ContainerProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ContainerProcessDefinition>, IContainerProcessDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the name of the container image to run
    /// </summary>
    protected virtual string? Image { get; set; }

    /// <summary>
    /// Gets/sets the command, if any, to execute on the container
    /// </summary>
    protected virtual string? Command { get; set; }

    /// <summary>
    /// Gets/sets a list containing the container's port mappings, if any
    /// </summary>
    protected virtual EquatableDictionary<ushort, ushort>? Ports { get; set; }

    /// <summary>
    /// Gets/sets the volumes mapping for the container, if any
    /// </summary>
    protected virtual EquatableDictionary<string, string>? Volumes { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping of the environment variables, if any, to use when running the configured process
    /// </summary>
    protected virtual EquatableDictionary<string, string>? Environment { get; set; }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder WithImage(string image)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(image);
        this.Image = image;
        return this;
    }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder WithCommand(string command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);
        this.Command = command;
        return this;
    }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder WithPort(ushort hostPort, ushort containerPort)
    {
        this.Ports ??= [];
        this.Ports[hostPort] = containerPort;
        return this;
    }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder WithPorts(IDictionary<ushort, ushort> portMapping)
    {
        ArgumentNullException.ThrowIfNull(portMapping);
        this.Ports = new(portMapping);
        return this;
    }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder WithVolume(string key, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        this.Volumes ??= [];
        this.Volumes[key] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder WithVolumes(IDictionary<string, string> volumes)
    {
        ArgumentNullException.ThrowIfNull(volumes);
        this.Volumes = new(volumes);
        return this;
    }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        this.Environment ??= [];
        this.Environment[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        this.Environment = new(environment);
        return this;
    }

    /// <inheritdoc/>
    public override ContainerProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Image)) throw new NullReferenceException("The image of the container to run must be set");
        return new()
        {
            Image = this.Image,
            Command = this.Command,
            Ports = this.Ports,
            Volumes = this.Volumes,
            Environment = this.Environment
        };
    }

}
