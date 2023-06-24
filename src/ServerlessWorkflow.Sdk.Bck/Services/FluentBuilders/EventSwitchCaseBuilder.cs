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
    /// Represents the default implementation of the <see cref="IEventSwitchCaseBuilder"/> interface
    /// </summary>
    public class EventSwitchCaseBuilder
        : SwitchCaseBuilder<IEventSwitchCaseBuilder, EventCaseDefinition>, IEventSwitchCaseBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="EventSwitchCaseBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="EventSwitchCaseBuilder"/> belongs to</param>
        public EventSwitchCaseBuilder(IPipelineBuilder pipeline)
            : base(pipeline)
        {

        }

        /// <inheritdoc/>
        public virtual IStateOutcomeBuilder On(string e)
        {
            if(string.IsNullOrWhiteSpace(e))
                throw new ArgumentNullException(nameof(e));
            this.Case.Event = e;
            return this;
        }

        /// <inheritdoc/>
        public virtual IStateOutcomeBuilder On(Action<IEventBuilder> eventSetup)
        {
            if (eventSetup == null)
                throw new ArgumentNullException(nameof(eventSetup));
            EventDefinition e = this.Pipeline.AddEvent(eventSetup);
            this.Case.Event = e.Name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IStateOutcomeBuilder On(EventDefinition e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            this.Pipeline.AddEvent(e);
            this.Case.Event = e.Name;
            return this;
        }

        /// <inheritdoc/>
        public virtual new EventCaseDefinition Build()
        {
            StateOutcomeDefinition outcome = base.Build();
            switch (outcome)
            {
                case EndDefinition end:
                    this.Case.End = end;
                    break;
                case TransitionDefinition transition:
                    this.Case.Transition = transition;
                    break;
                default:
                    throw new NotSupportedException($"The specified outcome type '{outcome.GetType().Name}' is not supported");
            }
            return this.Case;
        }

    }
}
