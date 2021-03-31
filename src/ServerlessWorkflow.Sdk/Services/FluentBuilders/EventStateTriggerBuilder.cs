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
using ServerlessWorkflow.Sdk.Models;
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IEventStateTriggerBuilder"/> interface
    /// </summary>
    public class EventStateTriggerBuilder
        : IEventStateTriggerBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="EventStateTriggerBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="EventStateTriggerBuilder"/> belongs to</param>
        public EventStateTriggerBuilder(IPipelineBuilder pipeline)
        {
            this.Pipeline = pipeline;
        }

        /// <summary>
        /// Gets the <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to
        /// </summary>
        protected IPipelineBuilder Pipeline { get; }

        /// <summary>
        /// Gets the <see cref="EventStateTriggerDefinition"/> to configure
        /// </summary>
        protected EventStateTriggerDefinition Trigger { get; } = new EventStateTriggerDefinition();

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder On(params string[] events)
        {
            if (events != null)
            {
                foreach(string e in events)
                {
                    this.Trigger.Events.Add(e);
                }
            }
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder On(params Action<IEventBuilder>[] eventSetups)
        {
            if (eventSetups != null)
            {
                foreach (Action<IEventBuilder> eventSetup in eventSetups)
                {
                    this.Trigger.Events.Add(this.Pipeline.AddEvent(eventSetup).Name);
                }
            }
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder On(params EventDefinition[] events)
        {
            if (events != null)
            {
                foreach (EventDefinition e in events)
                {
                    this.Trigger.Events.Add(this.Pipeline.AddEvent(e).Name);
                }
            }
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder Execute(ActionDefinition action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            this.Trigger.Actions.Add(action);
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder Execute(Action<IActionBuilder> actionSetup)
        {
            if (actionSetup == null)
                throw new ArgumentNullException(nameof(actionSetup));
            IActionBuilder builder = new ActionBuilder(this.Pipeline);
            actionSetup(builder);
            return this.Execute(builder.Build());
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder Execute(string name, Action<IActionBuilder> actionSetup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (actionSetup == null)
                throw new ArgumentNullException(nameof(actionSetup));
            return this.Execute(a =>
            {
                actionSetup(a);
                a.WithName(name);
            });
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder Sequentially()
        {
            this.Trigger.ActionMode = ActionExecutionMode.Sequential;
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder Concurrently()
        {
            this.Trigger.ActionMode = ActionExecutionMode.Parallel;
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder FilterPayload(string expression)
        {
            this.Trigger.DataFilter.Data = expression;
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventStateTriggerBuilder ToStateData(string expression)
        {
            this.Trigger.DataFilter.ToStateData = expression;
            return this;
        }

        /// <inheritdoc/>
        public virtual EventStateTriggerDefinition Build()
        {
            return this.Trigger;
        }

    }

}
