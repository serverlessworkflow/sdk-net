namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an in-memory implementation of the <see cref="IWorkflowDefinitionStore"/> interface
/// </summary>
public sealed class InMemoryWorkflowDefinitionStore
    : IWorkflowDefinitionStore
{

    readonly List<WorkflowDefinition> definitions = [];

    /// <inheritdoc/>
    public Task AddAsync(WorkflowDefinition definition, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        definitions.Add(definition);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<WorkflowDefinition?> GetAsync(string @namespace, string name, string? version = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return Task.FromResult(definitions.FirstOrDefault(d => d.Document.Namespace.Equals(@namespace, StringComparison.OrdinalIgnoreCase) && d.Document.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && (version is null || d.Document.Version.Equals(version, StringComparison.OrdinalIgnoreCase))));
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<WorkflowDefinition> ListAsync(CancellationToken cancellationToken = default) => definitions.ToAsyncEnumerable();

    /// <inheritdoc/>
    public IAsyncEnumerable<WorkflowDefinition> ListAsync(string @namespace, CancellationToken cancellationToken = default) => definitions.Where(d => d.Document.Namespace.Equals(@namespace, StringComparison.OrdinalIgnoreCase)).ToAsyncEnumerable();

    /// <inheritdoc/>
    public IAsyncEnumerable<WorkflowDefinition> ListAsync(string @namespace, string name, CancellationToken cancellationToken = default) => definitions.Where(d => d.Document.Namespace.Equals(@namespace, StringComparison.OrdinalIgnoreCase) && d.Document.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToAsyncEnumerable();

}