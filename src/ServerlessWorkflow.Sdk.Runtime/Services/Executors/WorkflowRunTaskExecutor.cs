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

namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute workflow <see cref="RunTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
/// <param name="definitions">The service used to manage <see cref="WorkflowDefinition"/>s</param>
public sealed class WorkflowRunTaskExecutor(IServiceProvider serviceProvider, ILogger<WorkflowRunTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory,
    ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<RunTaskDefinition> task, IWorkflowDefinitionStore definitions)
    : TaskExecutor<RunTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    IWorkflowProcess? subflow;
    bool cancelling;

    WorkflowProcessDefinition ProcessDefinition => Task.Definition.Run.Workflow!;

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var processDefinition = ProcessDefinition;
        var workflowDefinition = await definitions.GetAsync(processDefinition.Namespace, processDefinition.Name, processDefinition.Version, cancellationToken).ConfigureAwait(false)
            ?? throw new NullReferenceException($"Failed to find the specified workflow definition '{processDefinition.Namespace}.{processDefinition.Name}:{processDefinition.Version ?? "latest"}'");
        var input = processDefinition.Input == null
            ? Task.Instance.Input as JsonObject ?? []
            : (await Task.Workflow.Expressions.EvaluateAsync(processDefinition.Input, Task.Instance.Input ?? new JsonObject(), GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false))?.AsObject() ?? [];
        subflow = await Task.Workflow.Runtime.RunAsync(workflowDefinition, input, executionOptions: null, cancellationToken).ConfigureAwait(false);
        if (Task.Definition.Run.Await == false)
        {
            await SetResultAsync(new JsonObject(), Task.Definition.Then, cancellationToken).ConfigureAwait(false);
            return;
        }
        JsonNode? output = null;
        Error? error = null;
        using var subscription = subflow.Subscribe(e =>
        {
            switch (e.Type)
            {
                case WorkflowLifeCycleEventType.Completed:
                    output = e.Data as JsonNode;
                    break;
                case WorkflowLifeCycleEventType.Faulted:
                    error = e.Data as Error;
                    break;
            }
        });
        try
        {
            await subflow.WaitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (RuntimeErrorException ex)
        {
            await SetErrorAsync(error ?? ex.Error, cancellationToken).ConfigureAwait(false);
            return;
        }
        catch (OperationCanceledException)
        {
            if (cancelling) return;
            await SetErrorAsync(Error.Runtime(new Uri(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute), $"The execution of the subflow '{ProcessDefinition.Namespace}.{ProcessDefinition.Name}:{ProcessDefinition.Version ?? "latest"}' has been cancelled"), cancellationToken).ConfigureAwait(false);
            return;
        }
        if (error != null)
        {
            await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
            return;
        }
        await SetResultAsync(output, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override async Task CancelCoreAsync(CancellationToken cancellationToken)
    {
        if (subflow != null && Task.Definition.Run.Await != false)
        {
            try
            {
                cancelling = true;
                await subflow.CancelAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occurred while cancelling the subflow '{subflow}': {ex}", $"{ProcessDefinition.Namespace}.{ProcessDefinition.Name}:{ProcessDefinition.Version ?? "latest"}", ex);
            }
        }
        await base.CancelCoreAsync(cancellationToken).ConfigureAwait(false);
    }

}
