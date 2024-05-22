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
using Semver;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowProcessDefinitionBuilder"/> interface
/// </summary>
public class WorkflowProcessDefinitionBuilder
    : ProcessDefinitionBuilder<WorkflowProcessDefinition>, IWorkflowProcessDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the namespace of the workflow to run
    /// </summary>
    protected virtual string? Namespace { get; set; }

    /// <summary>
    /// Gets/sets the name of the workflow to run
    /// </summary>
    protected virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets the version of the workflow to run. Defaults to `latest`
    /// </summary>
    protected virtual string Version { get; set; } = "latest";

    /// <summary>
    /// Gets/sets the data, if any, to pass as input to the workflow to execute. The value should be validated against the target workflow's input schema, if specified
    /// </summary>
    protected virtual object? Input { get; set; }

    /// <inheritdoc/>
    public virtual IWorkflowProcessDefinitionBuilder WithNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        if (!NamingConvention.IsValidName(@namespace)) throw new ArgumentException($"The the specified value '{@namespace}' is not a valid RFC1123 DNS label name", nameof(@namespace));
        this.Namespace = @namespace;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowProcessDefinitionBuilder WithName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (!NamingConvention.IsValidName(name)) throw new ArgumentException($"The the specified value '{name}' is not a valid RFC1123 DNS label name", nameof(name));
        this.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowProcessDefinitionBuilder WithVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        if (!SemVersion.TryParse(version, SemVersionStyles.Strict, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version (SemVer 2.0)", nameof(version));
        this.Version = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowProcessDefinitionBuilder WithInput(object input)
    {
        this.Input = input;
        return this;
    }

    /// <inheritdoc/>
    public override WorkflowProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Name)) throw new NullReferenceException("The name of the workflow to run must be set");
        if (string.IsNullOrWhiteSpace(this.Version)) throw new NullReferenceException("The version of the workflow to run must be set");
        return new()
        {
            Namespace = string.IsNullOrWhiteSpace(this.Namespace) ? WorkflowDefinitionMetadata.DefaultNamespace : this.Namespace,
            Name = this.Name,
            Version = this.Version,
            Input = this.Input
        };
    }

}
