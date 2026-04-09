namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IScriptExecutorProvider"/> interface
/// </summary>
/// <param name="executors">An <see cref="IEnumerable{T}"/> containing all registered <see cref="IScriptExecutor"/> implementations</param>
public sealed class ScriptExecutorProvider(IEnumerable<IScriptExecutor> executors)
    : IScriptExecutorProvider
{

    /// <inheritdoc/>
    public IScriptExecutor? GetExecutor(string language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        return executors.FirstOrDefault(e => e.Supports(language));
    }

}
