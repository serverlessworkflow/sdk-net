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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="RetryDefinition"/>s
/// </summary>
public interface IRetryStrategyBuilder
    : IExtensibleBuilder<IRetryStrategyBuilder>
{

    /// <summary>
    /// Sets the name of the <see cref="RetryDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="RetryDefinition"/> to build</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithName(string name);

    /// <summary>
    /// Sets the duration between successive retry attempts
    /// </summary>
    /// <param name="duration">The duration to wait between two retry attempts</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithDelayOf(TimeSpan duration);

    /// <summary>
    /// Configures the <see cref="RetryDefinition"/> to not delay successive retry attempts
    /// </summary>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithNoDelay();

    /// <summary>
    /// Configures the <see cref="RetryDefinition"/>'s max delay between retry attempts
    /// </summary>
    /// <param name="duration">The maximum duration to wait between two retry attempt</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithMaxDelay(TimeSpan duration);

    /// <summary>
    /// Configures the maximum amount of retry attempts
    /// </summary>
    /// <param name="maxAttempts">The maximum amount of retry attempts</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder MaxAttempts(uint maxAttempts);

    /// <summary>
    /// Configures the duration which will be added to the delay between successive retries
    /// </summary>
    /// <param name="duration">The duration which will be added to the delay between successive retries</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithDelayIncrement(TimeSpan duration);

    /// <summary>
    /// Configures the value by which the delay is multiplied before each attempt.
    /// </summary>
    /// <param name="multiplier">The value by which the delay is multiplied before each attempt.</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithDelayMultiplier(float multiplier);

    /// <summary>
    /// Configures the maximum amount of random time added or subtracted from the delay between each retry relative to total delay
    /// </summary>
    /// <param name="multiplier">The maximum amount of random time added or subtracted from the delay between each retry relative to total delay</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithJitterMultiplier(float multiplier);

    /// <summary>
    /// Configures the absolute maximum amount of random time added or subtracted from the delay between each retry
    /// </summary>
    /// <param name="duration">The absolute maximum amount of random time added or subtracted from the delay between each retry</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithJitterDuration(TimeSpan duration);

    /// <summary>
    /// Builds the <see cref="RetryDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="RetryDefinition"/></returns>
    RetryDefinition Build();

}
