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
    /// Represents the default implementation of the <see cref="IBranchBuilder"/> interface
    /// </summary>
    public class BranchBuilder
        : IBranchBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="BranchBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="BranchBuilder"/> belongs to</param>
        public BranchBuilder(IPipelineBuilder pipeline)
        {
            this.Pipeline = pipeline;
        }

        /// <summary>
        /// Gets the <see cref="IPipelineBuilder"/> the <see cref="BranchBuilder"/> belongs to
        /// </summary>
        protected IPipelineBuilder Pipeline { get; set; }

        /// <summary>
        /// Gets the <see cref="BranchDefinition"/> to configure
        /// </summary>
        protected BranchDefinition Branch { get; } = new BranchDefinition();

        /// <inheritdoc/>
        public virtual IBranchBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Branch.Name = name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IBranchBuilder Execute(ActionDefinition action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            this.Branch.Actions.Add(action);
            return this;
        }

        /// <inheritdoc/>
        public virtual IBranchBuilder Execute(Action<IActionBuilder> actionSetup)
        {
            if (actionSetup == null)
                throw new ArgumentNullException(nameof(actionSetup));
            IActionBuilder actionBuilder = new ActionBuilder(this.Pipeline);
            actionSetup(actionBuilder);
            this.Branch.Actions.Add(actionBuilder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IBranchBuilder Concurrently()
        {
            this.Branch.ActionMode = ActionExecutionMode.Parallel;
            return this;
        }

        /// <inheritdoc/>
        public virtual IBranchBuilder Sequentially()
        {
            this.Branch.ActionMode = ActionExecutionMode.Sequential;
            return this;
        }

        /// <inheritdoc/>
        public virtual void RunSubflow(string workflowId)
        {
            if (string.IsNullOrWhiteSpace(workflowId))
                throw new ArgumentNullException(nameof(workflowId));
            this.Branch.WorkflowId = workflowId;
        }

        /// <inheritdoc/>
        public virtual BranchDefinition Build()
        {
            return this.Branch;
        }

    }

}
