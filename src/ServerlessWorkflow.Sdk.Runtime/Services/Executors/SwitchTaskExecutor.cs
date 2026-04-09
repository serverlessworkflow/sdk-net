namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="SwitchTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="taskProcessFactory">The service used to create <see cref="ITaskProcess"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskProcess"/></param>
public sealed class SwitchTaskExecutor(IServiceProvider serviceProvider, ILogger<SwitchTaskExecutor> logger, ITaskProcessFactory taskProcessFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskProcess<SwitchTaskDefinition> task)
    : TaskExecutor<SwitchTaskDefinition>(serviceProvider, logger, taskProcessFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        MapEntry<string, SwitchCaseDefinition>? match = null;
        var defaultCase = Task.Instance.Definition.Switch.FirstOrDefault(kvp => string.IsNullOrWhiteSpace(kvp.Value.When));
        foreach (var @case in Task.Instance.Definition.Switch.Where(c => !string.IsNullOrWhiteSpace(c.Value.When)))
        {
            if (!await Task.Workflow.Expressions.EvaluateConditionAsync(@case.Value.When!, Task.Instance.State.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false)) continue;
            match = @case;
            break;
        }
        if (match != null) await SetResultAsync(Task.Instance.State.Input, match.Value.Then, cancellationToken).ConfigureAwait(false);
        else if (defaultCase != null) await SetResultAsync(Task.Instance.State.Input, defaultCase.Value.Then, cancellationToken).ConfigureAwait(false);
        else await SetResultAsync(Task.Instance.State.Input, Task.Instance.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

}
