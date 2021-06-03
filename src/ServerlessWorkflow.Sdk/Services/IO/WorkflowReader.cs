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
using ServerlessWorkflow.Sdk.Services.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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
            string json = Encoding.UTF8.GetString(await this.JsonSerializer.SerializeAsync(workflowDefinition, cancellationToken));
            if (!this.SchemaValidator.Validate(json, out IList<string> validationErrors))
            {

            }
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
            if (workflow.DataInputSchema != null
                && workflow.DataInputSchema is ExternalJSchema externalJSchema
                && !externalJSchema.Loaded)
                workflow.DataInputSchema = await this.LoadJSchemaAsync(externalJSchema.DefinitionUri, cancellationToken);
            if (workflow.Events != null
                && workflow.Events is ExternalDefinitionCollection<EventDefinition> externalEventsDefinition
                && !externalEventsDefinition.Loaded)
                workflow.Events = await this.LoadExternalDefinitionCollectionAsync<EventDefinition>(externalEventsDefinition.DefinitionUri, cancellationToken);
            if(workflow.Functions != null
                && workflow.Functions is ExternalDefinitionCollection<FunctionDefinition> externalFunctionsDefinition
                && !externalFunctionsDefinition.Loaded)
                workflow.Functions = await this.LoadExternalDefinitionCollectionAsync<FunctionDefinition>(externalFunctionsDefinition.DefinitionUri, cancellationToken);
            if (workflow.Retries != null
                && workflow.Retries is ExternalDefinitionCollection<RetryStrategyDefinition> externalRetriesDefinition
                && !externalRetriesDefinition.Loaded)
                workflow.Retries = await this.LoadExternalDefinitionCollectionAsync<RetryStrategyDefinition>(externalRetriesDefinition.DefinitionUri, cancellationToken);
            if (workflow.Constants != null
                && workflow.Constants is ExternalDefinition externalConstantsDefinition
                && !externalConstantsDefinition.Loaded)
                workflow.Constants = (JObject)await this.LoadExternalDefinitionAsync(externalConstantsDefinition.DefinitionUri, cancellationToken);
            return workflow;
        }

        /// <summary>
        /// Loads the <see cref="JSchema"/> at the specified <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> the <see cref="JSchema"/> to load is located at</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The loaded <see cref="JSchema"/></returns>
        protected virtual async Task<JSchema> LoadJSchemaAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            string content;
            if (uri.IsFile)
            {
                string filePath = uri.LocalPath;
                if (filePath.StartsWith("\\\\"))
                    filePath = filePath[2..];
                content = File.ReadAllText(filePath);
            }
            else
            {
                using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
                content = await response.Content?.ReadAsStringAsync(cancellationToken);
                if (!response.IsSuccessStatusCode)
                    response.EnsureSuccessStatusCode();
            }
            if (!content.IsJson())
                content = (await this.YamlSerializer.DeserializeAsync<JObject>(content, cancellationToken)).ToString(Formatting.None);
            return JSchema.Parse(content, new JSchemaUrlResolver());
        }

        /// <summary>
        /// Loads an external definition
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> the external definition to load is located at</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="JToken"/> that represents the object defined in the loaded external definition</returns>
        protected virtual async Task<JToken> LoadExternalDefinitionAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            string content;
            if (uri.IsFile)
            {
                string filePath = uri.LocalPath;
                if (filePath.StartsWith("\\\\"))
                    filePath = filePath[2..];
                content = File.ReadAllText(filePath);
            }
            else
            {
                using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
                content = await response.Content?.ReadAsStringAsync(cancellationToken);
                if (!response.IsSuccessStatusCode)
                    response.EnsureSuccessStatusCode();
            }
            if (content.IsJson())
                return await this.JsonSerializer.DeserializeAsync<JToken>(content, cancellationToken);
            else
                return await this.YamlSerializer.DeserializeAsync<JToken>(content, cancellationToken);
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
            string content;
            if (uri.IsFile)
            {
                string filePath = uri.LocalPath;
                if (filePath.StartsWith("\\\\"))
                    filePath = filePath[2..];
                content = File.ReadAllText(filePath);
            }
            else
            {
                using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
                content = await response.Content?.ReadAsStringAsync(cancellationToken);
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
