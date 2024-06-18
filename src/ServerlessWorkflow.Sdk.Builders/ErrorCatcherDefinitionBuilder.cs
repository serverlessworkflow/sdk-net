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
/// Represents the default implementation of the <see cref="IErrorCatcherDefinitionBuilder"/> interface
/// </summary>
public class ErrorCatcherDefinitionBuilder
    : IErrorCatcherDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the definition of the errors to catch
    /// </summary>
    protected ErrorFilterDefinition? CatchErrors { get; set; }

    /// <summary>
    /// Gets/sets the name of the runtime expression variable to save the error as. Defaults to 'error'.
    /// </summary>
    protected string? CatchAs { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to catch the filtered error
    /// </summary>
    protected string? CatchWhen { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to catch the filtered error
    /// </summary>
    protected string? CatchExceptWhen { get; set; }

    /// <summary>
    /// Gets/sets a reference to the definition of the retry policy to use when catching errors
    /// </summary>
    protected Uri? RetryPolicyReference { get; set; }

    /// <summary>
    /// Gets/sets the definition of the retry policy to use when catching errors
    /// </summary>
    protected RetryPolicyDefinition? RetryPolicy { get; set; }

    /// <summary>
    /// Gets/sets the definition of the task to run when catching an error
    /// </summary>
    protected Map<string, TaskDefinition>? RetryDo { get; set; }

    /// <inheritdoc/>
    public virtual IErrorCatcherDefinitionBuilder Errors(ErrorFilterDefinition filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        this.CatchErrors = filter;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorCatcherDefinitionBuilder Errors(Action<IErrorFilterDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ErrorFilterDefinitionBuilder();
        setup(builder);
        return this.Errors(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IErrorCatcherDefinitionBuilder As(string variableName)
    {
        this.CatchAs = variableName;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorCatcherDefinitionBuilder When(string expression)
    {
        this.CatchWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorCatcherDefinitionBuilder ExceptWhen(string expression)
    {
        this.CatchExceptWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorCatcherDefinitionBuilder Retry(Uri reference)
    {
        this.RetryPolicyReference = reference;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorCatcherDefinitionBuilder Retry(RetryPolicyDefinition retryPolicy)
    {
        this.RetryPolicy = retryPolicy;
        return this;
    }

    /// <inheritdoc/>
    public virtual IErrorCatcherDefinitionBuilder Retry(Action<IRetryPolicyDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new RetryPolicyDefinitionBuilder();
        setup(builder);
        return this.Retry(builder.Build());
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        this.RetryDo = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual ErrorCatcherDefinition Build() => new()
    {
        Errors = this.CatchErrors,
        As = this.CatchAs,
        When = this.CatchWhen,
        ExceptWhen = this.CatchExceptWhen,
        Retry = this.RetryPolicyReference == null ? this.RetryPolicy : new() { Ref = this.RetryPolicyReference },
        Do = this.RetryDo
    };

}