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
    /// Defines the fundamentals of a service used to build <see cref="ParallelStateDefinition"/>s
    /// </summary>
    public interface IParallelStateBuilder
        : IStateBuilder<ParallelStateDefinition>
    {

        /// <summary>
        /// Creates and configures a new <see cref="BranchDefinition"/>
        /// </summary>
        /// <param name="branchSetup">The <see cref="Action{T}"/> used to setup the <see cref="BranchDefinition"/></param>
        /// <returns>The configured <see cref="IParallelStateBuilder"/></returns>
        IParallelStateBuilder Branch(Action<IBranchBuilder> branchSetup);

        /// <summary>
        /// Configures the <see cref="ParallelStateDefinition"/> to wait for all branches to complete before resuming the workflow's execution
        /// </summary>
        /// <returns>The configured <see cref="IParallelStateBuilder"/></returns>
        IParallelStateBuilder WaitForAll();

        /// <summary>
        /// Configures the <see cref="ParallelStateDefinition"/> to wait for any branch to complete before resuming the workflow's execution
        /// </summary>
        /// <returns>The configured <see cref="IParallelStateBuilder"/></returns>
        IParallelStateBuilder WaitForAny();

        /// <summary>
        /// Configures the <see cref="ParallelStateDefinition"/> to wait for the specified amount of branches to complete before resuming the workflow's execution
        /// </summary>
        /// <param name="amount">The amount of branches to wait for the execution of</param>
        /// <returns>The configured <see cref="IParallelStateBuilder"/></returns>
        IParallelStateBuilder WaitFor(uint amount);

    }

}
