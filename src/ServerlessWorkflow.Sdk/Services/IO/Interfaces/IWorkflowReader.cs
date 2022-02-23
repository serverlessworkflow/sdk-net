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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.IO
{

    /// <summary>
    /// Defines the fundamentals of a service used to read <see cref="WorkflowDefinition"/>s
    /// </summary>
    public interface IWorkflowReader
    {

        /// <summary>
        /// Reads a <see cref="WorkflowDefinition"/> from the specified <see cref="Stream"/>
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to read the <see cref="WorkflowDefinition"/> from</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="WorkflowDefinition"/></returns>
        Task<WorkflowDefinition> ReadAsync(Stream stream, CancellationToken cancellationToken = default);

    }

}
