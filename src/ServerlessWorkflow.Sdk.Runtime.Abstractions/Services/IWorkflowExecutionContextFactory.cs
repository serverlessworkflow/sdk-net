// Copyright © 2024-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="IWorkflowExecutionContext"/>s
/// </summary>
public interface IWorkflowExecutionContextFactory
{

    /// <summary>
    /// Creates a new <see cref="IWorkflowExecutionContext"/>
    /// </summary>
    /// <param name="definition">The <see cref="WorkflowDefinition"/> to create the context for</param>
    /// <param name="instance">The <see cref="IWorkflowInstance"/> to create the context for</param>
    /// <param name="executionsOptions">The options used to configure the workflow's execution</param>
    /// <returns>A new <see cref="IWorkflowExecutionContext"/></returns>
    IWorkflowExecutionContext Create(WorkflowDefinition definition, IWorkflowInstance instance, WorkflowExecutionsOptions executionsOptions);

}