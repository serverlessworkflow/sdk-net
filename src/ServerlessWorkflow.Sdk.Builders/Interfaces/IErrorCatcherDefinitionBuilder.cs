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

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ErrorCatcherDefinition"/>s
/// </summary>
public interface IErrorCatcherDefinitionBuilder
{

    /// <summary>
    /// Catches errors matching the specified filter
    /// </summary>
    /// <param name="filter">The filter used to catch errors. If not set, catches all errors.</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder Errors(ErrorFilterDefinition filter);

    /// <summary>
    /// Catches errors matching the specified filter
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the filter used to catch errors. If not set, catches all errors.</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder Errors(Action<IErrorFilterDefinitionBuilder> setup);

    /// <summary>
    /// Sets the name of the variable that contains caught errors
    /// </summary>
    /// <param name="variableName">The name of the variable that contains caught errors</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder As(string variableName);

    /// <summary>
    /// Sets the runtime expression used to determine whether to catch the filtered error
    /// </summary>
    /// <param name="expression">The runtime expression used to determine whether to catch the filtered error</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder When(string expression);

    /// <summary>
    /// Sets the runtime expression used to determine whether not to catch the filtered error
    /// </summary>
    /// <param name="expression">The runtime expression used to determine whether not to catch the filtered error</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder ExceptWhen(string expression);

    /// <summary>
    /// Sets the reference to the retry policy to use
    /// </summary>
    /// <param name="reference">A reference to the retry policy to use</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder Retry(Uri reference);

    /// <summary>
    /// Sets the reference to the retry policy to use
    /// </summary>
    /// <param name="retryPolicy">The retry policy to use</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder Retry(RetryPolicyDefinition retryPolicy);

    /// <summary>
    /// Sets the reference to the retry policy to use
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the retry policy to use</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder Retry(Action<IRetryPolicyDefinitionBuilder> setup);

    /// <summary>
    /// Configures the tasks to execute the specified task after catching or after retry exhaustion
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tasks to execute</param>
    /// <returns>The configured <see cref="ITryTaskDefinitionBuilder"/></returns>
    IErrorCatcherDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="ErrorCatcherDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ErrorCatcherDefinition"/></returns>
    ErrorCatcherDefinition Build();

}
