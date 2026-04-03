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

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowDefinitionBuilder"/> interface
/// </summary>
public class WorkflowDefinitionBuilder
    : IWorkflowDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the version of the DSL used to define the workflow
    /// </summary>
    protected string Dsl { get; set; } = ServerlessWorkflowSpecificationDefaults.Version;

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
    /// Gets/sets the workflow's timeout, if any
    /// </summary>
    protected OneOf<TimeoutDefinition, string>? Timeout { get; set; }

    /// <summary>
    /// Gets/sets the workflow's input data, if any
    /// </summary>
    protected InputDataModelDefinition? Input { get; set; }

    /// <summary>
    /// Gets/sets the workflow's output data, if any
    /// </summary>
    protected OutputDataModelDefinition? Output { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the workflow's reusable components
    /// </summary>
    protected ComponentDefinitionCollection? Components { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the tasks the workflow is made out of
    /// </summary>
    protected Map<string, TaskDefinition>? Tasks { get; set; }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseDsl(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        if (!SemVersion.TryParse(version, SemVersionStyles.Strict, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version (SemVer 2.0)", nameof(version));
        Dsl = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        if (!NamingConvention.IsValidName(@namespace)) throw new ArgumentException($"The the specified value '{@namespace}' is not a valid RFC1123 DNS label name", nameof(@namespace));
        Namespace = @namespace;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (!NamingConvention.IsValidName(name)) throw new ArgumentException($"The the specified value '{name}' is not a valid RFC1123 DNS label name", nameof(name));
        Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        if (!SemVersion.TryParse(version, SemVersionStyles.Strict, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version (SemVer 2.0)", nameof(version)); 
        Version = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTitle(string title)
    {
        Title = title;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithSummary(string description)
    {
        Summary = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTag(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Tags ??= [];
        Tags[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTag(IDictionary<string, string> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        Tags = [.. arguments];
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTimeout(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Timeout = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTimeout(TimeoutDefinition timeout)
    {
        ArgumentNullException.ThrowIfNull(timeout);
        Timeout = timeout;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithTimeout(Action<ITimeoutDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TimeoutDefinitionBuilder();
        setup(builder);
        Timeout = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithInput(Action<IInputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new InputDataModelDefinitionBuilder();
        setup(builder);
        Input = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder WithOutput(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        Output = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseAuthentication(string name, AuthenticationPolicyDefinition authentication)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(authentication);
        Components ??= new();
        Components.Authentications ??= [];
        Components.Authentications[name] = authentication;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseAuthentication(string name, Action<IAuthenticationPolicyDefinitionBuilder> setup)
    {
        var builder = new AuthenticationPolicyDefinitionBuilder();
        setup(builder);
        return UseAuthentication(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseExtension(string name, ExtensionDefinition extension)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(extension);
        Components ??= new();
        Components.Extensions ??= [];
        Components.Extensions[name] = extension;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseExtension(string name, Action<IExtensionDefinitionBuilder> setup)
    {
        var builder = new ExtensionDefinitionBuilder();
        setup(builder);
        return UseExtension(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseFunction(string name, TaskDefinition task)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(task);
        Components ??= new();
        Components.Functions ??= [];
        Components.Functions[name] = task;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseFunction(string name, Action<IGenericTaskDefinitionBuilder> setup)
    {
        var builder = new GenericTaskDefinitionBuilder();
        setup(builder);
        return UseFunction(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseRetry(string name, RetryPolicyDefinition retry)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(retry);
        Components ??= new();
        Components.Retries ??= [];
        Components.Retries[name] = retry;
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseRetry(string name, Action<IRetryPolicyDefinitionBuilder> setup)
    {
        var builder = new RetryPolicyDefinitionBuilder();
        setup(builder);
        return UseRetry(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseSecret(string secret)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret);
        Components ??= new();
        Components.Secrets ??= [];
        Components.Secrets.Add(secret);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder UseSecrets(params string[] secrets)
    {
        ArgumentNullException.ThrowIfNull(secrets);
        Components ??= new();
        Components.Secrets = new(secrets);
        return this;
    }

    /// <inheritdoc/>
    public virtual IWorkflowDefinitionBuilder Do(string name, TaskDefinition task)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(task);
        Tasks ??= [];
        Tasks[name] = task;
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
        return Do(name, task);
    }

    /// <inheritdoc/>
    public virtual WorkflowDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(Dsl)) throw new NullReferenceException("The workflow DSL must be set");
        if (string.IsNullOrWhiteSpace(Name)) throw new NullReferenceException("The workflow name must be set");
        if (string.IsNullOrWhiteSpace(Version)) throw new NullReferenceException("The workflow version must be set");
        if (Tasks == null || Tasks.Count < 1) throw new NullReferenceException("The workflow must define at least one task");
        var definition =  new WorkflowDefinition()
        {
            Document = new()
            {
                Dsl = Dsl,
                Namespace = string.IsNullOrWhiteSpace(Namespace) ? WorkflowDefinitionMetadata.DefaultNamespace : Namespace,
                Name = Name,
                Version = Version,
                Title = Title,
                Summary = Summary,
                Tags = Tags
            },
            Use = Components,
            Do = Tasks
        };
        if (Timeout != null)
        {
            if (Timeout.T1Value != null) definition.Timeout = Timeout.T1Value;
            else definition.TimeoutReference = Timeout.T2Value;
        }
        return definition;
    }

    Map<string, TaskDefinition> ITaskDefinitionMapBuilder<IWorkflowDefinitionBuilder>.Build() => Tasks!;

}
