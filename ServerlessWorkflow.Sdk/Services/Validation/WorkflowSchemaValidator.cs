using JsonCons.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowSchemaValidator"/> interface
/// </summary>
public class WorkflowSchemaValidator
    : IWorkflowSchemaValidator
{

    /// <summary>
    /// Initializes a new <see cref="WorkflowSchemaValidator"/>
    /// </summary>
    /// <param name="httpClient">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
    public WorkflowSchemaValidator(IHttpClientFactory httpClientFactory)
    {
        this.HttpClient = httpClientFactory.CreateClient();
        SchemaRegistry.Global.Fetch = GetExternalJsonSchema;
    }

    /// <summary>
    /// Gets the service used to perform HTTP requests
    /// </summary>
    protected HttpClient HttpClient { get; }

    /// <summary>
    /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing the loaded Serverless Workflow spec <see cref="JsonSchema"/>s
    /// </summary>
    protected ConcurrentDictionary<string, JsonSchema> Schemas { get; } = new();

    /// <inheritdoc/>
    public async Task<EvaluationResults> ValidateAsync(WorkflowDefinition workflow, CancellationToken cancellationToken = default)
    {
        if (workflow == null) throw new ArgumentNullException(nameof(workflow));
        var jsonDocument = Serialization.Serializer.Json.SerializeToDocument(workflow)!;
        var jsonSchema = await this.GetOrLoadSpecificationSchemaAsync(workflow.SpecVersion, cancellationToken).ConfigureAwait(false);
        if (workflow.Extensions?.Any() == true)
        {
            var jsonSchemaElement = Serialization.Serializer.Json.SerializeToElement(jsonSchema)!.Value;
            var jsonSchemaDocument = Serialization.Serializer.Json.SerializeToDocument(jsonSchema)!;
            foreach (var extension in workflow.Extensions)
            {
                var extensionSchemaElement = await this.GetExtensionSchemaAsync(extension, cancellationToken);
                jsonSchemaDocument = JsonMergePatch.ApplyMergePatch(jsonSchemaElement, extensionSchemaElement);
            }
            jsonSchema = Serialization.Serializer.Json.Deserialize<JsonSchema>(jsonSchemaDocument)!;
        }
        var evaluationOptions = EvaluationOptions.Default;
        evaluationOptions.OutputFormat = OutputFormat.Hierarchical;
        return jsonSchema.Evaluate(jsonDocument, evaluationOptions);
    }

    /// <summary>
    /// Loads the Serverless Workflow <see cref="JsonSchema"/>
    /// </summary>
    /// <returns>The Serverless Workflow <see cref="JsonSchema"/></returns>
    protected virtual async Task<JsonSchema> GetOrLoadSpecificationSchemaAsync(string specVersion, CancellationToken cancellationToken = default)
    {
        if (this.Schemas.TryGetValue(specVersion, out var schema) && schema != null) return schema;
        using var response = await this.HttpClient.GetAsync($"https://serverlessworkflow.io/schemas/{specVersion}/workflow.json", cancellationToken).ConfigureAwait(false);
        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        schema = await JsonSchema.FromStream(stream).ConfigureAwait(false);
        schema = schema.Bundle();
        this.Schemas.TryAdd(specVersion, schema);
        return schema;
    }

    /// <summary>
    /// Retrieves the JSON content of the specified <see cref="ExtensionDefinition"/>'s schema
    /// </summary>
    /// <param name="extension">The <see cref="ExtensionDefinition"/> that defines the referenced JSON schema</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The JSON content of the specified schema</returns>
    protected virtual async Task<JsonElement> GetExtensionSchemaAsync(ExtensionDefinition extension, CancellationToken cancellationToken = default)
    {
        if (extension == null) throw new ArgumentNullException(nameof(extension));
        var uri = extension.Resource;
        if (!uri.IsAbsoluteUri) uri = this.ResolveRelativeUri(uri);
        string json;
        if (uri.IsFile)
        {
            json = await File.ReadAllTextAsync(uri.LocalPath, cancellationToken);
        }
        else
        {
            using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
            json = await response.Content?.ReadAsStringAsync(cancellationToken)!;
        }
        return Serialization.Serializer.Json.Deserialize<JsonElement>(json);
    }

    /// <summary>
    /// Retrieves the <see cref="JsonSchema"/> at the specified <see cref="Uri"/>
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> of the external <see cref="JsonSchema"/> to retrieve</param>
    /// <returns>The <see cref="JsonSchema"/> referenced by the specified <see cref="Uri"/></returns>
    protected virtual JsonSchema GetExternalJsonSchema(Uri uri)
    {
        using var response = this.HttpClient.GetAsync(uri).ConfigureAwait(false).GetAwaiter().GetResult();
        using var stream = response.Content.ReadAsStream();
        using var streamReader = new StreamReader(stream);
        return JsonSchema.FromText(streamReader.ReadToEnd());
    }

    /// <summary>
    /// Resolves the specified relative <see cref="Uri"/>
    /// </summary>
    /// <param name="uri">The relative <see cref="Uri"/> to resolve</param>
    /// <returns>The resolved <see cref="Uri"/></returns>
    protected virtual Uri ResolveRelativeUri(Uri uri)
    {
        if (uri == null) throw new ArgumentNullException(nameof(uri));
        var localPath = uri.ToString();
        if (localPath.StartsWith("//") || localPath.StartsWith("\\\\")) localPath = localPath[2..];
        return new Uri(Path.Combine(AppContext.BaseDirectory, localPath));
    }

    /// <summary>
    /// Creates a new <see cref="WorkflowSchemaValidator"/>
    /// </summary>
    /// <returns>A new <see cref="WorkflowSchemaValidator"/></returns>
    public static WorkflowSchemaValidator Create()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddHttpClient();
        services.AddSingleton<WorkflowSchemaValidator>();
        services.AddSingleton<IWorkflowSchemaValidator>(provider => provider.GetRequiredService<WorkflowSchemaValidator>());
        return services.BuildServiceProvider().GetRequiredService<WorkflowSchemaValidator>();
    }

}