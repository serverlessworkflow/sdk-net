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
/// Defines the fundamentals of a service used to build <see cref="RetryPolicyDefinition"/>s
/// </summary>
public interface IRetryPolicyDefinitionBuilder
{

    /// <summary>
    /// Sets the runtime expression used to determine whether to retry the filtered error
    /// </summary>
    /// <param name="expression">The runtime expression used to determine whether to retry the filtered error</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder When(string expression);

    /// <summary>
    /// Sets the runtime expression used to determine whether not to retry the filtered error
    /// </summary>
    /// <param name="expression">The runtime expression used to determine whether not to retry the filtered error</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder ExceptWhen(string expression);

    /// <summary>
    /// Sets the limits of the retry policy to build
    /// </summary>
    /// <param name="limits">The <see cref="RetryPolicyLimitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder Limit(RetryPolicyLimitDefinition limits);

    /// <summary>
    /// Sets the limits of the retry policy to build
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to build the <see cref="RetryPolicyDefinition"/> to use</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder Limit(Action<IRetryPolicyLimitDefinitionBuilder> setup);

    /// <summary>
    /// Sets the delay duration between retry attempts
    /// </summary>
    /// <param name="duration">The duration between retry attempts</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder Delay(Duration duration);

    /// <summary>
    /// Sets the backoff strategy of the retry policy to build
    /// </summary>
    /// <param name="backoff">The <see cref="BackoffStrategyDefinition"/> to use</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder Backoff(BackoffStrategyDefinition backoff);

    /// <summary>
    /// Sets the backoff strategy of the retry policy to build
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to build the <see cref="RetryPolicyDefinition"/> to use</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder Backoff(Action<IBackoffStrategyDefinitionBuilder> setup);

    /// <summary>
    /// Sets the jitter to apply to the retry policy to build
    /// </summary>
    /// <param name="jitter">The <see cref="JitterDefinition"/> to use</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder Jitter(JitterDefinition jitter);

    /// <summary>
    /// Sets the jitter to apply to the retry policy to build
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to build the <see cref="JitterDefinition"/> to use</param>
    /// <returns>The configured <see cref="IRetryPolicyDefinitionBuilder"/></returns>
    IRetryPolicyDefinitionBuilder Jitter(Action<IJitterDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="RetryPolicyDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="RetryPolicyDefinition"/></returns>
    RetryPolicyDefinition Build();

}
