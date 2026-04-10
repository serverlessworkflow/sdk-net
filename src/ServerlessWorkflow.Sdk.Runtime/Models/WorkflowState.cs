namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowState"/> interface
/// </summary>
[DataContract]
public sealed class WorkflowState
    : IWorkflowState
{

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
    public DateTimeOffset? EndedAt { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 7, Name = "input"), JsonPropertyOrder(7), JsonPropertyName("input")]
    public JsonObject? Input { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 8, Name = "contextData"), JsonPropertyOrder(8), JsonPropertyName("contextData")]
    public JsonObject ContextData { get; init; } = [];

    /// <inheritdoc/>
    [DataMember(Order = 9, Name = "output"), JsonPropertyOrder(9), JsonPropertyName("output")]
    public JsonNode? Output { get; init; }

    /// <inheritdoc/>
    [DataMember(Order = 10, Name = "error"), JsonPropertyOrder(10), JsonPropertyName("error")]
    public Error? Error { get; init; }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        Status = WorkflowStatus.Running;
        StartedAt = DateTimeOffset.Now;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task ResumeAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SetOutputAsync(JsonNode? output, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task CancelAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}