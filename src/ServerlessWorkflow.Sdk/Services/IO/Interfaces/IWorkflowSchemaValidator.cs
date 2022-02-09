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
using Newtonsoft.Json.Schema;
using ServerlessWorkflow.Sdk.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.IO
{

    /// <summary>
    /// Defines the fundamentals of a service used to validate <see cref="WorkflowDefinition"/>s
    /// </summary>
    public interface IWorkflowSchemaValidator
    {

        /// <summary>
        /// Validates the specified <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="workflow">The input to validate</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>An <see cref="IList{T}"/> containing the <see cref="ValidationError"/>s that have occured</returns>
        Task<IList<ValidationError>> ValidateAsync(WorkflowDefinition workflow, CancellationToken cancellationToken = default);

        /// <summary>
        /// Validates the specified JSON input
        /// </summary>
        /// <param name="json">The input to validate</param>
        /// <param name="specVersion">The Serverless Workflow spec version to evaluate the <see cref="WorkflowDefinition"/> against</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>An <see cref="IList{T}"/> containing the <see cref="ValidationError"/>s that have occured</returns>
        Task<IList<ValidationError>> ValidateAsync(string json, string specVersion, CancellationToken cancellationToken = default);

    }

}
