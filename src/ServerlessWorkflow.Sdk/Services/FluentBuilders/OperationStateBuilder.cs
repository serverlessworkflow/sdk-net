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
    /// Represents the default implementation of the <see cref="IOperationStateBuilder"/> interface
    /// </summary>
    public class OperationStateBuilder
        : StateBuilder<OperationStateDefinition>, IOperationStateBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="OperationStateBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
        public OperationStateBuilder(IPipelineBuilder pipeline) 
            : base(pipeline)
        {
        }

        /// <inheritdoc/>
        public virtual IOperationStateBuilder Execute(ActionDefinition action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            this.State.Actions.Add(action);
            return this;
        }

        /// <inheritdoc/>
        public virtual IOperationStateBuilder Execute(Action<IActionBuilder> actionSetup)
        {
            if (actionSetup == null)
                throw new ArgumentNullException(nameof(actionSetup));
            IActionBuilder actionBuilder = new ActionBuilder(this.Pipeline);
            actionSetup(actionBuilder);
            this.State.Actions.Add(actionBuilder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IOperationStateBuilder Execute(string name, Action<IActionBuilder> actionSetup)
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
        public virtual IOperationStateBuilder Concurrently()
        {
            this.State.ActionMode = ActionExecutionMode.Parallel;
            return this;
        }

        /// <inheritdoc/>
        public virtual IOperationStateBuilder Sequentially()
        {
            this.State.ActionMode = ActionExecutionMode.Sequential;
            return this;
        }

    }

}
