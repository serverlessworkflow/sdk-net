/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Octokit;
using ServerlessWorkflow.Sdk.Models;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
    /// <param name="serializer">The service used to serialize and deserialize JSON</param>
    /// <param name="httpClientFactory">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
    public WorkflowSchemaValidator(IJsonSerializer serializer, IHttpClientFactory httpClientFactory)
    {
        this.Serializer = serializer;
        this.HttpClient = httpClientFactory.CreateClient();
    }

    /// <summary>
    /// Gets the service used to serialize and deserialize JSON
    /// </summary>
    protected IJsonSerializer Serializer { get; }

    /// <summary>
    /// Gets the <see cref="System.Net.Http.HttpClient"/> used to fetch the Serverless Workflow schema
    /// </summary>
    protected HttpClient HttpClient { get; }

    /// <summary>
    /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing the loaded Serverless Workflow spec <see cref="JSchema"/>s
    /// </summary>
    protected ConcurrentDictionary<string, JSchema> Schemas { get;  } = new();

    /// <summary>
    /// Gets the service used to resolve <see cref="JSchema"/>s by <see cref="Uri"/>
    /// </summary>
    protected JSchemaPreloadedResolver SchemaResolver { get; } = new();

    /// <inheritdoc/>
    public virtual async Task<IList<ValidationError>> ValidateAsync(string workflowJson, string specVersion, CancellationToken cancellationToken = default)
    {
        var workflow = this.Serializer.Deserialize<JObject>(workflowJson);
        var schema = await this.LoadSpecificationSchemaAsync(specVersion, cancellationToken);
        workflow.IsValid(schema, out IList<ValidationError> validationErrors);
        return validationErrors;
    }

    /// <inheritdoc/>
    public virtual async Task<IList<ValidationError>> ValidateAsync(WorkflowDefinition workflow, CancellationToken cancellationToken = default)
    {
        if (workflow == null) throw new ArgumentNullException(nameof(workflow));
        var serializerSettings = JsonConvert.DefaultSettings?.Invoke();
        serializerSettings ??= new();
        serializerSettings.DefaultValueHandling = DefaultValueHandling.Populate | DefaultValueHandling.Ignore;
        var obj = JObject.FromObject(workflow, Newtonsoft.Json.JsonSerializer.Create(serializerSettings));
        var schema = await this.LoadSpecificationSchemaAsync(workflow.SpecVersion, cancellationToken);
        if(workflow.Extensions?.Any() == true)
        {
            var schemaObject = JObject.FromObject(schema, Newtonsoft.Json.JsonSerializer.Create(serializerSettings));
            foreach(var extension in workflow.Extensions)
            {
                var extensionSchemaObject = await this.GetExtensionSchemaObjectAsync(extension, cancellationToken);
                schemaObject.Merge(extensionSchemaObject);
            }
            var json = JsonConvert.SerializeObject(schemaObject, serializerSettings);
            schema = JSchema.Parse(json, this.SchemaResolver);
        }
        obj.IsValid(schema, out IList<ValidationError> validationErrors);
        return validationErrors;
    }

    /// <summary>
    /// Loads the Serverless Workflow <see cref="JSchema"/>
    /// </summary>
    /// <returns>The Serverless Workflow <see cref="JSchema"/></returns>
    protected virtual async Task<JSchema> LoadSpecificationSchemaAsync(string specVersion, CancellationToken cancellationToken = default)
    {
        if (this.Schemas.TryGetValue(specVersion, out var schema)) return schema;
        var client = new GitHubClient(new ProductHeaderValue("serverless-workflow-sdk-net"));
        var specJson = null as string;
        foreach (var content in await client.Repository.Content.GetAllContentsByRef("serverlessworkflow", "specification", "schema", $"{specVersion[..3]}.x"))
        {
            if (string.IsNullOrWhiteSpace(content.DownloadUrl)) continue;
            var json = await this.GetSpecificationSchemaJsonAsync(new(content.DownloadUrl), specVersion, cancellationToken);
            if (content.Name == "workflow.json") specJson = json;
        }
        schema = JSchema.Parse(specJson!, this.SchemaResolver);
        this.Schemas.TryAdd(specVersion, schema);
        return schema;
    }

    /// <summary>
    /// Retrieves the JSON content of the specified schema
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> of the referenced JSON schema</param>
    /// <param name="specVersion">The Serverless Workflow specification version</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The JSON content of the specified schema</returns>
    protected virtual async Task<string> GetSpecificationSchemaJsonAsync(Uri uri, string specVersion, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
        var json = await response.Content?.ReadAsStringAsync(cancellationToken)!;
        this.SchemaResolver.Add(new($"https://serverlessworkflow.io/schemas/{specVersion[..3]}/{uri.PathAndQuery.Split('/', StringSplitOptions.RemoveEmptyEntries).Last()}"), json);
        return json;
    }

    /// <summary>
    /// Retrieves the JSON content of the specified <see cref="ExtensionDefinition"/>'s schema
    /// </summary>
    /// <param name="extension">The <see cref="ExtensionDefinition"/> that defines the referenced JSON schema</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The JSON content of the specified schema</returns>
    protected virtual async Task<JObject> GetExtensionSchemaObjectAsync(ExtensionDefinition extension, CancellationToken cancellationToken = default)
    {
        if(extension == null) throw new ArgumentNullException(nameof(extension));
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
        return JObject.Parse(json)!;
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

}
