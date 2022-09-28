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
using ServerlessWorkflow.Sdk.Models;
using System;
using System.IO;
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
        /// <param name="externalDefinitionResolver">The service used to resolve external definitions referenced by <see cref="WorkflowDefinition"/>s</param>
        /// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
        /// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
        public WorkflowReader(ILogger<WorkflowReader> logger, IWorkflowExternalDefinitionResolver externalDefinitionResolver, IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer)
        {
            this.Logger = logger;
            this.ExternalDefinitionResolver = externalDefinitionResolver;
            this.JsonSerializer = jsonSerializer;
            this.YamlSerializer = yamlSerializer;
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
        /// Gets the service used to resolve external definitions referenced by <see cref="WorkflowDefinition"/>s
        /// </summary>
        protected IWorkflowExternalDefinitionResolver ExternalDefinitionResolver { get; }

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
            if(options.LoadExternalDefinitions)
                workflowDefinition = await this.ExternalDefinitionResolver.LoadExternalDefinitionsAsync(workflowDefinition, options, cancellationToken);
            return workflowDefinition;
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
