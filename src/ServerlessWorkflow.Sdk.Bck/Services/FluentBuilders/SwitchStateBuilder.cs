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
    /// Represents the default implementation of the <see cref="IDataSwitchStateBuilder"/> interface
    /// </summary>
    public class SwitchStateBuilder
        : StateBuilder<SwitchStateDefinition>, IDataSwitchStateBuilder, IEventSwitchStateBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="SwitchStateBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
        public SwitchStateBuilder(IPipelineBuilder pipeline)
            : base(pipeline)
        {

        }

        /// <inheritdoc/>
        public virtual IDataSwitchStateBuilder Data()
        {
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventSwitchStateBuilder Events()
        {
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventSwitchStateBuilder Timeout(TimeSpan duration)
        {
            this.State.EventTimeout = duration;
            return this;
        }

        /// <inheritdoc/>
        public virtual IDataSwitchStateBuilder Case(Action<IDataSwitchCaseBuilder> caseSetup)
        {
            if (caseSetup == null)
                throw new ArgumentException(nameof(caseSetup));
            IDataSwitchCaseBuilder builder = new DataSwitchCaseBuilder(this.Pipeline);
            caseSetup(builder);
            this.State.DataConditions = new();
            this.State.DataConditions.Add(builder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IEventSwitchStateBuilder Case(Action<IEventSwitchCaseBuilder> caseSetup)
        {
            if (caseSetup == null)
                throw new ArgumentException(nameof(caseSetup));
            IEventSwitchCaseBuilder builder = new EventSwitchCaseBuilder(this.Pipeline);
            caseSetup(builder);
            this.State.EventConditions.Add(builder.Build());
            return this;
        }

    }
}
