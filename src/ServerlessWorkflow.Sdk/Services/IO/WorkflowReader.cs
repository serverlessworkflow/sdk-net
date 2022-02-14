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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
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
        /// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
        /// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
        /// <param name="httpClientFactory">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
        public WorkflowReader(ILogger<WorkflowReader> logger, IWorkflowSchemaValidator schemaValidator, IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer, IHttpClientFactory httpClientFactory)
        {
            this.Logger = logger;
            this.SchemaValidator = schemaValidator;
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
        public virtual async Task<WorkflowDefinition> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            ISerializer serializer;
            long offset = stream.Position;
            using StreamReader reader = new(stream);
            string input = reader.ReadToEnd();
            stream.Position = offset;
            if(input.IsJson())
                serializer = this.JsonSerializer;
            else
                serializer = this.YamlSerializer;
            WorkflowDefinition workflowDefinition = await this.LoadExternalDefinitionsForAsync(await serializer.DeserializeAsync<WorkflowDefinition>(stream, cancellationToken), cancellationToken);
            IList<ValidationError> validationErrors = await this.SchemaValidator.ValidateAsync(workflowDefinition, cancellationToken);
            if (validationErrors.Any())
                throw new ValidationException($"Failed to validate the specified workflow definition:{Environment.NewLine}{string.Join(Environment.NewLine, validationErrors.Select(e => $"Message: {e.Message}{Environment.NewLine}Schema path: {e.Path}{Environment.NewLine}Line: {e.LineNumber}, Position: {e.LinePosition}"))}");
            return workflowDefinition;
        }

        /// <summary>
        /// Loads external definitions for the specified <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to load external definitions for</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The updated <see cref="WorkflowDefinition"/></returns>
        protected virtual async Task<WorkflowDefinition> LoadExternalDefinitionsForAsync(WorkflowDefinition workflow, CancellationToken cancellationToken = default)
        {
            if (workflow == null)
                throw new ArgumentNullException(nameof(workflow));
            if (workflow.DataInputSchemaUri != null)
                workflow.DataInputSchema = await this.LoadDataInputSchemaAsync(workflow.DataInputSchemaUri, cancellationToken); //todo: load schema sub property
            if (workflow.EventsUri != null)
                workflow.Events = await this.LoadExternalDefinitionCollectionAsync<EventDefinition>(workflow.EventsUri, cancellationToken);
            if(workflow.FunctionsUri != null)
                workflow.Functions = await this.LoadExternalDefinitionCollectionAsync<FunctionDefinition>(workflow.FunctionsUri, cancellationToken);
            if (workflow.RetriesUri != null)
                workflow.Retries = await this.LoadExternalDefinitionCollectionAsync<RetryDefinition>(workflow.RetriesUri, cancellationToken);
            if (workflow.ConstantsUri != null)
                workflow.Constants = await this.LoadExternalDefinitionAsync(workflow.ConstantsUri, cancellationToken);
            if (workflow.SecretsUri != null)
                workflow.Secrets = await this.LoadExternalDefinitionCollectionAsync<string>(workflow.SecretsUri, cancellationToken);
            if (workflow.AuthUri != null)
                workflow.Auth = await this.LoadExternalDefinitionCollectionAsync<AuthenticationDefinition>(workflow.AuthUri, cancellationToken);
            return workflow;
        }

        /// <summary>
        /// Loads the <see cref="DataInputSchemaDefinition"/> at the specified <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> the <see cref="DataInputSchemaDefinition"/> to load is located at</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The loaded <see cref="DataInputSchemaDefinition"/></returns>
        protected virtual async Task<DataInputSchemaDefinition> LoadDataInputSchemaAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            string? content;
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
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="JToken"/> that represents the object defined in the loaded external definition</returns>
        protected virtual async Task<Any> LoadExternalDefinitionAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            string? content;
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
                return await this.JsonSerializer.DeserializeAsync<Any>(content, cancellationToken);
            else
                return await this.YamlSerializer.DeserializeAsync<Any>(content, cancellationToken);
        }

        /// <summary>
        /// Loads external definitions of the specified type
        /// </summary>
        /// <typeparam name="T">The type of external definition to load</typeparam>
        /// <param name="uri">The <see cref="Uri"/> the external definition to load is located at</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="List{T}"/> containing the elements defined by the loaded external definition</returns>
        protected virtual async Task<List<T>> LoadExternalDefinitionCollectionAsync<T>(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            string? content;
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
