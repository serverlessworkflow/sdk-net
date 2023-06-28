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

namespace ServerlessWorkflow.Sdk.Services.IO;

/// <summary>
/// Defines the fundamentals of a service used to resolve the external definitions referenced by a <see cref="WorkflowDefinition"/>
/// </summary>
public interface IWorkflowExternalDefinitionResolver
{

    /// <summary>
    /// Loads the external definitions referenced by the specified <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to load the external references of</param>
    /// <param name="options">The options used to configure how to read external definitions</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The loaded <see cref="WorkflowDefinition"/></returns>
    Task<WorkflowDefinition> LoadExternalDefinitionsAsync(WorkflowDefinition workflow, WorkflowReaderOptions options, CancellationToken cancellationToken = default);

}
