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
    /// Defines the fundamentals of a service used to configure a <see cref="StateDefinition"/>
    /// </summary>
    public interface IStateBuilder
    {

        /// <summary>
        /// Sets the name of the <see cref="StateDefinition"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to build</param>
        /// <returns>The configured <see cref="IStateBuilder"/></returns>
        IStateBuilder WithName(string name);

        /// <summary>
        /// Builds the <see cref="StateDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="StateDefinition"/></returns>
        StateDefinition Build();

    }

    /// <summary>
    /// Defines the fundamentals of a service used to configure a <see cref="StateDefinition"/>
    /// </summary>
    /// <typeparam name="TState">The type of <see cref="StateDefinition"/> to build</typeparam>
    public interface IStateBuilder<TState>
        : IStateBuilder, IMetadataContainerBuilder<IStateBuilder<TState>>
        where TState : StateDefinition, new()
    {

        /// <summary>
        /// Sets the name of the <see cref="StateDefinition"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to build</param>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        new IStateBuilder<TState> WithName(string name);

        /// <summary>
        /// Filters the <see cref="StateDefinition"/>'s input
        /// </summary>
        /// <param name="expression">The workflow expression used to filter the <see cref="StateDefinition"/>'s input</param>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        IStateBuilder<TState> FilterInput(string expression);

        /// <summary>
        /// Filters the <see cref="StateDefinition"/>'s output
        /// </summary>
        /// <param name="expression">The workflow expression used to filter the <see cref="StateDefinition"/>'s output</param>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        IStateBuilder<TState> FilterOutput(string expression);

        /// <summary>
        /// Configures the handling for the specified error
        /// </summary>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        IStateBuilder<TState> HandleError(Action<IErrorHandlerBuilder> builder);

        /// <summary>
        /// Compensates the <see cref="StateDefinition"/> with the specified <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to use for compensation</param>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        IStateBuilder<TState> CompensateWith(string name);

        /// <summary>
        /// Compensates the <see cref="StateDefinition"/> with the specified <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="stateSetup">A <see cref="Func{T, TResult}"/> used to create the <see cref="StateDefinition"/> to use for compensation</param>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        IStateBuilder<TState> CompensateWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Compensates the <see cref="StateDefinition"/> with the specified <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="state">Tthe <see cref="StateDefinition"/> to use for compensation</param>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        IStateBuilder<TState> CompensateWith(StateDefinition state);

    }

}
