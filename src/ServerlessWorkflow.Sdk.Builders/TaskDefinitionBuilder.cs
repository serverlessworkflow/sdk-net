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
/// Represents the base class for all <see cref="ITaskDefinitionBuilder{TDefinition}"/> implementations
/// </summary>
/// <typeparam name="TBuilder">The type of the implementing <see cref="ITaskDefinitionBuilder{TBuilder}"/></typeparam>
/// <typeparam name="TDefinition">The type of <see cref="TaskDefinition"/> to build</typeparam>
public abstract class TaskDefinitionBuilder<TBuilder, TDefinition>
    : ITaskDefinitionBuilder<TBuilder, TDefinition>
    where TBuilder : ITaskDefinitionBuilder<TBuilder>
    where TDefinition : TaskDefinition
{

    /// <summary>
    /// Gets/sets the runtime expression, if any, used to determine whether or not to run the task to build
    /// </summary>
    protected string? IfExpression { get; set; }

    /// <summary>
    /// Gets/sets the task's timeout, if any
    /// </summary>
    protected OneOf<TimeoutDefinition, string>? Timeout { get; set; }

    /// <summary>
    /// Gets/sets the task's input data, if any
    /// </summary>
    protected InputDataModelDefinition? Input { get; set; }

    /// <summary>
    /// Gets/sets the task's output data, if any
    /// </summary>
    protected OutputDataModelDefinition? Output { get; set; }

    /// <summary>
    /// Gets/sets the task's export data, if any
    /// </summary>
    protected OutputDataModelDefinition? Export { get; set; }

    /// <summary>
    /// Gets/sets the flow directive, if any, used to then execute
    /// </summary>
    protected string? ThenDirective { get; set; }

    /// <inheritdoc/>
    public virtual TBuilder If(string condition)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(condition);
        this.IfExpression = condition;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithTimeout(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.Timeout = name;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithTimeout(TimeoutDefinition timeout)
    {
        ArgumentNullException.ThrowIfNull(timeout);
        this.Timeout = timeout;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithTimeout(Action<ITimeoutDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TimeoutDefinitionBuilder();
        setup(builder);
        this.Timeout = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithInput(Action<IInputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new InputDataModelDefinitionBuilder();
        setup(builder);
        this.Input = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithOutput(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        this.Output = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithExport(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        this.Export = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder Then(string directive)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(directive);
        this.ThenDirective = directive;
        return (TBuilder)(object)this;
    }

    /// <summary>
    /// Applies the configuration common to all types of tasks
    /// </summary>
    /// <param name="definition">The task definition to configure</param>
    /// <returns>The configured task definition</returns>
    protected virtual TDefinition Configure(TDefinition definition)
    {
        definition.If = this.IfExpression;
        if (this.Timeout != null)
        {
            if (this.Timeout.T1Value != null) definition.Timeout = this.Timeout.T1Value;
            else definition.TimeoutReference = this.Timeout.T2Value;
        }
        definition.Then = this.ThenDirective;
        definition.Input = this.Input;
        definition.Output = this.Output;
        definition.Export = this.Export;
        return definition;
    }

    /// <inheritdoc/>
    public abstract TDefinition Build();

    TaskDefinition ITaskDefinitionBuilder.Build() => this.Build();

}
