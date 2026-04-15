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

#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IWorkflowExecutionContext"/>s
/// </summary>
public static class IWorkflowExecutionContextExtensions
{

    /// <summary>
    /// Gets a new <see cref="WorkflowDescriptor"/> used to describe the <see cref="IWorkflowExecutionContext"/>
    /// </summary>
    /// <param name="workflow">The <see cref="IWorkflowExecutionContext"/> to describe</param>
    /// <returns>A new <see cref="WorkflowDescriptor"/></returns>
    public static WorkflowDescriptor GetDescriptor(this IWorkflowExecutionContext workflow)
    {
        return new()
        {
            Id = workflow.Instance.Id,
            Definition = workflow.Definition,
            Input = workflow.Instance.Input,
            StartedAt = workflow.Instance.StartedAt?.GetDescriptor()
        };
    }

}
