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
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IActionBuilder"/> interface
    /// </summary>
    public class ActionBuilder
        : IActionBuilder, IEventTriggerActionBuilder, IFunctionActionBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ActionBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="ActionBuilder"/> belongs to</param>
        public ActionBuilder(IPipelineBuilder pipeline)
        {
            this.Pipeline = pipeline;
        }

        /// <summary>
        /// Gets the <see cref="IPipelineBuilder"/> the <see cref="ActionBuilder"/> belongs to
        /// </summary>
        protected IPipelineBuilder Pipeline { get; set; }

        /// <summary>
        /// Gets the <see cref="ActionDefinition"/> to configure
        /// </summary>
        protected ActionDefinition Action { get; } = new ActionDefinition();

        /// <inheritdoc/>
        public virtual IActionBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Action.Name = name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IActionBuilder FromStateData(string expression)
        {
            this.Action.DataFilter.FromStateData = expression;
            return this;
        }

        /// <inheritdoc/>
        public virtual IActionBuilder FilterResults(string expression)
        {
            this.Action.DataFilter.Results = expression;
            return this;
        }

        /// <inheritdoc/>
        public virtual IActionBuilder ToStateData(string expression)
        {
            this.Action.DataFilter.ToStateData = expression;
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventTriggerActionBuilder Consume(string e)
        {
            if (string.IsNullOrWhiteSpace(e))
                throw new ArgumentNullException(nameof(e));
            this.Action.Event = new EventReference() { TriggerEvent = e, ResultEvent = string.Empty };
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventTriggerActionBuilder Consume(Action<IEventBuilder> eventSetup)
        {
            if (eventSetup == null)
                throw new ArgumentNullException(nameof(eventSetup));
            EventDefinition e = this.Pipeline.AddEvent(eventSetup);
            this.Action.Event = new EventReference() { TriggerEvent = e.Name, ResultEvent = string.Empty };
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventTriggerActionBuilder Consume(EventDefinition e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            this.Pipeline.AddEvent(e);
            this.Action.Event = new EventReference() { TriggerEvent = e.Name, ResultEvent = string.Empty };
            return this;
        }

        /// <inheritdoc/>
        public virtual IFunctionActionBuilder Invoke(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException(nameof(function));
            this.Action.Function = new FunctionReference() { Name = function };
            return this;
        }

        /// <inheritdoc/>
        public virtual IFunctionActionBuilder Invoke(Action<IFunctionBuilder> functionSetup)
        {
            if (functionSetup == null)
                throw new ArgumentNullException(nameof(functionSetup));
            FunctionDefinition function = this.Pipeline.AddFunction(functionSetup);
            this.Action.Function = new FunctionReference() { Name = function.Name };
            return this;
        }

        /// <inheritdoc/>
        public virtual IFunctionActionBuilder Invoke(FunctionDefinition function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));
            this.Pipeline.AddFunction(function);
            this.Action.Function = new FunctionReference() { Name = function.Name };
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventTriggerActionBuilder ThenProduce(string e)
        {
            this.Action.Event.ResultEvent = e;
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventTriggerActionBuilder WithContextAttribute(string name, string value)
        {
            this.Action.Event.ContextAttributes.Add(name, value);
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventTriggerActionBuilder WithContextAttribute(IDictionary<string, string> contextAttributes)
        {
            this.Action.Event.ContextAttributes = JObject.FromObject(contextAttributes);
            return this;
        }

        /// <inheritdoc/>
        public virtual IFunctionActionBuilder WithArgument(string name, string value)
        {
            this.Action.Function.Arguments.Add(name, value);
            return this;
        }

        /// <inheritdoc/>
        public virtual IFunctionActionBuilder WithArguments(IDictionary<string, string> args)
        {
            this.Action.Function.Arguments = JObject.FromObject(args);
            return this;
        }

        /// <inheritdoc/>
        public virtual ActionDefinition Build()
        {
            return this.Action;
        }

    }

}
