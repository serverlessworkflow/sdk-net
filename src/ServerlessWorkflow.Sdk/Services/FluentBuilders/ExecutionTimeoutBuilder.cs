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
    /// Represents the default implementation of the <see cref="ExecutionTimeoutBuilder"/> interface
    /// </summary>
    public class ExecutionTimeoutBuilder
        : IExecutionTimeoutBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ExecutionTimeoutBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="ExecutionTimeoutBuilder"/> belongs to</param>
        public ExecutionTimeoutBuilder(IPipelineBuilder pipeline)
        {
            this.Pipeline = pipeline;
        }

        /// <summary>
        /// Gets the <see cref="IPipelineBuilder"/> the <see cref="ExecutionTimeoutBuilder"/> belongs to
        /// </summary>
        protected IPipelineBuilder Pipeline { get; }

        /// <summary>
        /// Gets the <see cref="ExecutionTimeoutDefinition"/> to configure
        /// </summary>
        protected ExecutionTimeoutDefinition Timeout { get; } = new ExecutionTimeoutDefinition();

        /// <inheritdoc/>
        public virtual IExecutionTimeoutBuilder After(TimeSpan duration)
        {
            this.Timeout.Duration = duration;
            return this;
        }

        /// <inheritdoc/>
        public virtual IExecutionTimeoutBuilder InterruptExecution(bool interrupts = true)
        {
            this.Timeout.Interrupt = interrupts;
            return this;
        }

        /// <inheritdoc/>
        public virtual IExecutionTimeoutBuilder Run(string state)
        {
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentNullException(nameof(state));
            this.Timeout.RunBefore = state;
            return this;
        }

        /// <inheritdoc/>
        public virtual IExecutionTimeoutBuilder Run(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if(stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            return this.Run(this.Pipeline.AddState(stateSetup).Name);
        }

        /// <inheritdoc/>
        public virtual IExecutionTimeoutBuilder Run(StateDefinition state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            return this.Run(this.Pipeline.AddState(state).Name);
        }

        /// <inheritdoc/>
        public virtual ExecutionTimeoutDefinition Build()
        {
            return this.Timeout;
        }

    }

}
