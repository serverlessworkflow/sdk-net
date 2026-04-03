namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ISchemaHandlerProvider"/> interface
/// </summary>
/// <param name="handlers">An <see cref="IEnumerable{T}"/> containing all registered <see cref="ISchemaHandler"/>s</param>
public sealed class SchemaHandlerProvider(IEnumerable<ISchemaHandler> handlers)
    : ISchemaHandlerProvider
{

    /// <inheritdoc/>
    public ISchemaHandler? GetHandler(string format)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(format);
        format = format.Trim();
        return handlers.FirstOrDefault(h => h.Supports(format));
    }

}
