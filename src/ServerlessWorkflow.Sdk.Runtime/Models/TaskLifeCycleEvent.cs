namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskLifeCycleEvent"/>
/// </summary>
/// <param name="Type">The <see cref="ITaskLifeCycleEvent"/>'s type</param>
/// <param name="Data">The <see cref="ITaskLifeCycleEvent"/>'s data, if any</param>
public sealed record TaskLifeCycleEvent(string Type, object? Data = null)
    : ITaskLifeCycleEvent
{

    /// <inheritdoc/>
    public string Type { get; init; } = Type;

    /// <inheritdoc/>
    public object? Data { get; init; } = Data;

}
