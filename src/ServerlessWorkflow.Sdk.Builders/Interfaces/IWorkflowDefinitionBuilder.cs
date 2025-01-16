﻿// Copyright © 2024-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Defines the fundamentals of a service used to build and configure <see cref="WorkflowDefinition"/>s
/// </summary>
public interface IWorkflowDefinitionBuilder
    : ITaskDefinitionMapBuilder<IWorkflowDefinitionBuilder>
{

    /// <summary>
    /// Sets the semantic version of the Serverless Workflow DSL used to define the workflow
    /// </summary>
    /// <param name="version">The semantic version of the Serverless Workflow DSL used to define the workflow</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseDsl(string version);

    /// <summary>
    /// Sets the workflow's namespace
    /// </summary>
    /// <param name="namespace">The workflow's namespace</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithNamespace(string @namespace);

    /// <summary>
    /// Sets the workflow's name
    /// </summary>
    /// <param name="name">The workflow's name</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithName(string name);

    /// <summary>
    /// Sets the workflow's semantic version
    /// </summary>
    /// <param name="version">The workflow's semantic version</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithVersion(string version);

    /// <summary>
    /// Sets the workflow's title
    /// </summary>
    /// <param name="title">The workflow's title</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithTitle(string title);

    /// <summary>
    /// Sets the workflow's summary
    /// </summary>
    /// <param name="summary">The workflow's summary</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithSummary(string summary);

    /// <summary>
    /// Adds a new tag to the workflow
    /// </summary>
    /// <param name="name">The tag's name</param>
    /// <param name="value">The tag's value</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithTag(string name, string value);

    /// <summary>
    /// Sets the tags of the workflow
    /// </summary>
    /// <param name="arguments">A name/value mapping of the workflow's tags</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithTag(IDictionary<string, string> arguments);

    /// <summary>
    /// Sets the workflow's timeout
    /// </summary>
    /// <param name="name">The name of the workflow's timeout</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithTimeout(string name);

    /// <summary>
    /// Sets the workflow's timeout
    /// </summary>
    /// <param name="timeout">The workflow's timeout</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithTimeout(TimeoutDefinition timeout);

    /// <summary>
    /// Sets the workflow's timeout
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the workflow's timeout</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithTimeout(Action<ITimeoutDefinitionBuilder> setup);

    /// <summary>
    /// Sets the workflow's input data
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the workflow's input</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithInput(Action<IInputDataModelDefinitionBuilder> setup);

    /// <summary>
    /// Sets the workflow's output data
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the workflow's output</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder WithOutput(Action<IOutputDataModelDefinitionBuilder> setup);

    /// <summary>
    /// Uses the specified authentication policy
    /// </summary>
    /// <param name="name">The name of the authentication policy to register</param>
    /// <param name="authentication">The authentication policy to use</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseAuthentication(string name, AuthenticationPolicyDefinition authentication);

    /// <summary>
    /// Uses the specified authentication policy
    /// </summary>
    /// <param name="name">The name of the authentication policy to register</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the authentication policy to use</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseAuthentication(string name, Action<IAuthenticationPolicyDefinitionBuilder> setup);

    /// <summary>
    /// Uses the specified extension
    /// </summary>
    /// <param name="name">The name of the extension to use</param>
    /// <param name="extension">The definition of the extension to use</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseExtension(string name, ExtensionDefinition extension);

    /// <summary>
    /// Uses the specified extension
    /// </summary>
    /// <param name="name">The name of the extension to use</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the definition of the extension to use</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseExtension(string name, Action<IExtensionDefinitionBuilder> setup);

    /// <summary>
    /// Uses the specified function
    /// </summary>
    /// <param name="name">The name of the function to use</param>
    /// <param name="task">The underlying task the function performs</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseFunction(string name, TaskDefinition task);

    /// <summary>
    /// Uses the specified function
    /// </summary>
    /// <param name="name">The name of the function to use</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the underlying task the function performs</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseFunction(string name, Action<IGenericTaskDefinitionBuilder> setup);

    /// <summary>
    /// Uses the specified retry policy
    /// </summary>
    /// <param name="name">The name of the retry policy to register</param>
    /// <param name="retry">The retry policy to use</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseRetry(string name, RetryPolicyDefinition retry);

    /// <summary>
    /// Uses the specified retry policy
    /// </summary>
    /// <param name="name">The name of the retry policy to register</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the retry policy to use</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseRetry(string name, Action<IRetryPolicyDefinitionBuilder> setup);

    /// <summary>
    /// Uses the specified secret
    /// </summary>
    /// <param name="secret">The name of the secret to use</param>
    /// <returns>The configured <see cref="IWorkflowDefinitionBuilder"/></returns>
    IWorkflowDefinitionBuilder UseSecret(string secret);

    /// <summary>
    /// Builds the configured <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="WorkflowDefinition"/></returns>
    new WorkflowDefinition Build();

}
