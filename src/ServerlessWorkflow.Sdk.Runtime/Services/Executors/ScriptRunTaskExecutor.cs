namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute script <see cref="RunTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="externalResourceReader">The service used to read external resources</param>
/// <param name="scriptExecutorProvider">The service used to provide <see cref="IScriptExecutor"/>s</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class ScriptRunTaskExecutor(IServiceProvider serviceProvider, ILogger<ScriptRunTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, IExternalResourceReader externalResourceReader, IScriptExecutorProvider scriptExecutorProvider, ITaskExecutionContext<RunTaskDefinition> task)
    : TaskExecutor<RunTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var processDefinition = Task.Definition.Run.Script!;
        var executor = scriptExecutorProvider.GetExecutor(processDefinition.Language) ?? throw new NullReferenceException($"Failed to find a script executor for the specified language '{processDefinition.Language}'");
        var script = processDefinition.Code;
        if (string.IsNullOrWhiteSpace(script))
        {
            if (processDefinition.Source == null) throw new NullReferenceException("The script's code or source must be set");
            using var stream = await externalResourceReader.ReadAsync(processDefinition.Source, Task.Workflow.Definition, cancellationToken).ConfigureAwait(false);
            using var streamReader = new StreamReader(stream);
            script = await streamReader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
        }
        var expressionArguments = GetExpressionEvaluationArguments();
        List<string>? arguments = null;
        if (processDefinition.Arguments != null)
        {
            arguments = [];
            foreach (var kvp in processDefinition.Arguments)
            {
                var value = await EvaluateAndSerializeAsync(kvp.Value, expressionArguments, cancellationToken).ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(value) && value != "True" && value != "False") arguments.AddRange([$"--{kvp.Key}", value]);
            }
        }
        Dictionary<string, string>? environment = null;
        if (processDefinition.Environment != null)
        {
            environment = [];
            foreach (var kvp in processDefinition.Environment)
            {
                var value = await EvaluateAndSerializeAsync(kvp.Value, expressionArguments, cancellationToken).ConfigureAwait(false);
                if (value != null) environment[kvp.Key] = value;
            }
        }
        var process = await executor.ExecuteAsync(script, arguments, environment, cancellationToken).ConfigureAwait(false);
        if (Task.Definition.Run.Await == false)
        {
            await SetResultAsync(new JsonObject(), Task.Definition.Then, cancellationToken).ConfigureAwait(false);
            return;
        }
        await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
        var rawOutput = (await process.StandardOutput.ReadToEndAsync(cancellationToken).ConfigureAwait(false)).Trim();
        var errorMessage = (await process.StandardError.ReadToEndAsync(cancellationToken).ConfigureAwait(false)).Trim();
        if (process.ExitCode == 0) await SetResultAsync(new JsonObject { ["output"] = rawOutput }, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
        else await SetErrorAsync(Error.Runtime(new Uri(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute), errorMessage), cancellationToken).ConfigureAwait(false);
        process.Dispose();
    }

    async Task<string?> EvaluateAndSerializeAsync(object? value, JsonObject? expressionArguments, CancellationToken cancellationToken)
    {
        if (value == null) return null;
        if (value is string str && str.IsRuntimeExpression())
        {
            var evaluated = await Task.Workflow.Expressions.EvaluateAsync(str, Task.Instance.Input, expressionArguments, cancellationToken).ConfigureAwait(false);
            if (evaluated == null) return null;
            if (evaluated is JsonValue jsonValue) return jsonValue.ToString();
            return evaluated.ToJsonString();
        }
        if (value.GetType().IsValueType || value is string) return value.ToString();
        return JsonSerializer.Serialize(value);
    }

}
