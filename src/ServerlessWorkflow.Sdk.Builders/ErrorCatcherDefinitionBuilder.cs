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
public sealed class ErrorCatcherDefinitionBuilder
    : IErrorCatcherDefinitionBuilder
{

    ErrorFilterDefinition? catchErrors;
    string? catchAs;
    string? catchWhen;
    string? catchExceptWhen;
    OneOf<RetryPolicyDefinition, string>? retryPolicy;
    Map<string, TaskDefinition>? retryDo;

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder Errors(ErrorFilterDefinition filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        catchErrors = filter;
        return this;
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder Errors(Action<IErrorFilterDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ErrorFilterDefinitionBuilder();
        setup(builder);
        return Errors(builder.Build());
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder As(string variableName)
    {
        catchAs = variableName;
        return this;
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder When(string expression)
    {
        catchWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder ExceptWhen(string expression)
    {
        catchExceptWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder Retry(Uri reference)
    {
        retryPolicy = new RetryPolicyDefinition()
        {
            Ref = reference
        };
        return this;
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder Retry(RetryPolicyDefinition retryPolicy)
    {
        this.retryPolicy = retryPolicy;
        return this;
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder Retry(Action<IRetryPolicyDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new RetryPolicyDefinitionBuilder();
        setup(builder);
        return Retry(builder.Build());
    }

    /// <inheritdoc/>
    public IErrorCatcherDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        retryDo = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public ErrorCatcherDefinition Build() => new()
    {
        Errors = catchErrors,
        As = catchAs,
        When = catchWhen,
        ExceptWhen = catchExceptWhen,
        Retry = retryPolicy,
        Do = retryDo
    };

}