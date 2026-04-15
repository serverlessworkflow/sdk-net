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

namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="RaiseTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class RaiseTaskExecutor(IServiceProvider serviceProvider, ILogger<RaiseTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<RaiseTaskDefinition> task)
    : TaskExecutor<RaiseTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var input = Task.Instance.Input;
        var errorDefinition = Task.Definition.Raise.Error.Match(
            e => e,
            reference =>
            {
                if (string.IsNullOrWhiteSpace(reference)) throw new NullReferenceException("The error to raise must be defined (or referenced)");
                if (Task.Workflow.Definition.Use is null || Task.Workflow.Definition.Use.Errors is null || !Task.Workflow.Definition.Use.Errors!.TryGetValue(reference, out var error) || error is null) throw new NullReferenceException($"Failed to find the referenced error definition '{reference}'");
                return error;
            });
        var status = errorDefinition.Status is string expression
            ? expression.IsRuntimeExpression()
                ? await Task.Workflow.Expressions.EvaluateAsync<ushort>(errorDefinition.Status, input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false)
                : ushort.Parse(expression)
            : ushort.Parse(errorDefinition.Status.ToString()!);
        var type = errorDefinition.Type.IsRuntimeExpression()
            ? (await Task.Workflow.Expressions.EvaluateAsync<Uri>(errorDefinition.Type, input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false))!
            : new(errorDefinition.Type, UriKind.RelativeOrAbsolute);
        var title = errorDefinition.Title.IsRuntimeExpression()
            ? (await Task.Workflow.Expressions.EvaluateAsync<string>(errorDefinition.Title, input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false))!
            : errorDefinition.Title;
        var detail = string.IsNullOrWhiteSpace(errorDefinition.Detail) ? null : errorDefinition.Detail!.IsRuntimeExpression()
            ? await Task.Workflow.Expressions.EvaluateAsync<string>(errorDefinition.Detail!, input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false)
            : errorDefinition.Detail;
        var errorInstance = new Error()
        {
            Status = status,
            Type = type,
            Title = title,
            Detail = detail,
            Instance = new(Task.Instance.Reference.ToString(), UriKind.Relative)
        };
        await SetErrorAsync(errorInstance, cancellationToken).ConfigureAwait(false);
    }

}
