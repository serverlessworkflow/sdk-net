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

using Neuroglia;
using Semver;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowDefinitionBuilder"/> interface
/// </summary>
public class WorkflowDefinitionBuilder
    : IWorkflowDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the workflow's namespace
    /// </summary>
    protected string? Namespace { get; set; }

    /// <summary>
    /// Gets/sets the workflow's name
    /// </summary>
    protected string? Name { get; set; }

    /// <summary>
    /// Gets the workflow's semantic version
    /// </summary>
    protected string? Version { get; set; }

    /// <summary>
    /// Gets/sets the workflow's title
    /// </summary>
    protected string? Title { get; set; }

    /// <summary>
    /// Gets/sets the workflow's Markdown summary
    /// </summary>
    protected string? Summary { get; set; }

    /// <summary>
    /// Gets/sets the workflow's tags
    /// </summary>
    protected EquatableDictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the workflow's reusable components
    /// </summary>
    protected ComponentDefinitionCollection? Components { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the tasks the workflow is made out of
    /// </summary>
    protected Map<string, TaskDefinition>? Tasks { get; set; }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        if (!NamingConvention.IsValidName(@namespace)) throw new ArgumentException($"The the specified value '{@namespace}' is not a valid RFC1123 DNS label name", nameof(@namespace));
        this.Namespace = @namespace;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (!NamingConvention.IsValidName(name)) throw new ArgumentException($"The the specified value '{name}' is not a valid RFC1123 DNS label name", nameof(name));
        this.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        if (!SemVersion.TryParse(version, SemVersionStyles.Strict, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version (SemVer 2.0)", nameof(version)); 
        this.Version = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTitle(string title)
    {
        this.Title = title;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithSummary(string description)
    {
        this.Summary = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTag(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.Tags ??= [];
        this.Tags[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTag(IDictionary<string, string> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.Tags = new(arguments);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseAuthentication(string name, AuthenticationPolicyDefinition authentication)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(authentication);
        this.Components ??= new();
        this.Components.Authentications ??= [];
        this.Components.Authentications[name] = authentication;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseAuthentication(string name, Action<IAuthenticationPolicyDefinitionBuilder> setup)
    {
        var builder = new AuthenticationPolicyDefinitionBuilder();
        setup(builder);
        return this.UseAuthentication(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseExtension(string name, ExtensionDefinition extension)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(extension);
        this.Components ??= new();
        this.Components.Extensions ??= [];
        this.Components.Extensions[name] = extension;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseExtension(string name, Action<IExtensionDefinitionBuilder> setup)
    {
        var builder = new ExtensionDefinitionBuilder();
        setup(builder);
        return this.UseExtension(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseFunction(string name, TaskDefinition task)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(task);
        this.Components ??= new();
        this.Components.Functions ??= [];
        this.Components.Functions[name] = task;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseFunction(string name, Action<IGenericTaskDefinitionBuilder> setup)
    {
        var builder = new GenericTaskDefinitionBuilder();
        setup(builder);
        return this.UseFunction(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseRetry(string name, RetryPolicyDefinition retry)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(retry);
        this.Components ??= new();
        this.Components.Retries ??= [];
        this.Components.Retries[name] = retry;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseRetry(string name, Action<IRetryPolicyDefinitionBuilder> setup)
    {
        var builder = new RetryPolicyDefinitionBuilder();
        setup(builder);
        return this.UseRetry(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseSecret(string secret)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret);
        this.Components ??= new();
        this.Components.Secrets ??= [];
        this.Components.Secrets.Add(secret);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseSecrets(params string[] secrets)
    {
        ArgumentNullException.ThrowIfNull(secrets);
        this.Components ??= new();
        this.Components.Secrets = new(secrets);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder Do(string name, TaskDefinition task)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(task);
        this.Tasks ??= [];
        this.Tasks[name] = task;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder Do(string name, Action<IGenericTaskDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new GenericTaskDefinitionBuilder();
        setup(builder);
        var task = builder.Build();
        return this.Do(name, task);
    }

    /// <inheritdoc/>
    public virtual WorkflowDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Name)) throw new NullReferenceException("The workflow name must be set");
        if (string.IsNullOrWhiteSpace(this.Version)) throw new NullReferenceException("The workflow version must be set");
        if (this.Tasks == null || this.Tasks.Count < 1) throw new NullReferenceException("The workflow must define at least one task");
        return new()
        {
            Document = new()
            {
                Dsl = DslVersion.V010,
                Namespace = string.IsNullOrWhiteSpace(this.Namespace) ? WorkflowDefinitionMetadata.DefaultNamespace : this.Namespace,
                Name = this.Name,
                Version = this.Version,
                Title = this.Title,
                Summary = this.Summary,
                Tags = this.Tags
            },
            Use = this.Components,
            Do = this.Tasks
        };
    }

    Map<string, TaskDefinition> ITaskDefinitionMapBuilder<IWorkflowDefinitionBuilder>.Build() => this.Tasks!;

}
