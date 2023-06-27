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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.IO
{
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
        public WorkflowExternalDefinitionResolver(ILogger<WorkflowReader> logger, IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer, IHttpClientFactory httpClientFactory)
        {
            this.Logger = logger;
            this.JsonSerializer = jsonSerializer;
            this.YamlSerializer = yamlSerializer;
            this.HttpClient = httpClientFactory.CreateClient();
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to serialize and deserialize JSON
        /// </summary>
        protected IJsonSerializer JsonSerializer { get; }

        /// <summary>
        /// Gets the service used to serialize and deserialize YAML
        /// </summary>
        protected IYamlSerializer YamlSerializer { get; }

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> used to retrieve external definitions
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <inheritdoc/>
        public virtual async Task<WorkflowDefinition> LoadExternalDefinitionsAsync(WorkflowDefinition workflow, WorkflowReaderOptions options, CancellationToken cancellationToken = default)
        {
            if (workflow == null)
                throw new ArgumentNullException(nameof(workflow));
            var loadedWorkflow = await this.JsonSerializer.DeserializeAsync<WorkflowDefinition>(await this.JsonSerializer.SerializeAsync(workflow, cancellationToken), cancellationToken);
            if (loadedWorkflow.DataInputSchemaUri != null
                && loadedWorkflow.DataInputSchema == null)
                loadedWorkflow.DataInputSchema = await this.LoadDataInputSchemaAsync(loadedWorkflow.DataInputSchemaUri, options, cancellationToken); //todo: load schema sub property
            if (loadedWorkflow.EventsUri != null
                && loadedWorkflow.Events == null)
                loadedWorkflow.Events = await this.LoadExternalDefinitionCollectionAsync<EventDefinition>(loadedWorkflow.EventsUri, options, cancellationToken);
            if (loadedWorkflow.FunctionsUri != null
                && loadedWorkflow.Functions == null)
                loadedWorkflow.Functions = await this.LoadExternalDefinitionCollectionAsync<FunctionDefinition>(loadedWorkflow.FunctionsUri, options, cancellationToken);
            if (loadedWorkflow.RetriesUri != null
                && loadedWorkflow.Retries == null)
                loadedWorkflow.Retries = await this.LoadExternalDefinitionCollectionAsync<RetryDefinition>(loadedWorkflow.RetriesUri, options, cancellationToken);
            if (loadedWorkflow.ConstantsUri != null
                && loadedWorkflow.Constants == null)
                loadedWorkflow.Constants = await this.LoadExternalDefinitionAsync(loadedWorkflow.ConstantsUri, options, cancellationToken);
            if (loadedWorkflow.SecretsUri != null
                && loadedWorkflow.Secrets == null)
                loadedWorkflow.Secrets = await this.LoadExternalDefinitionCollectionAsync<string>(loadedWorkflow.SecretsUri, options, cancellationToken);
            if (loadedWorkflow.AuthUri != null
                && loadedWorkflow.Auth == null)
                loadedWorkflow.Auth = await this.LoadExternalDefinitionCollectionAsync<AuthenticationDefinition>(loadedWorkflow.AuthUri, options, cancellationToken);
            return loadedWorkflow;
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
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            string? content;
            if (!uri.IsAbsoluteUri
                 || (uri.IsFile && Path.IsPathRooted(uri.LocalPath)))
                uri = this.ResolveRelativeUri(uri, options);
            if (uri.IsFile)
            {
                string filePath = uri.LocalPath;
                if (filePath.StartsWith('/'))
                    filePath = filePath[1..];
                content = File.ReadAllText(filePath);
            }
            else
            {
                using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
                content = await response.Content?.ReadAsStringAsync(cancellationToken)!;
                if (!response.IsSuccessStatusCode)
                    response.EnsureSuccessStatusCode();
            }
            if (!content.IsJson())
                content = (await this.YamlSerializer.DeserializeAsync<JObject>(content, cancellationToken)).ToString(Formatting.None);
            return JsonConvert.DeserializeObject<DataInputSchemaDefinition>(content)!;
        }

        /// <summary>
        /// Loads an external definition
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> the external definition to load is located at</param>
        /// <param name="options">The <see cref="WorkflowReaderOptions"/> to use</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new object that represents the object defined in the loaded external definition</returns>
        protected virtual async Task<DynamicObject> LoadExternalDefinitionAsync(Uri uri, WorkflowReaderOptions options, CancellationToken cancellationToken = default)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            string? content;
            if (!uri.IsAbsoluteUri
                || (uri.IsFile && Path.IsPathRooted(uri.LocalPath)))
                uri = this.ResolveRelativeUri(uri, options);
            if (uri.IsFile)
            {
                string filePath = uri.LocalPath;
                if (filePath.StartsWith('/'))
                    filePath = filePath[1..];
                content = File.ReadAllText(filePath);
            }
            else
            {
                using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
                content = await response.Content?.ReadAsStringAsync(cancellationToken)!;
                if (!response.IsSuccessStatusCode)
                    response.EnsureSuccessStatusCode();
            }
            if (content.IsJson())
                return await this.JsonSerializer.DeserializeAsync<DynamicObject>(content, cancellationToken);
            else
                return await this.YamlSerializer.DeserializeAsync<DynamicObject>(content, cancellationToken);
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
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            string? content;
            if (!uri.IsAbsoluteUri
                || (uri.IsFile && Path.IsPathRooted(uri.LocalPath)))
                uri = this.ResolveRelativeUri(uri, options);
            if (uri.IsFile)
            {
                string filePath = uri.LocalPath;
                if (filePath.StartsWith("/"))
                    filePath = filePath[1..];
                content = File.ReadAllText(filePath);
            }
            else
            {
                using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
                content = await response.Content?.ReadAsStringAsync(cancellationToken)!;
                if (!response.IsSuccessStatusCode)
                    response.EnsureSuccessStatusCode();
            }
            if (content.IsJson())
                return await this.JsonSerializer.DeserializeAsync<ExternalDefinitionCollection<T>>(content, cancellationToken);
            else
                return await this.YamlSerializer.DeserializeAsync<ExternalDefinitionCollection<T>>(content, cancellationToken);
        }

        /// <summary>
        /// Resolves the specified relative <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">The relative <see cref="Uri"/> to resolve</param>
        /// <param name="options">The <see cref="WorkflowReaderOptions"/> to use</param>
        /// <returns>The resolved <see cref="Uri"/></returns>
        protected virtual Uri ResolveRelativeUri(Uri uri, WorkflowReaderOptions options)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            switch (options.RelativeUriResolutionMode)
            {
                case RelativeUriReferenceResolutionMode.ConvertToAbsolute:
                    if (options.BaseUri == null)
                        throw new NullReferenceException($"The '{nameof(WorkflowReaderOptions.BaseUri)}' property must be set when using the specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}'");
                    return new(options.BaseUri, uri.ToString());
                case RelativeUriReferenceResolutionMode.ConvertToRelativeFilePath:
                    var localPath = uri.LocalPath;
                    if (localPath.StartsWith("//") || localPath.StartsWith("\\\\"))
                        localPath = localPath[2..];
                    return new Uri(Path.Combine(options.BaseDirectory, localPath));
                case RelativeUriReferenceResolutionMode.None:
                    throw new NotSupportedException($"Relative uris are not supported when using the specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}'");
                default:
                    throw new NotSupportedException($"The specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}' is not supported");
            }
        }

    }

}
