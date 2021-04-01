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
    /// Represents the default implementation of the <see cref="IErrorHandlerBuilder"/> interface
    /// </summary>
    public class ErrorHandlerBuilder
        : IErrorHandlerBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ErrorHandlerBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="ErrorHandlerBuilder"/> belongs to</param>
        public ErrorHandlerBuilder(IPipelineBuilder pipeline)
        {
            this.Pipeline = pipeline;
            this.Outcome = new StateOutcomeBuilder(this.Pipeline);
        }

        /// <summary>
        /// Gets the <see cref="IPipelineBuilder"/> the <see cref="ErrorHandlerBuilder"/> belongs to
        /// </summary>
        protected IPipelineBuilder Pipeline { get; }

        /// <summary>
        /// Gets the <see cref="ErrorHandlerDefinition"/> to configure
        /// </summary>
        protected ErrorHandlerDefinition ErrorHandler { get; } = new ErrorHandlerDefinition();

        /// <summary>
        /// Gets the service used to build the <see cref="ErrorHandlerDefinition"/>'s outcome
        /// </summary>
        protected IStateOutcomeBuilder Outcome { get; }

        /// <inheritdoc/>
        public virtual IStateOutcomeBuilder When(string error, string errorCode)
        {
            this.ErrorHandler.Error = error;
            this.ErrorHandler.Code = errorCode;
            return this.Outcome;
        }

        /// <inheritdoc/>
        public virtual IStateOutcomeBuilder When(string error)
        {
            this.ErrorHandler.Error = error;
            return this.Outcome;
        }

        /// <inheritdoc/>
        public virtual IStateOutcomeBuilder WhenAny()
        {
            return this.When("*");
        }

        /// <inheritdoc/>
        public virtual IErrorHandlerBuilder UseRetryStrategy(string policy)
        {
            this.ErrorHandler.Retry = policy;
            return this;
        }

        /// <inheritdoc/>
        public virtual ErrorHandlerDefinition Build()
        {
            StateOutcomeDefinition outcome = this.Outcome.Build();
            switch (outcome)
            {
                case TransitionDefinition transition:
                    this.ErrorHandler.Transition = transition;
                    break;
                case EndDefinition end:
                    this.ErrorHandler.End = end;
                    break;
                default:
                    throw new NotSupportedException($"the specified {nameof(StateOutcomeDefinition)} type '{outcome.GetType().Name}' is not supported");
            }
            return this.ErrorHandler;
        }

    }

}
