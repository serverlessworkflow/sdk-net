namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowState"/> interface
/// </summary>
[DataContract]
public sealed class WorkflowState
    : IWorkflowState
{

    List<WorkflowRun>? runs;

    /// <inheritdoc/>
    [DataMember(Order = 1, Name = "id"), JsonPropertyOrder(1), JsonPropertyName("id")]
    public string Id { get; init; } = Guid.NewGuid().ToString("N");

    /// <inheritdoc/>
    [DataMember(Order = 2, Name = "definition"), JsonPropertyOrder(2), JsonPropertyName("definition")]
    public required WorkflowDefinitionReference Definition { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "status"), JsonPropertyOrder(3), JsonPropertyName("status")]
    public string Status { get; private set; } = WorkflowStatus.Pending;

    /// <inheritdoc/>
    [DataMember(Order = 4, Name = "createdAt"), JsonPropertyOrder(4), JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;

    /// <inheritdoc/>
    [DataMember(Order = 5, Name = "startedAt"), JsonPropertyOrder(5), JsonPropertyName("startedAt")]
    public DateTimeOffset? StartedAt { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 6, Name = "endedAt"), JsonPropertyOrder(6), JsonPropertyName("endedAt")]
    public DateTimeOffset? EndedAt { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 7, Name = "input"), JsonPropertyOrder(7), JsonPropertyName("input")]
    public JsonObject? Input { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 8, Name = "contextData"), JsonPropertyOrder(8), JsonPropertyName("contextData")]
    public JsonObject ContextData { get; private set; } = [];

    /// <inheritdoc/>
    [DataMember(Order = 9, Name = "output"), JsonPropertyOrder(9), JsonPropertyName("output")]
    public JsonNode? Output { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 10, Name = "error"), JsonPropertyOrder(10), JsonPropertyName("error")]
    public Error? Error { get; private set; }

    /// <summary>
    /// Gets a collection containing the workflow's runs
    /// </summary>
    [DataMember(Order = 11, Name = "runs"), JsonPropertyOrder(11), JsonPropertyName("runs")]
    public IReadOnlyCollection<WorkflowRun>? Runs => runs;

    IReadOnlyCollection<IWorkflowRun>? IWorkflowState.Runs => Runs;

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        Status = WorkflowStatus.Running;
        StartedAt = DateTimeOffset.Now;
        runs ??= [];
        runs.Add(new() { StartedAt = DateTimeOffset.Now });
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        Status = WorkflowStatus.Suspended;
        var run = runs?.LastOrDefault();
        run?.EndedAt = DateTimeOffset.Now;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task ResumeAsync(CancellationToken cancellationToken = default)
    {
        Status = WorkflowStatus.Running;
        runs ??= [];
        runs.Add(new() 
        { 
            StartedAt = DateTimeOffset.Now 
        });
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SetOutputAsync(JsonNode? output, CancellationToken cancellationToken = default)
    {
        Status = WorkflowStatus.Completed;
        EndedAt = DateTimeOffset.Now;
        Output = output;
        var run = runs?.LastOrDefault();
        run?.EndedAt = DateTimeOffset.Now;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SetContextDataAsync(JsonObject contextData, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(contextData);
        ContextData = contextData;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(error);
        Status = WorkflowStatus.Faulted;
        EndedAt = DateTimeOffset.Now;
        Error = error;
        var run = runs?.LastOrDefault();
        run?.EndedAt = DateTimeOffset.Now;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task CancelAsync(CancellationToken cancellationToken = default)
    {
        Status = WorkflowStatus.Cancelled;
        EndedAt = DateTimeOffset.Now;
        var run = runs?.LastOrDefault();
        run?.EndedAt = DateTimeOffset.Now;
        return Task.CompletedTask;
    }

}
