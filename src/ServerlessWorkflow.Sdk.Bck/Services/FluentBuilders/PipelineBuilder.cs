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
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IPipelineBuilder"/> interface
    /// </summary>
    public class PipelineBuilder
        : IPipelineBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="PipelineBuilder"/>
        /// </summary>
        /// <param name="workflow">The <see cref="IWorkflowBuilder"/> the <see cref="IPipelineBuilder"/> belongs to</param>
        public PipelineBuilder(IWorkflowBuilder workflow)
        {
            this.Workflow = workflow;
        }

        /// <summary>
        /// Gets the <see cref="IWorkflowBuilder"/> the <see cref="IPipelineBuilder"/> belongs to
        /// </summary>
        protected IWorkflowBuilder Workflow { get; }

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the state definitions the pipeline is made out of
        /// </summary>
        protected List<StateDefinition> States { get; } = new List<StateDefinition>();

        /// <summary>
        /// Gets the current state definition in the main pipeline of the <see cref="WorkflowDefinition"/>
        /// </summary>
        protected StateDefinition CurrentState { get; private set; } = null!;

        /// <inheritdoc/>
        public virtual EventDefinition AddEvent(Action<IEventBuilder> eventSetup)
        {
            if (eventSetup == null)
                throw new ArgumentNullException(nameof(eventSetup));
            IEventBuilder builder = new EventBuilder();
            eventSetup(builder);
            return this.AddEvent(builder.Build());
        }

        /// <inheritdoc/>
        public virtual EventDefinition AddEvent(EventDefinition e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            this.Workflow.AddEvent(e);
            return e;
        }

        /// <inheritdoc/>
        public virtual FunctionDefinition AddFunction(Action<IFunctionBuilder> functionSetup)
        {
            if (functionSetup == null)
                throw new ArgumentNullException(nameof(functionSetup));
            IFunctionBuilder builder = new FunctionBuilder(this.Workflow);
            functionSetup(builder);
            return this.AddFunction(builder.Build());
        }

        /// <inheritdoc/>
        public virtual FunctionDefinition AddFunction(FunctionDefinition function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));
            this.Workflow.AddFunction(function);
            return function;
        }

        /// <inheritdoc/>
        public virtual StateDefinition AddState(StateDefinition state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            this.States.Add(state);
            return state;
        }

        /// <inheritdoc/>
        public virtual StateDefinition AddState(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            IStateBuilder builder = stateSetup(new StateBuilderFactory(this));
            StateDefinition state = this.AddState(builder.Build());
            if (this.CurrentState == null)
                this.CurrentState = state;
            return state;
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder Then(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            var nextState = this.AddState(stateSetup);
            this.CurrentState.TransitionToStateName = nextState.Name;
            this.CurrentState = nextState;
            return this;
        }

        /// <inheritdoc/>
        public virtual IPipelineBuilder Then(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            return this.Then(flow => stateSetup(flow).WithName(name));
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder EndsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            StateDefinition state = this.AddState(stateSetup);
            state.End = new EndDefinition();
            return this.Workflow;
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder EndsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.EndsWith(flow => stateSetup(flow).WithName(name));
        }

        /// <inheritdoc/>
        public virtual IWorkflowBuilder End()
        {
            this.CurrentState.IsEnd = true;
            return this.Workflow;
        }

        /// <inheritdoc/>
        public virtual IEnumerable<StateDefinition> Build()
        {
            return this.States;
        }

    }

}
