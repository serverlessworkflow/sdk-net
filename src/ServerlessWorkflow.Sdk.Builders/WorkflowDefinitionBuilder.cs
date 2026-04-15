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
public sealed class WorkflowDefinitionBuilder
    : IWorkflowDefinitionBuilder
{

    string dsl = ServerlessWorkflowSpecificationDefaults.Version;
    string? @namespace;
    string? name;
    string? version;
    string? title;
    string? summary;
    EquatableDictionary<string, string>? tags;
    OneOf<TimeoutDefinition, string>? timeout;
    InputDataModelDefinition? input;
    OutputDataModelDefinition? output;
    ComponentDefinitionCollection? components;
    Map<string, TaskDefinition>? tasks;

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseDsl(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        if (!SemVersion.TryParse(version, SemVersionStyles.Strict, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version (SemVer 2.0)", nameof(version));
        dsl = version;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        if (!NamingConvention.IsValidName(@namespace)) throw new ArgumentException($"The the specified value '{@namespace}' is not a valid RFC1123 DNS label name", nameof(@namespace));
        this.@namespace = @namespace;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (!NamingConvention.IsValidName(name)) throw new ArgumentException($"The the specified value '{name}' is not a valid RFC1123 DNS label name", nameof(name));
        this.name = name;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        if (!SemVersion.TryParse(version, SemVersionStyles.Strict, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version (SemVer 2.0)", nameof(version)); 
        this.version = version;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithTitle(string title)
    {
        this.title = title;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithSummary(string summary)
    {
        this.summary = summary;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithTag(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        tags ??= [];
        tags[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithTag(IDictionary<string, string> tags)
    {
        ArgumentNullException.ThrowIfNull(tags);
        this.tags = [.. tags];
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithTimeout(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        timeout = name;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithTimeout(TimeoutDefinition timeout)
    {
        ArgumentNullException.ThrowIfNull(timeout);
        this.timeout = timeout;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithTimeout(Action<ITimeoutDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TimeoutDefinitionBuilder();
        setup(builder);
        timeout = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithInput(Action<IInputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new InputDataModelDefinitionBuilder();
        setup(builder);
        this.input = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder WithOutput(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        this.output = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseAuthentication(string name, AuthenticationPolicyDefinition authentication)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(authentication);
        components ??= new();
        var authentications = components.Authentications ?? [];
        authentications[name] = authentication;
        components = components with
        {
            Authentications = authentications
        };
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseAuthentication(string name, Action<IAuthenticationPolicyDefinitionBuilder> setup)
    {
        var builder = new AuthenticationPolicyDefinitionBuilder();
        setup(builder);
        return UseAuthentication(name, builder.Build());
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseExtension(string name, ExtensionDefinition extension)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(extension);
        components ??= new();
        var extensions = components.Extensions ?? [];
        extensions[name] = extension;
        components = components with
        {
            Extensions = extensions
        };
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseExtension(string name, Action<IExtensionDefinitionBuilder> setup)
    {
        var builder = new ExtensionDefinitionBuilder();
        setup(builder);
        return UseExtension(name, builder.Build());
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseFunction(string name, TaskDefinition task)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(task);
        components ??= new();
        var functions = components.Functions ?? [];
        functions[name] = task;
        components = components with
        {
            Functions = functions
        };
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseFunction(string name, Action<IGenericTaskDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new GenericTaskDefinitionBuilder();
        setup(builder);
        return UseFunction(name, builder.Build());
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseRetry(string name, RetryPolicyDefinition retry)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(retry);
        components ??= new();
        var retries = components.Retries ?? [];
        retries[name] = retry;
        components = components with
        {
            Retries = retries
        };
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseRetry(string name, Action<IRetryPolicyDefinitionBuilder> setup)
    {
        var builder = new RetryPolicyDefinitionBuilder();
        setup(builder);
        return UseRetry(name, builder.Build());
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseSecret(string secret)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret);
        components ??= new();
        var secrets = components.Secrets ?? [];
        secrets.Add(secret);
        components = components with
        {
            Secrets = secrets
        };
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder UseSecrets(params string[] secrets)
    {
        ArgumentNullException.ThrowIfNull(secrets);
        components ??= new();
        var existingSecrets = components.Secrets ?? [];
        existingSecrets.AddRange(secrets);
        components = components with
        {
            Secrets = existingSecrets
        };
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder Do(string name, TaskDefinition task)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(task);
        tasks ??= [];
        tasks[name] = task;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowDefinitionBuilder Do(string name, Action<IGenericTaskDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new GenericTaskDefinitionBuilder();
        setup(builder);
        var task = builder.Build();
        return Do(name, task);
    }

    /// <inheritdoc/>
    public WorkflowDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(dsl)) throw new NullReferenceException("The workflow DSL must be set");
        if (string.IsNullOrWhiteSpace(name)) throw new NullReferenceException("The workflow name must be set");
        if (string.IsNullOrWhiteSpace(version)) throw new NullReferenceException("The workflow version must be set");
        if (tasks == null || tasks.Count < 1) throw new NullReferenceException("The workflow must define at least one task");
        var definition =  new WorkflowDefinition()
        {
            Document = new()
            {
                Dsl = dsl,
                Namespace = string.IsNullOrWhiteSpace(@namespace) ? WorkflowDefinitionMetadata.DefaultNamespace : @namespace,
                Name = name,
                Version = version,
                Title = title,
                Summary = summary,
                Tags = tags
            },
            Use = components,
            Do = tasks,
            Timeout = timeout
        };
        return definition;
    }

    Map<string, TaskDefinition> ITaskDefinitionMapBuilder<IWorkflowDefinitionBuilder>.Build() => tasks!;

}
