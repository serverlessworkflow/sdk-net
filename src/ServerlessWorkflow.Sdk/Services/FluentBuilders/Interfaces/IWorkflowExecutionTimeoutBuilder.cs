﻿// Copyright © 2023-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="WorkflowExecutionTimeoutDefinition"/>s
/// </summary>
public interface IWorkflowExecutionTimeoutBuilder
{

    /// <summary>
    /// Configures the workflow definition's execution to time out after the specified duration
    /// </summary>
    /// <param name="duration">The duration after which  to time out the workflow definition's execution</param>
    /// <returns>The configured <see cref="IWorkflowExecutionTimeoutBuilder"/></returns>
    IWorkflowExecutionTimeoutBuilder After(TimeSpan duration);

    /// <summary>
    /// Configures the workflow definition to interrupt its execution on timeout 
    /// </summary>
    /// <param name="interrupts">A boolean indicating whether or not interrupt the workflow definition's execution</param>
    /// <returns>The configured <see cref="IWorkflowExecutionTimeoutBuilder"/></returns>
    IWorkflowExecutionTimeoutBuilder InterruptExecution(bool interrupts = true);

    /// <summary>
    /// Configures the workflow definition to run the specified state definition before terminating its execution
    /// </summary>
    /// <param name="state">The reference name of the state definition to run before termination</param>
    /// <returns>The configured <see cref="IWorkflowExecutionTimeoutBuilder"/></returns>
    IWorkflowExecutionTimeoutBuilder Run(string state);

    /// <summary>
    /// Configures the workflow definition to run the specified state definition before terminating its execution
    /// </summary>
    /// <param name="stateSetup">The <see cref="Func{T, TResult}"/> used to build the state definition to run before termination</param>
    /// <returns>The configured <see cref="IWorkflowExecutionTimeoutBuilder"/></returns>
    IWorkflowExecutionTimeoutBuilder Run(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

    /// <summary>
    /// Configures the workflow definition to run the specified state definition before terminating its execution
    /// </summary>
    /// <param name="state">The state definition to run before termination</param>
    /// <returns>The configured <see cref="IWorkflowExecutionTimeoutBuilder"/></returns>
    IWorkflowExecutionTimeoutBuilder Run(StateDefinition state);

    /// <summary>
    /// Builds the <see cref="WorkflowExecutionTimeoutDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="WorkflowExecutionTimeoutDefinition"/></returns>
    WorkflowExecutionTimeoutDefinition Build();

}
