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
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IWorkflowBuilder"/> interface
    /// </summary>
    public class WorkflowBuilder
        : MetadataContainerBuilder<IWorkflowBuilder>, IWorkflowBuilder, IDisposable
    {
        private bool _Disposed;

        /// <summary>
        /// Initializes a new <see cref="WorkflowBuilder"/>
        /// </summary>
        /// <param name="httpClient">The <see cref="System.Net.Http.HttpClient"/> to use to fetch external resources</param>
        public WorkflowBuilder(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
            this.Pipeline = new PipelineBuilder(this);
        }

        /// <summary>
        /// Initializes a new <see cref="WorkflowBuilder"/>
        /// </summary>
        /// <param name="httpClientFactory">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
        public WorkflowBuilder(IHttpClientFactory httpClientFactory)
            : this(httpClientFactory.CreateClient())
        {

        }

        /// <summary>
        /// Initializes a new <see cref="WorkflowBuilder"/>
        /// </summary>
        public WorkflowBuilder()
            : this(new HttpClient())
        {

        }

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> to use to fetch external resources
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> to configure
        /// </summary>
        protected WorkflowDefinition Workflow { get; } = new WorkflowDefinition();

        /// <summary>
        /// Gets the service used to build the <see cref="WorkflowDefinition"/>'s <see cref="StartDefinition"/> chart
        /// </summary>
        protected IPipelineBuilder Pipeline { get; }

        /// <inheritdoc/>
        public override JObject Metadata
        {
            get
            {
                return this.Workflow.Metadata;
            }
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            this.Workflow.Key = key;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));
            this.Workflow.Id = id;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Workflow.Name = name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithDescription(string description)
        {
            this.Workflow.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version));
            this.Workflow.Version = version;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithSpecVersion(string specVersion)
        {
            if (string.IsNullOrWhiteSpace(specVersion))
                throw new ArgumentNullException(nameof(specVersion));
            this.Workflow.SpecVersion = specVersion;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithDataInputSchema(Uri uri)
        {
            this.Workflow.DataInputSchemaUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithDataInputSchema(JSchema schema)
        {
            this.Workflow.DataInputSchema = schema ?? throw new ArgumentNullException(nameof(schema));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AnnotateWith(string annotation)
        {
            if (string.IsNullOrWhiteSpace(annotation))
                throw new ArgumentNullException(nameof(annotation));
            this.Workflow.Annotations.Add(annotation);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseExpressionLanguage(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentNullException(nameof(language));
            this.Workflow.ExpressionLanguage = language;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseJq()
        {
            return this.UseExpressionLanguage("jq");
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder WithExecutionTimeout(Action<IExecutionTimeoutBuilder> timeoutSetup)
        {
            IExecutionTimeoutBuilder builder = new ExecutionTimeoutBuilder(this.Pipeline);
            timeoutSetup(builder);
            this.Workflow.ExecutionTimeout = builder.Build();
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder KeepActive(bool keepActive = true)
        {
            this.Workflow.KeepActive = keepActive;
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportConstantsFrom(Uri uri)
        {
            this.Workflow.ConstantsUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseConstants(object constants)
        {
            if (constants == null)
                throw new ArgumentNullException(nameof(constants));
            this.Workflow.Constants = JObject.FromObject(constants);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddConstant(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (this.Workflow.Constants == null)
                this.Workflow.Constants = new JObject();
            this.Workflow.Constants.Add(name, JToken.FromObject(value));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder UseSecrets(IEnumerable<string> secrets)
        {
            this.Workflow.Secrets = secrets?.ToList();
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddSecret(string secret)
        {
            if(this.Workflow.Secrets == null)
                this.Workflow.Secrets = new();
            this.Workflow.Secrets.Add(secret);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportEventsFrom(Uri uri)
        {
            this.Workflow.EventsUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddEvent(EventDefinition e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            if (this.Workflow.Events.Any(ed => ed.Name == e.Name))
                throw new ArgumentException($"The workflow already defines an event with the specified name '{e.Name}'", nameof(e));
            this.Workflow.Events.Add(e);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddEvent(Action<IEventBuilder> eventSetup)
        {
            if (eventSetup == null)
                throw new ArgumentNullException(nameof(eventSetup));
            IEventBuilder builder = new EventBuilder();
            eventSetup(builder);
            return this.AddEvent(builder.Build());
        }
        
        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportFunctionsFrom(Uri uri)
        {
            this.Workflow.FunctionsUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddFunction(FunctionDefinition function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));
            if (this.Workflow.Functions.Any(fd => fd.Name == function.Name))
                throw new ArgumentException($"The workflow already defines a function with the specified name '{function.Name}'", nameof(function));
            this.Workflow.Functions.Add(function);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddFunction(Action<IFunctionBuilder> functionSetup)
        {
            if (functionSetup == null)
                throw new ArgumentNullException(nameof(functionSetup));
            IFunctionBuilder builder = new FunctionBuilder();
            functionSetup(builder);
            return this.AddFunction(builder.Build());
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder ImportRetryStrategiesFrom(Uri uri)
        {
            this.Workflow.RetriesUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddRetryStrategy(RetryStrategyDefinition strategy)
        {
            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));
            if (this.Workflow.Retries.Any(rs => rs.Name == strategy.Name))
                throw new ArgumentException($"The workflow already defines a function with the specified name '{strategy.Name}'", nameof(strategy));
            this.Workflow.Retries.Add(strategy);
            return this;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder AddRetryStrategy(Action<IRetryStrategyBuilder> retryStrategySetup)
        {
            if (retryStrategySetup == null)
                throw new ArgumentNullException(nameof(retryStrategySetup));
            IRetryStrategyBuilder builder = new RetryStrategyBuilder();
            retryStrategySetup(builder);
            return this.AddRetryStrategy(builder.Build());
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder StartsWith(StateDefinition state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            this.Pipeline.AddState(state);
            this.Workflow.Start = new StartDefinition() { StateName = state.Name };
            return this.Pipeline;
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            StateDefinition state = this.Pipeline.AddState(stateSetup);
            this.Workflow.Start = new StartDefinition() { StateName = state.Name };
            return this.Pipeline;
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder StartsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            return this.StartsWith(flow => stateSetup(flow).WithName(name));
        }

        /// <inheritdoc/>
        public virtual WorkflowDefinition Build()
        {
            this.Workflow.States = this.Pipeline.Build().ToList();
            return this.Workflow;
        }

        /// <summary>
        /// Disposes of the <see cref="WorkflowBuilder"/>
        /// </summary>
        /// <param name="disposing">A boolean indicating whether or not the <see cref="WorkflowBuilder"/> is being disposed of</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                    this.HttpClient?.Dispose();
                this._Disposed = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
