namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="SwitchTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class SwitchTaskExecutor(IServiceProvider serviceProvider, ILogger<SwitchTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<SwitchTaskDefinition> task)
    : TaskExecutor<SwitchTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        MapEntry<string, SwitchCaseDefinition>? match = null;
        var defaultCase = Task.Definition.Switch.FirstOrDefault(kvp => string.IsNullOrWhiteSpace(kvp.Value.When));
        foreach (var @case in Task.Definition.Switch.Where(c => !string.IsNullOrWhiteSpace(c.Value.When)))
        {
            if (!await Task.Workflow.Expressions.EvaluateConditionAsync(@case.Value.When!, Task.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false)) continue;
            match = @case;
            break;
        }
        if (match != null) await SetResultAsync(Task.Input, match.Value.Then, cancellationToken).ConfigureAwait(false);
        else if (defaultCase != null) await SetResultAsync(Task.Input, defaultCase.Value.Then, cancellationToken).ConfigureAwait(false);
        else await SetResultAsync(Task.Input, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

}
