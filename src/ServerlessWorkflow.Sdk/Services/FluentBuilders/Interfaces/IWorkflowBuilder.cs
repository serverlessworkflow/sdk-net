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
    /// Defines the fundamentals of a service used to build <see cref="WorkflowDefinition"/>s
    /// </summary>
    public interface IWorkflowBuilder
        : IMetadataContainerBuilder<IWorkflowBuilder>
    {

        /// <summary>
        /// Sets the id of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="id">The id of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithId(string id);

        /// <summary>
        /// Sets the name of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="name">The name of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithName(string name);

        /// <summary>
        /// Sets the description of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="description">The description of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithDescription(string description);

        /// <summary>
        /// Sets the version of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="version">The description of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithVersion(string version);

        /// <summary>
        /// Sets and configures the startup <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the startup <see cref="StateDefinition"/></param>
        /// <returns>A new <see cref="IPipelineBuilder"/> used to configure the <see cref="WorkflowDefinition"/>'s <see cref="StateDefinition"/>s</returns>
        IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Adds the specified <see cref="EventDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="e">The <see cref="EventDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddEvent(EventDefinition e);

        /// <summary>
        /// Adds the specified <see cref="EventDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="eventSetup">The <see cref="Action{T}"/> used to setup the <see cref="EventDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddEvent(Action<IEventBuilder> eventSetup);

        /// <summary>
        /// Adds the specified <see cref="FunctionDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="functionSetup">The <see cref="Action{T}"/> used to setup the <see cref="FunctionDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddFunction(Action<IFunctionBuilder> functionSetup);

        /// <summary>
        /// Adds the specified <see cref="FunctionDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="function">The <see cref="FunctionDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddFunction(FunctionDefinition function);

        /// <summary>
        /// Adds the specified <see cref="RetryStrategyDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="strategy">The <see cref="Action{T}"/> used to setup the <see cref="RetryStrategyDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddRetryStrategy(RetryStrategyDefinition strategy);

        /// <summary>
        /// Adds the specified <see cref="RetryStrategyDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="retryStrategySetup">The <see cref="Action{T}"/> used to setup the <see cref="RetryStrategyDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddRetryStrategy(Action<IRetryStrategyBuilder> retryStrategySetup);

        /// <summary>
        /// Builds the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="WorkflowDefinition"/></returns>
        WorkflowDefinition Build();

    }

}
