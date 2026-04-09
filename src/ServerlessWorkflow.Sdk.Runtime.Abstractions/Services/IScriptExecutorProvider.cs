namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to provide <see cref="IScriptExecutor"/>s
/// </summary>
public interface IScriptExecutorProvider
{

    /// <summary>
    /// Gets the <see cref="IScriptExecutor"/> for the specified language.
    /// </summary>
    /// <param name="language">The scripting language.</param>
    /// <returns>The <see cref="IScriptExecutor"/> for the specified language.</returns>
    IScriptExecutor? GetExecutor(string language);

}
