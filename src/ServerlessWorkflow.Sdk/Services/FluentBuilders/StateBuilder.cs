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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IStateBuilder{TState}"/> interface
    /// </summary>
    /// <typeparam name="TState">The type of <see cref="StateDefinition"/> to build</typeparam>
    public abstract class StateBuilder<TState>
        : MetadataContainerBuilder<IStateBuilder<TState>>, IStateBuilder<TState>
        where TState : StateDefinition, new()
    {

        /// <summary>
        /// Initializes a new <see cref="StateBuilder{TState}"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
        protected StateBuilder(IPipelineBuilder pipeline)
        {
            this.Pipeline = pipeline;
        }

        /// <summary>
        /// Gets the <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to
        /// </summary>
        protected IPipelineBuilder Pipeline { get; }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/> to configure
        /// </summary>
        protected TState State { get; } = new TState();

        /// <inheritdoc/>
        public override Any Metadata
        {
            get
            {
                return this.State.Metadata;
            }
        }

        /// <inheritdoc/>
        public virtual IStateBuilder<TState> WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.State.Name = name;
            return this;
        }

        IStateBuilder IStateBuilder.WithName(string name)
        {
            return this.WithName(name);
        }

        /// <inheritdoc/>
        public virtual IStateBuilder<TState> FilterInput(string expression)
        {
            this.State.DataFilter.Input = expression;
            return this;
        }

        /// <inheritdoc/>
        public virtual IStateBuilder<TState> FilterOutput(string expression)
        {
            this.State.DataFilter.Output = expression;
            return this;
        }

        /// <inheritdoc/>
        public virtual IStateBuilder<TState> CompensateWith(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.State.CompensatedBy = name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IStateBuilder<TState> CompensateWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
        {
            if (stateSetup == null)
                throw new ArgumentNullException(nameof(stateSetup));
            StateDefinition compensatedBy = this.Pipeline.AddState(stateSetup);
            compensatedBy.UsedForCompensation = true;
            this.State.CompensatedBy = compensatedBy.Name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IStateBuilder<TState> CompensateWith(StateDefinition state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            state.UsedForCompensation = true;
            this.State.CompensatedBy = this.Pipeline.AddState(state).Name;
            return this;
        }

        /// <inheritdoc/>
        public virtual IStateBuilder<TState> HandleError(Action<IErrorHandlerBuilder> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));
            IErrorHandlerBuilder builder = new ErrorHandlerBuilder(this.Pipeline);
            setupAction(builder);
            ErrorHandlerDefinition errorHandler = builder.Build();
            this.State.Errors.Add(errorHandler);
            return this;
        }

        /// <inheritdoc/>
        public virtual StateDefinition Build()
        {
            return this.State;
        }

    }

}
