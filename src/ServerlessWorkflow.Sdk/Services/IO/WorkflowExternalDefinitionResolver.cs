using Microsoft.Extensions.Logging;

namespace ServerlessWorkflow.Sdk.Services.IO;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowExternalDefinitionResolver"/> interface
/// </summary>
public class WorkflowExternalDefinitionResolver
    : IWorkflowExternalDefinitionResolver
{

    /// <summary>
    /// Initializes a new <see cref="WorkflowExternalDefinitionResolver"/>
    /// </summary>
    /// <param name="logger">The service used to perform logging</param>
    /// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
    /// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
    /// <param name="httpClientFactory">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
    public WorkflowExternalDefinitionResolver(ILogger<WorkflowReader> logger, IHttpClientFactory httpClientFactory)
    {
        this.Logger = logger;
        this.HttpClient = httpClientFactory.CreateClient();
    }

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Gets the <see cref="System.Net.Http.HttpClient"/> used to retrieve external definitions
    /// </summary>
    protected HttpClient HttpClient { get; }

    /// <inheritdoc/>
    public virtual async Task<WorkflowDefinition> LoadExternalDefinitionsAsync(WorkflowDefinition workflow, WorkflowReaderOptions options, CancellationToken cancellationToken = default)
    {
        if (workflow == null) throw new ArgumentNullException(nameof(workflow));
        var bundledWorkflow = Serialization.Serializer.Json.Deserialize<WorkflowDefinition>(Serialization.Serializer.Json.Serialize(workflow))!;
        if (bundledWorkflow.DataInputSchemaUri != null && bundledWorkflow.DataInputSchema == null) bundledWorkflow.DataInputSchema = await this.LoadDataInputSchemaAsync(bundledWorkflow.DataInputSchemaUri, options, cancellationToken).ConfigureAwait(false); //todo: load schema sub property
        if (bundledWorkflow.EventsUri != null && bundledWorkflow.Events == null) bundledWorkflow.Events = await this.LoadExternalDefinitionCollectionAsync<EventDefinition>(bundledWorkflow.EventsUri, options, cancellationToken).ConfigureAwait(false);
        if (bundledWorkflow.FunctionsUri != null && bundledWorkflow.Functions == null) bundledWorkflow.Functions = await this.LoadExternalDefinitionCollectionAsync<FunctionDefinition>(bundledWorkflow.FunctionsUri, options, cancellationToken).ConfigureAwait(false);
        if (bundledWorkflow.RetriesUri != null && bundledWorkflow.Retries == null) bundledWorkflow.Retries = await this.LoadExternalDefinitionCollectionAsync<RetryDefinition>(bundledWorkflow.RetriesUri, options, cancellationToken).ConfigureAwait(false);
        if (bundledWorkflow.ConstantsUri != null && bundledWorkflow.Constants == null) bundledWorkflow.Constants = await this.LoadExternalDefinitionAsync(bundledWorkflow.ConstantsUri, options, cancellationToken).ConfigureAwait(false);
        if (bundledWorkflow.SecretsUri != null && bundledWorkflow.Secrets == null) bundledWorkflow.Secrets = await this.LoadExternalDefinitionCollectionAsync<string>(bundledWorkflow.SecretsUri, options, cancellationToken).ConfigureAwait(false);
        if (bundledWorkflow.AuthUri != null && bundledWorkflow.Auth == null) bundledWorkflow.Auth = await this.LoadExternalDefinitionCollectionAsync<AuthenticationDefinition>(bundledWorkflow.AuthUri, options, cancellationToken).ConfigureAwait(false);
        return bundledWorkflow;
    }

    /// <summary>
    /// Loads the <see cref="DataInputSchemaDefinition"/> at the specified <see cref="Uri"/>
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> the <see cref="DataInputSchemaDefinition"/> to load is located at</param>
    /// <param name="options">The <see cref="WorkflowReaderOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The loaded <see cref="DataInputSchemaDefinition"/></returns>
    protected virtual async Task<DataInputSchemaDefinition> LoadDataInputSchemaAsync(Uri uri, WorkflowReaderOptions options, CancellationToken cancellationToken = default)
    {
        if (uri == null)throw new ArgumentNullException(nameof(uri));
        string? content;
        if (!uri.IsAbsoluteUri|| (uri.IsFile && Path.IsPathRooted(uri.LocalPath))) uri = this.ResolveRelativeUri(uri, options);
        if (uri.IsFile)
        {
            var filePath = uri.LocalPath;
            if (filePath.StartsWith('/')) filePath = filePath[1..];
            content = File.ReadAllText(filePath);
        }
        else
        {
            using var response = await this.HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            content = await response.Content?.ReadAsStringAsync(cancellationToken)!;
            if (!response.IsSuccessStatusCode) response.EnsureSuccessStatusCode();
        }
        if (!content.IsJson()) content = Serialization.Serializer.Json.Serialize(Serialization.Serializer.Yaml.Deserialize<IDictionary<string, object>>(content));
        return Serialization.Serializer.Json.Deserialize<DataInputSchemaDefinition>(content)!;
    }

    /// <summary>
    /// Loads an external definition
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> the external definition to load is located at</param>
    /// <param name="options">The <see cref="WorkflowReaderOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new object that represents the object defined in the loaded external definition</returns>
    protected virtual async Task<DynamicMapping> LoadExternalDefinitionAsync(Uri uri, WorkflowReaderOptions options, CancellationToken cancellationToken = default)
    {
        if (uri == null)throw new ArgumentNullException(nameof(uri));
        string? content;
        if (!uri.IsAbsoluteUri|| (uri.IsFile && Path.IsPathRooted(uri.LocalPath)))uri = this.ResolveRelativeUri(uri, options);
        if (uri.IsFile)
        {
            var filePath = uri.LocalPath;
            if (filePath.StartsWith('/'))filePath = filePath[1..];
            content = File.ReadAllText(filePath);
        }
        else
        {
            using var response = await this.HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            content = await response.Content?.ReadAsStringAsync(cancellationToken)!;
            if (!response.IsSuccessStatusCode)response.EnsureSuccessStatusCode();
        }
        if (content.IsJson()) return Serialization.Serializer.Json.Deserialize<DynamicMapping>(content)!;
        else return Serialization.Serializer.Yaml.Deserialize<DynamicMapping>(content)!;
    }

    /// <summary>
    /// Loads external definitions of the specified type
    /// </summary>
    /// <typeparam name="T">The type of external definition to load</typeparam>
    /// <param name="uri">The <see cref="Uri"/> the external definition to load is located at</param>
    /// <param name="options">The <see cref="WorkflowReaderOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="List{T}"/> containing the elements defined by the loaded external definition</returns>
    protected virtual async Task<List<T>> LoadExternalDefinitionCollectionAsync<T>(Uri uri, WorkflowReaderOptions options, CancellationToken cancellationToken = default)
    {
        if (uri == null) throw new ArgumentNullException(nameof(uri));
        string? content;
        if (!uri.IsAbsoluteUri || (uri.IsFile && Path.IsPathRooted(uri.LocalPath))) uri = this.ResolveRelativeUri(uri, options);
        if (uri.IsFile)
        {
            var filePath = uri.LocalPath;
            if (filePath.StartsWith("/")) filePath = filePath[1..];
            content = File.ReadAllText(filePath);
        }
        else
        {
            using var response = await this.HttpClient.GetAsync(uri, cancellationToken);
            content = await response.Content?.ReadAsStringAsync(cancellationToken)!;
            if (!response.IsSuccessStatusCode) response.EnsureSuccessStatusCode();
        }
        if (content.IsJson()) return Serialization.Serializer.Json.Deserialize<ExternalDefinitionCollection<T>>(content)!;
        else return Serialization.Serializer.Yaml.Deserialize<ExternalDefinitionCollection<T>>(content)!;
    }

    /// <summary>
    /// Resolves the specified relative <see cref="Uri"/>
    /// </summary>
    /// <param name="uri">The relative <see cref="Uri"/> to resolve</param>
    /// <param name="options">The <see cref="WorkflowReaderOptions"/> to use</param>
    /// <returns>The resolved <see cref="Uri"/></returns>
    protected virtual Uri ResolveRelativeUri(Uri uri, WorkflowReaderOptions options)
    {
        if (uri == null) throw new ArgumentNullException(nameof(uri));
        switch (options.RelativeUriResolutionMode)
        {
            case RelativeUriReferenceResolutionMode.ConvertToAbsolute:
                if (options.BaseUri == null) throw new NullReferenceException($"The '{nameof(WorkflowReaderOptions.BaseUri)}' property must be set when using the specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}'");
                return new(options.BaseUri, uri.ToString());
            case RelativeUriReferenceResolutionMode.ConvertToRelativeFilePath:
                var localPath = uri.LocalPath;
                if (localPath.StartsWith("//") || localPath.StartsWith("\\\\")) localPath = localPath[2..];
                return new Uri(Path.Combine(options.BaseDirectory, localPath));
            case RelativeUriReferenceResolutionMode.None:  throw new NotSupportedException($"Relative uris are not supported when using the specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}'");
            default: throw new NotSupportedException($"The specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}' is not supported");
        }
    }

}
