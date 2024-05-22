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
/// Defines the fundamentals of a service used to build <see cref="ContainerProcessDefinition"/>s
/// </summary>
public interface IContainerProcessDefinitionBuilder
    : IProcessDefinitionBuilder<ContainerProcessDefinition>
{

    /// <summary>
    /// Configures the container to use the specified image
    /// </summary>
    /// <param name="image">The image to use</param>
    /// <returns>The configured <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder WithImage(string image);

    /// <summary>
    /// Configures the command, if any, to execute on the container
    /// </summary>
    /// <param name="command">The command to execute</param>
    /// <returns>The configured <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder WithCommand(string command);

    /// <summary>
    /// Adds the specified container port mapping
    /// </summary>
    /// <returns>The configured <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder WithPort(ushort hostPort, ushort containerPort);

    /// <summary>
    /// Sets the container's port mapping
    /// </summary>
    /// <param name="portMapping">The host/container port mapping to use</param>
    /// <returns>The configured <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder WithPorts(IDictionary<ushort, ushort> portMapping);

    /// <summary>
    /// Adds the specified volume to the container
    /// </summary>
    /// <param name="key">The key of the volume to add</param>
    /// <param name="value">The volume to add</param>
    /// <returns>The configured <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder WithVolume(string key, string value);

    /// <summary>
    /// Sets the container's volumes
    /// </summary>
    /// <param name="volumes">A key/value mapping of the volumes to use</param>
    /// <returns>The configured <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder WithVolumes(IDictionary<string, string> volumes);

    /// <summary>
    /// Adds the specified environment variable to the container
    /// </summary>
    /// <param name="name">The environment variable's name</param>
    /// <param name="value">The environment variable's value</param>
    /// <returns>The configured <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder WithEnvironment(string name, string value);

    /// <summary>
    /// Sets the container's environment variables
    /// </summary>
    /// <param name="environment">A name/value mapping of the environment variables to use</param>
    /// <returns>The configured <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment);

}
