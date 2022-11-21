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

namespace ServerlessWorkflow.Sdk.Services.IO
{

    /// <summary>
    /// Defines the fundamentals of a service used to write <see cref="WorkflowDefinition"/>s
    /// </summary>
    public interface IWorkflowWriter
    {

        /// <summary>
        /// Writes the specified <see cref="WorkflowDefinition"/> to a <see cref="Stream"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to write</param>
        /// <param name="stream">The <see cref="Stream"/> to read the <see cref="WorkflowDefinition"/> from</param>
        /// <param name="format">The format of the <see cref="WorkflowDefinition"/> to read. Defaults to '<see cref="WorkflowDefinitionFormat.Yaml"/>'</param>
        /// <returns>A new <see cref="WorkflowDefinition"/></returns>
        void Write(WorkflowDefinition workflow, Stream stream, string format = WorkflowDefinitionFormat.Yaml);

    }

}
