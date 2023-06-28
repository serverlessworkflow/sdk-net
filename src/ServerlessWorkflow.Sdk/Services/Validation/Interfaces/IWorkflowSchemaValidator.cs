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

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Defines the fundamentals of a service used to validate <see cref="WorkflowDefinition"/>s against the adequate version of the <see href="https://serverlessworkflow.io/schemas/latest/workflow.json">Serverless Workflow Specification schema</see>
/// </summary>
public interface IWorkflowSchemaValidator
{

    /// <summary>
    /// Validates the specified <see cref="WorkflowDefinition"/> against the adequate version of the <see href="https://serverlessworkflow.io/schemas/latest/workflow.json">Serverless Workflow Specification schema</see> 
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="EvaluationResults"/> that describes the result of the validation</returns>
    Task<EvaluationResults> ValidateAsync(WorkflowDefinition workflow, CancellationToken cancellationToken = default);

}
