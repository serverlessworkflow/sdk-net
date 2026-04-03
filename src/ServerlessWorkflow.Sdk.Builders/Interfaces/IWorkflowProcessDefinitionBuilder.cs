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

using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="WorkflowProcessDefinition"/>s
/// </summary>
public interface IWorkflowProcessDefinitionBuilder
    : IProcessDefinitionBuilder<WorkflowProcessDefinition>
{

    /// <summary>
    /// Configures the task to run the workflow with the specified namespace
    /// </summary>
    /// <param name="namespace">The namespace the workflow to run belongs to</param>
    /// <returns>The configured <see cref="IWorkflowProcessDefinitionBuilder"/></returns>
    IWorkflowProcessDefinitionBuilder WithNamespace(string @namespace);

    /// <summary>
    /// Configures the task to run the workflow with the specified name
    /// </summary>
    /// <param name="name">The name of the workflow to run</param>
    /// <returns>The configured <see cref="IWorkflowProcessDefinitionBuilder"/></returns>
    IWorkflowProcessDefinitionBuilder WithName(string name);

    /// <summary>
    /// Configures the task to run the workflow with the specified version
    /// </summary>
    /// <param name="version">The version of the workflow to run</param>
    /// <returns>The configured <see cref="IWorkflowProcessDefinitionBuilder"/></returns>
    IWorkflowProcessDefinitionBuilder WithVersion(string version);

    /// <summary>
    /// Sets the input of the workflow to run
    /// </summary>
    /// <param name="input">The input of the workflow to run. Supports runtime expressions</param>
    /// <returns>The configured <see cref="IWorkflowProcessDefinitionBuilder"/></returns>
    IWorkflowProcessDefinitionBuilder WithInput(object input);

}