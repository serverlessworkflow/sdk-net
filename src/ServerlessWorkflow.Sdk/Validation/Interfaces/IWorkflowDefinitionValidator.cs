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

using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Defines the fundamentals of a service used to validate <see cref="WorkflowDefinition"/>s
/// </summary>
public interface IWorkflowDefinitionValidator
{

    /// <summary>
    /// Validates the specified <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflowDefinition">The <see cref="WorkflowDefinition"/> to validate</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An object that describe the result of the validation attempt</returns>
    Task<IValidationResult> ValidateAsync(WorkflowDefinition workflowDefinition, CancellationToken cancellationToken = default);

}