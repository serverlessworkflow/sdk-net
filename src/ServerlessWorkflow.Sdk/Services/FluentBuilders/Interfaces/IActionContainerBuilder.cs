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
    /// Defines the fundamentals of a service that defines an <see cref="ActionDefinition"/>
    /// </summary>
    /// <typeparam name="TContainer">The container's type</typeparam>
    public interface IActionContainerBuilder<TContainer>
          where TContainer : class, IActionContainerBuilder<TContainer>
    {

        /// <summary>
        /// Creates and configures a new <see cref="ActionDefinition"/> to be executed by the container
        /// </summary>
        /// <param name="action">The <see cref="ActionDefinition"/> to execute</param>
        /// <returns>The configured container</returns>
        TContainer Execute(ActionDefinition action);

        /// <summary>
        /// Creates and configures a new <see cref="ActionDefinition"/> to be executed by the container
        /// </summary>
        /// <param name="actionSetup">An <see cref="Action{T}"/> used to setup the <see cref="ActionDefinition"/> to execute</param>
        /// <returns>The configured container</returns>
        TContainer Execute(Action<IActionBuilder> actionSetup);

        /// <summary>
        /// Creates and configures a new <see cref="ActionDefinition"/> to be executed by the container
        /// </summary>
        /// <param name="name">The name of the <see cref="ActionDefinition"/> to execute</param>
        /// <param name="actionSetup">An <see cref="Action{T}"/> used to setup the <see cref="ActionDefinition"/> to execute</param>
        /// <returns>The configured container</returns>
        TContainer Execute(string name, Action<IActionBuilder> actionSetup);

    }

}
