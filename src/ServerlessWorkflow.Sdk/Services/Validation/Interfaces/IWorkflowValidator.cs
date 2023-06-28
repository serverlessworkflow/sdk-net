// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Defines the fundamentals of a service used to validate <see cref="WorkflowDefinition"/>s
/// </summary>
public interface IWorkflowValidator
{

    /// <summary>
    /// Validates the specified <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflowDefinition">The <see cref="WorkflowDefinition"/> to validate</param>
    /// <param name="validateSchema">A boolean indicating whether or not to validate the schema of the specified <see cref="WorkflowDefinition"/></param>
    /// <param name="validateDsl">A boolean indicating whether or not to validate the DSL of the specified <see cref="WorkflowDefinition"/></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IWorkflowValidationResult"/></returns>
    Task<IWorkflowValidationResult> ValidateAsync(WorkflowDefinition workflowDefinition, bool validateSchema = true, bool validateDsl = true, CancellationToken cancellationToken = default);

}
