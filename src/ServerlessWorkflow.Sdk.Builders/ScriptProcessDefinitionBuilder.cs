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
using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IScriptProcessDefinitionBuilder"/> interface
/// </summary>
public class ScriptProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ScriptProcessDefinition>, IScriptProcessDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the language of the script to run
    /// </summary>
    public virtual string? Language { get; set; }

    /// <summary>
    /// Gets/sets the script's code
    /// </summary>
    public virtual string? Code { get; set; }

    /// <summary>
    /// Gets/sets the script's source
    /// </summary>
    public ExternalResourceDefinition? Source { get; set; }

    /// <summary>
    /// Gets/sets the uri that references the script's source.
    /// </summary>
    public Uri? SourceUri { get; set; }

    /// <summary>
    /// Gets the arguments, if any, of the command to execute
    /// </summary>
    protected virtual EquatableDictionary<string, object>? Arguments { get; set; }

    /// <summary>
    /// Gets/sets the environment variables, if any, of the shell command to execute
    /// </summary>
    protected virtual EquatableDictionary<string, string>? Environment { get; set; }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder WithLanguage(string language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        this.Language = language;
        return this;
    }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder WithCode(string code)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        this.Code = code;
        return this;
    }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder WithSource(Uri source)
    {
        ArgumentNullException.ThrowIfNull(source);
        this.SourceUri = source;
        return this;
    }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder WithSource(Action<IExternalResourceDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ExternalResourceDefinitionBuilder();
        setup(builder);
        this.Source = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder WithArgument(string name, object value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.Arguments ??= [];
        this.Arguments[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder WithArguments(IDictionary<string, object> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.Arguments = new(arguments);
        return this;
    }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.Environment ??= [];
        this.Environment[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        this.Environment = new(environment);
        return this;
    }

    /// <inheritdoc/>
    public override ScriptProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Language)) throw new NullReferenceException("The language in which the script to run is expressed must be set");
        if (string.IsNullOrWhiteSpace(this.Code) && this.Source == null && this.SourceUri == null) throw new NullReferenceException("Either the code or the source properties must be set");
        var process = new ScriptProcessDefinition()
        {
            Language = this.Language,
            Code = this.Code,
            Arguments = this.Arguments,
            Environment = this.Environment
        };
        if (this.Source != null) process.Source = this.Source;
        else if (this.SourceUri != null) process.Source = new() { EndpointUri = this.SourceUri };
        return process;
    }

}
