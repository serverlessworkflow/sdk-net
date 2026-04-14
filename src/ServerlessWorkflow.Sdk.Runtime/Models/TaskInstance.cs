namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskInstance"/> interface
/// </summary>
[DataContract]
public sealed class TaskInstance
    : ITaskInstance
{

    List<TaskRun>? runs;
    List<TaskRetryAttempt>? retries;

    /// <inheritdoc/>
    [DataMember(Order = 1, Name = "id"), JsonPropertyOrder(1), JsonPropertyName("id")]
    public string Id { get; init; } = Guid.NewGuid().ToString("N");

    /// <inheritdoc/>
    [DataMember(Order = 2, Name = "workflowId"), JsonPropertyOrder(2), JsonPropertyName("workflowId")]
    public required string WorkflowId { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "name"), JsonPropertyOrder(3), JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 4, Name = "reference"), JsonPropertyOrder(4), JsonPropertyName("reference")]
    public required JsonPointer Reference { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 5, Name = "isExtension"), JsonPropertyOrder(5), JsonPropertyName("isExtension")]
    public bool IsExtension { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 6, Name = "parentId"), JsonPropertyOrder(6), JsonPropertyName("parentId")]
    public string? ParentId { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 7, Name = "createdAt"), JsonPropertyOrder(7), JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;

    /// <inheritdoc/>
    [DataMember(Order = 8, Name = "startedAt"), JsonPropertyOrder(8), JsonPropertyName("startedAt")]
    public DateTimeOffset? StartedAt { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 9, Name = "endedAt"), JsonPropertyOrder(9), JsonPropertyName("endedAt")]
    public DateTimeOffset? EndedAt { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 10, Name = "status"), JsonPropertyOrder(10), JsonPropertyName("status")]
    public string? Status { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 11, Name = "statusReason"), JsonPropertyOrder(11), JsonPropertyName("statusReason")]
    public string? StatusReason { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 12, Name = "error"), JsonPropertyOrder(12), JsonPropertyName("error")]
    public Error? Error { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 13, Name = "input"), JsonPropertyOrder(13), JsonPropertyName("input")]
    public required JsonNode Input { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 14, Name = "contextData"), JsonPropertyOrder(14), JsonPropertyName("contextData")]
    public JsonNode? Output { get; private set; }

    /// <inheritdoc/>
    [DataMember(Order = 15, Name = "output"), JsonPropertyOrder(15), JsonPropertyName("output")]
    public string? Next { get; private set; }

    /// <summary>
    /// Gets a collection containing the task's runs
    /// </summary>
    [DataMember(Order = 16, Name = "runs"), JsonPropertyOrder(16), JsonPropertyName("runs")]
    public IReadOnlyCollection<TaskRun>? Runs => runs;

    /// <summary>
    /// Gets a collection containing the task's retry attempts
    /// </summary>
    [DataMember(Order = 17, Name = "retries"), JsonPropertyOrder(17), JsonPropertyName("retries")]
    public IReadOnlyCollection<TaskRetryAttempt>? Retries => retries;

    IReadOnlyCollection<ITaskRun>? ITaskInstance.Runs => Runs;

    IReadOnlyCollection<ITaskRetryAttempt>? ITaskInstance.Retries => Retries;

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        Status = TaskStatus.Running;
        StartedAt = DateTimeOffset.Now;
        runs ??= [];
        runs.Add(new()
        {
            StartedAt = StartedAt.Value
        });
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        Status = TaskStatus.Suspended;
        var run = runs!.Last();
        run.EndedAt = DateTimeOffset.Now;
        run.Outcome = Status;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task ResumeAsync(CancellationToken cancellationToken = default)
    {
        Status = TaskStatus.Running;
        runs ??= [];
        runs.Add(new()
        {
            StartedAt = DateTimeOffset.Now
        });
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task RetryAsync(Error cause, CancellationToken cancellationToken = default)
    {
        retries ??= [];
        retries.Add(new()
        {
            Number = (uint)retries.Count + 1,
            Cause = cause
        });
        Status = TaskStatus.Running;
        StartedAt ??= DateTimeOffset.Now;
        runs ??= [];
        runs.Add(new() 
        { 
            StartedAt = DateTimeOffset.Now 
        });
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        Status = TaskStatus.Faulted;
        EndedAt = DateTimeOffset.Now;
        var run = runs?.LastOrDefault();
        if (run != null)
        {
            run.EndedAt = DateTimeOffset.Now;
            run.Outcome = Status;
        }
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SkipAsync(JsonNode? result, string next, CancellationToken cancellationToken = default)
    {
        Status = TaskStatus.Skipped;
        EndedAt = DateTimeOffset.Now;
        Output = result;
        Next = next;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SetOutputAsync(JsonNode? output, string next, CancellationToken cancellationToken = default)
    {
        Status = TaskStatus.Completed;
        EndedAt = DateTimeOffset.Now;
        Output = output;
        Next = next;
        var run = runs?.LastOrDefault();
        run?.EndedAt = EndedAt.Value;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task CancelAsync(CancellationToken cancellationToken = default)
    {
        Status = TaskStatus.Cancelled;
        EndedAt = DateTimeOffset.Now;
        var run = runs?.LastOrDefault();
        run?.EndedAt = EndedAt.Value;
        return Task.CompletedTask;
    }

}
