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
/// Represents the default implementation of the <see cref="IForTaskDefinitionBuilder"/> interface
/// </summary>
public class ForTaskDefinitionBuilder
    : TaskDefinitionBuilder<ForTaskDefinition>, IForTaskDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the name of the variable that represents each element in the collection during iteration
    /// </summary>
    protected virtual string? EachVariableName { get; set; }

    /// <summary>
    /// Gets/sets the runtime expression used to get the collection to iterate over
    /// </summary>
    protected virtual string? InExpression { get; set; }

    /// <summary>
    /// Gets/sets the name of the variable used to hold the index of each element in the collection during iteration
    /// </summary>
    protected virtual string? AtVariableName { get; set; }

    /// <summary>
    /// Gets an <see cref="Action{T}"/> used to setup the task to perform for each iteration
    /// </summary>
    protected virtual Action<IGenericTaskDefinitionBuilder>? DoSetup { get; set; }

    /// <inheritdoc/>
    public virtual IForTaskDefinitionBuilder Each(string variableName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(variableName);
        this.EachVariableName = variableName;
        return this;
    }

    /// <inheritdoc/>
    public virtual IForTaskDefinitionBuilder In(string expression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        this.InExpression = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IForTaskDefinitionBuilder At(string variableName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(variableName);
        this.AtVariableName = variableName;
        return this;
    }

    /// <inheritdoc/>
    public virtual IForTaskDefinitionBuilder Do(Action<IGenericTaskDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        this.DoSetup = setup;
        return this;
    }

    /// <inheritdoc/>
    public override ForTaskDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.EachVariableName)) throw new NullReferenceException("The variable name used to store the iterated items must be set");
        if (string.IsNullOrWhiteSpace(this.InExpression)) throw new NullReferenceException("The runtime expression used to resolve the collection to iterate must be set");
        if (this.DoSetup == null) throw new NullReferenceException("The task to perform at each iteration must be set");
        var taskBuilder = new GenericTaskDefinitionBuilder();
        this.DoSetup(taskBuilder);
        return new()
        {
            For = new()
            {
                Each = this.EachVariableName,
                In = this.InExpression,
                At = this.AtVariableName
            },
            Do = taskBuilder.Build()
        };
    }

}
