namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IExternalResourceReader"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="httpClient">The <see cref="HttpClient"/> used to read external resources over http</param>
public sealed class ExternalResourceReader(IServiceProvider serviceProvider, HttpClient httpClient)
    : IExternalResourceReader
{

    /// <inheritdoc/>
    public async Task<Stream> ReadAsync(ExternalResourceDefinition resource, WorkflowDefinition? workflow = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(resource);
        var endpointUri = resource.Endpoint.Match(
            ep => ep.Uri,
            uri => uri);
        return endpointUri.Scheme switch
        {
            "file" => new FileStream(endpointUri.LocalPath, FileMode.Open),
            "http" or "https" => await ReadOverHttpAsync(endpointUri, resource, workflow, cancellationToken).ConfigureAwait(false),
            _ => throw new NotSupportedException($"Cannot retrieve resource at uri '{endpointUri}': the scheme '{endpointUri.Scheme}' is not supported")
        };
    }

    async Task<Stream> ReadOverHttpAsync(Uri endpointUri, ExternalResourceDefinition resource, WorkflowDefinition? workflow, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(resource);
        if (resource.Endpoint.TryGetAsT1(out var endpoint) && endpoint is not null && endpoint.Authentication is not null) await httpClient.ConfigureAuthenticationAsync(endpoint.Authentication, serviceProvider, workflow, cancellationToken).ConfigureAwait(false);
        return await httpClient.GetStreamAsync(endpointUri, cancellationToken).ConfigureAwait(false);
    }

}