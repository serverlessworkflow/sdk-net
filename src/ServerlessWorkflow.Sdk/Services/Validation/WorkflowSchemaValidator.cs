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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Octokit;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

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
            if (workflow == null)
                throw new ArgumentNullException(nameof(workflow));
            var obj = JObject.FromObject(workflow);
            var schema = await this.LoadSpecificationSchemaAsync(workflow.SpecVersion, cancellationToken);
            obj.IsValid(schema, out IList<ValidationError> validationErrors);
            return validationErrors;
        }

        /// <summary>
        /// Loads the Serverless Workflow <see cref="JSchema"/>
        /// </summary>
        /// <returns>The Serverless Workflow <see cref="JSchema"/></returns>
        protected virtual async Task<JSchema> LoadSpecificationSchemaAsync(string specVersion, CancellationToken cancellationToken = default)
        {
            if (this.Schemas.TryGetValue(specVersion, out var schema))
                return schema;
            var client = new GitHubClient(new ProductHeaderValue("serverless-workflow-sdk-net"));
            var specJson = null as string;
            foreach (var content in await client.Repository.Content.GetAllContentsByRef("serverlessworkflow", "specification", "schema", $"{specVersion[..3]}.x"))
            {
                if (string.IsNullOrWhiteSpace(content.DownloadUrl))
                    continue;
                var json = await this.GetSchemaJsonAsync(new(content.DownloadUrl), specVersion, cancellationToken);
                if (content.Name == "workflow.json")
                    specJson = json;
            }
            schema = JSchema.Parse(specJson!, this.SchemaResolver);
            this.Schemas.TryAdd(specVersion, schema);
            return schema;
        }

        protected virtual async Task<string> GetSchemaJsonAsync(Uri uri, string specVersion, CancellationToken cancellationToken = default)
        {
            using HttpResponseMessage response = await this.HttpClient.GetAsync(uri, cancellationToken);
            var json = await response.Content?.ReadAsStringAsync(cancellationToken)!;
            this.SchemaResolver.Add(new($"https://serverlessworkflow.io/schemas/{specVersion[..3]}/{uri.PathAndQuery.Split('/', StringSplitOptions.RemoveEmptyEntries).Last()}"), json);
            return json;
        }

    }

}
