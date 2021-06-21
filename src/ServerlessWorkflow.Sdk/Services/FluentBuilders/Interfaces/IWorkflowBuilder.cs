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
using System;
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="WorkflowDefinition"/>s
    /// </summary>
    public interface IWorkflowBuilder
        : IMetadataContainerBuilder<IWorkflowBuilder>
    {

        /// <summary>
        /// Sets the id of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="key">The id of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithId(string key);

        /// <summary>
        /// Sets the unique key of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="key">The unique key of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithKey(string key);

        /// <summary>
        /// Sets the name of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="name">The name of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithName(string name);

        /// <summary>
        /// Sets the description of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="description">The description of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithDescription(string description);

        /// <summary>
        /// Sets the version of the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="version">The description of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithVersion(string version);

        /// <summary>
        /// Sets the Serverless Workflow specification version. Defaults to latest
        /// </summary>
        /// <param name="specVersion">The Serverless Workflow specification version</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithSpecVersion(string specVersion);

        /// <summary>
        /// Sets the <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/> <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to the data <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/></param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithDataInputSchema(Uri uri);

        /// <summary>
        /// Sets the <see cref="WorkflowDefinition"/> data input <see cref="JSchema"/>
        /// </summary>
        /// <param name="schema">The <see cref="WorkflowDefinition"/>'s <see cref="JSchema"/></param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithDataInputSchema(JSchema schema);

        /// <summary>
        /// Annotates the <see cref="WorkflowDefinition"/> to build
        /// </summary>
        /// <param name="annotation">The annotation to append to the <see cref="WorkflowDefinition"/> to build</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AnnotateWith(string annotation);

        /// <summary>
        /// Configures the expression language used by the <see cref="WorkflowDefinition"/> to build
        /// </summary>
        /// <param name="language">The expression language to use</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder UseExpressionLanguage(string language);

        /// <summary>
        /// Configures the <see cref="WorkflowDefinition"/> to use the 'jq' expression language
        /// </summary>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder UseJq();

        /// <summary>
        /// Adds the <see cref="WorkflowDefinition"/> <see cref="AuthenticationDefinition"/>s defined in the specified file
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of the file that defines the <see cref="AuthenticationDefinition"/>s</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder ImportAuthenticationDefinitionsFrom(Uri uri);

        /// <summary>
        /// Uses the specified <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/>s
        /// </summary>
        /// <param name="authenticationDefinitions">An array that contains the <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/>s</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder UseAuthenticationDefinitions(params AuthenticationDefinition[] authenticationDefinitions);

        /// <summary>
        /// Adds the specified <see cref="AuthenticationDefinition"/> to the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="authenticationDefinition">The <see cref="AuthenticationDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddAuthenticationDefinition(AuthenticationDefinition authenticationDefinition);

        /// <summary>
        /// Adds a new <see cref="AuthenticationDefinition"/> with scheme <see cref="AuthenticationScheme.Basic"/> to the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to add</param>
        /// <param name="configurationAction">An <see cref="Action{T}"/> used to configure the service used to build <see cref="AuthenticationDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddBasicAuthentication(string name, Action<IBasicAuthenticationBuilder> configurationAction);

        /// <summary>
        /// Adds a new <see cref="AuthenticationDefinition"/> with scheme <see cref="AuthenticationScheme.Bearer"/> to the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to add</param>
        /// <param name="configurationAction">An <see cref="Action{T}"/> used to configure the service used to build <see cref="AuthenticationDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddBearerAuthentication(string name, Action<IBearerAuthenticationBuilder> configurationAction);

        /// <summary>
        /// Adds a new <see cref="AuthenticationDefinition"/> with scheme <see cref="AuthenticationScheme.OAuth2"/> to the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to add</param>
        /// <param name="configurationAction">An <see cref="Action{T}"/> used to configure the service used to build <see cref="AuthenticationDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddOAuth2Authentication(string name, Action<IOAuth2AuthenticationBuilder> configurationAction);

        /// <summary>
        /// Adds the <see cref="WorkflowDefinition"/> constants defined in the specified file
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of the file that defines the constants</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder ImportConstantsFrom(Uri uri);

        /// <summary>
        /// Uses the specified <see cref="WorkflowDefinition"/>'s constants
        /// </summary>
        /// <param name="constants">An object that represents the <see cref="WorkflowDefinition"/>'s constants</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder UseConstants(object constants);

        /// <summary>
        /// Adds the specified constants to the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="name">The name of the constant to add</param>
        /// <param name="value">The value of the constant to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddConstant(string name, object value);

        /// <summary>
        /// Uses the specified <see cref="WorkflowDefinition"/> secrets
        /// </summary>
        /// <param name="secrets">An <see cref="IEnumerable{T}"/> containing the secrets to use</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder UseSecrets(IEnumerable<string> secrets);

        /// <summary>
        /// Adds the specified secret to the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="secret">The secret to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddSecret(string secret);

        /// <summary>
        /// Configures the <see cref="WorkflowDefinition"/>'s <see cref="ExecutionTimeoutDefinition"/>
        /// </summary>
        /// <param name="timeoutSetup">An <see cref="Action{T}"/> used to setup the <see cref="WorkflowDefinition"/>'s <see cref="ExecutionTimeoutDefinition"/></param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder WithExecutionTimeout(Action<IExecutionTimeoutBuilder> timeoutSetup);

        /// <summary>
        /// Configures the <see cref="WorkflowDefinition"/> to not terminate its execution when there are no active execution paths
        /// </summary>
        /// <param name="keepActive">A boolean indicating whether or not to keep the <see cref="WorkflowDefinition"/> active</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder KeepActive(bool keepActive = true);

        /// <summary>
        /// Sets and configures the startup <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the startup <see cref="StateDefinition"/></param>
        /// <returns>A new <see cref="IPipelineBuilder"/> used to configure the <see cref="WorkflowDefinition"/>'s <see cref="StateDefinition"/>s</returns>
        IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Sets and configures the startup <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="name">The name of the startup <see cref="StateDefinition"/></param>
        /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the startup <see cref="StateDefinition"/></param>
        /// <returns>A new <see cref="IPipelineBuilder"/> used to configure the <see cref="WorkflowDefinition"/>'s <see cref="StateDefinition"/>s</returns>
        IPipelineBuilder StartsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Adds the <see cref="EventDefinition"/>s defined in the specified file
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of the file that defines the <see cref="EventDefinition"/>s</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder ImportEventsFrom(Uri uri);

        /// <summary>
        /// Adds the specified <see cref="EventDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="e">The <see cref="EventDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddEvent(EventDefinition e);

        /// <summary>
        /// Adds the specified <see cref="EventDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="eventSetup">The <see cref="Action{T}"/> used to setup the <see cref="EventDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddEvent(Action<IEventBuilder> eventSetup);

        /// <summary>
        /// Adds the <see cref="FunctionDefinition"/>s defined in the specified file
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of the file that defines the <see cref="FunctionDefinition"/>s</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder ImportFunctionsFrom(Uri uri);

        /// <summary>
        /// Adds the specified <see cref="FunctionDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="functionSetup">The <see cref="Action{T}"/> used to setup the <see cref="FunctionDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddFunction(Action<IFunctionBuilder> functionSetup);

        /// <summary>
        /// Adds the specified <see cref="FunctionDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="function">The <see cref="FunctionDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddFunction(FunctionDefinition function);

        /// <summary>
        /// Adds the <see cref="RetryStrategyDefinition"/>s defined in the specified file
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of the file that defines the <see cref="RetryStrategyDefinition"/>s</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder ImportRetryStrategiesFrom(Uri uri);

        /// <summary>
        /// Adds the specified <see cref="RetryStrategyDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="strategy">The <see cref="Action{T}"/> used to setup the <see cref="RetryStrategyDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddRetryStrategy(RetryStrategyDefinition strategy);

        /// <summary>
        /// Adds the specified <see cref="RetryStrategyDefinition"/> to the <see cref="WorkflowDefinition"/> to create
        /// </summary>
        /// <param name="retryStrategySetup">The <see cref="Action{T}"/> used to setup the <see cref="RetryStrategyDefinition"/> to add</param>
        /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
        IWorkflowBuilder AddRetryStrategy(Action<IRetryStrategyBuilder> retryStrategySetup);

        /// <summary>
        /// Builds the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="WorkflowDefinition"/></returns>
        WorkflowDefinition Build();

    }

}
