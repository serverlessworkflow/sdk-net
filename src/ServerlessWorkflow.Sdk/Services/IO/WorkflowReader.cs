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
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.IO
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IWorkflowReader"/> interface
    /// </summary>
    public class WorkflowReader
        : IWorkflowReader
    {

        /// <summary>
        /// Initializes a new <see cref="WorkflowReader"/>
        /// </summary>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="schemaValidator">The service used to validate <see cref="WorkflowDefinition"/>s</param>
        /// <param name="dslValidators">An <see cref="IEnumerable{T}"/> containing the services used to validate Serverless Workflow DSL</param>
        /// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
        /// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
        /// <param name="httpClientFactory">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
        public WorkflowReader(ILogger<WorkflowReader> logger, IWorkflowSchemaValidator schemaValidator, IEnumerable<IValidator<WorkflowDefinition>> dslValidators, IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer, IHttpClientFactory httpClientFactory)
        {
            this.Logger = logger;
            this.SchemaValidator = schemaValidator;
            this.DslValidators = dslValidators;
            this.JsonSerializer = jsonSerializer;
            this.YamlSerializer = yamlSerializer;
            this.HttpClient = httpClientFactory.CreateClient();
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to validate <see cref="WorkflowDefinition"/>s
        /// </summary>
        protected IWorkflowSchemaValidator SchemaValidator { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the services used to validate Serverless Workflow DSL
        /// </summary>
        protected IEnumerable<IValidator<WorkflowDefinition>> DslValidators { get; }

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
        public virtual async Task<WorkflowDefinition> ReadAsync(Stream stream, WorkflowReaderOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if(options == null)
                options = new WorkflowReaderOptions();
            ISerializer serializer;
            var offset = stream.Position;
            using var reader = new StreamReader(stream);
            var input = reader.ReadToEnd();
            stream.Position = offset;
            if(input.IsJson())
                serializer = this.JsonSerializer;
            else
                serializer = this.YamlSerializer;
            var workflowDefinition = await serializer.DeserializeAsync<WorkflowDefinition>(stream, cancellationToken);
            await this.LoadExternalDefinitionsForAsync(workflowDefinition, options, cancellationToken);
            return workflowDefinition;
        }

        /// <summary>
        /// Loads external definitions for the specified <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to load external definitions for</param>
        /// <param name="options">The <see cref="WorkflowReaderOptions"/> to use</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task LoadExternalDefinitionsForAsync(WorkflowDefinition workflow, WorkflowReaderOptions options, CancellationToken cancellationToken = default)
        {
            if (workflow == null)
                throw new ArgumentNullException(nameof(workflow));
            if (workflow.DataInputSchemaUri != null)
                workflow.DataInputSchema = await this.LoadDataInputSchemaAsync(workflow.DataInputSchemaUri, options, cancellationToken); //todo: load schema sub property
            if (workflow.EventsUri != null)
                workflow.Events = await this.LoadExternalDefinitionCollectionAsync<EventDefinition>(workflow.EventsUri, options, cancellationToken);
            if(workflow.FunctionsUri != null)
                workflow.Functions = await this.LoadExternalDefinitionCollectionAsync<FunctionDefinition>(workflow.FunctionsUri, options, cancellationToken);
            if (workflow.RetriesUri != null)
                workflow.Retries = await this.LoadExternalDefinitionCollectionAsync<RetryDefinition>(workflow.RetriesUri, options, cancellationToken);
            if (workflow.ConstantsUri != null)
                workflow.Constants = await this.LoadExternalDefinitionAsync(workflow.ConstantsUri, options, cancellationToken);
            if (workflow.SecretsUri != null)
                workflow.Secrets = await this.LoadExternalDefinitionCollectionAsync<string>(workflow.SecretsUri, options, cancellationToken);
            if (workflow.AuthUri != null)
                workflow.Auth = await this.LoadExternalDefinitionCollectionAsync<AuthenticationDefinition>(workflow.AuthUri, options, cancellationToken);
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
        /// <returns>A new <see cref="JToken"/> that represents the object defined in the loaded external definition</returns>
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
            if(uri == null)
                throw new ArgumentNullException(nameof(uri));
            switch (options.RelativeUriResolutionMode)
            {
                case RelativeUriReferenceResolutionMode.ConvertToAbsolute:
                    if (options.BaseUri == null)
                        throw new NullReferenceException($"The '{nameof(WorkflowReaderOptions.BaseUri)}' property must be set when using the specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}'");
                    return new(options.BaseUri, uri.ToString());
                case RelativeUriReferenceResolutionMode.ConvertToRelativeFilePath:
                    var localPath = uri.LocalPath;
                    if(localPath.StartsWith("//") || localPath.StartsWith("\\\\"))
                        localPath = localPath.Substring(2);
                    return new Uri(Path.Combine(options.BaseDirectory, localPath));
                case RelativeUriReferenceResolutionMode.None:
                    throw new NotSupportedException($"Relative uris are not supported when using the specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}'");
                default:
                    throw new NotSupportedException($"The specified {nameof(RelativeUriReferenceResolutionMode)} '{RelativeUriReferenceResolutionMode.ConvertToAbsolute}' is not supported");
            }
        }

        /// <summary>
        /// Creates a new default instance of the <see cref="IWorkflowReader"/> interface
        /// </summary>
        /// <returns>A new <see cref="IWorkflowReader"/></returns>
        public static IWorkflowReader Create()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServerlessWorkflow();
            return services.BuildServiceProvider().GetRequiredService<IWorkflowReader>();
        }

    }

}
