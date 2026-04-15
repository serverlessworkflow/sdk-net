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
/// Represents the default implementation of the <see cref="IRetryPolicyDefinitionBuilder"/> interface
/// </summary>
public sealed class RetryPolicyDefinitionBuilder
    : IRetryPolicyDefinitionBuilder
{

    string? retryWhen;
    string? retryExceptWhen;
    RetryPolicyLimitDefinition? retryLimit;
    Duration? retryDelay;
    BackoffStrategyDefinition? retryBackoff;
    JitterDefinition? retryJitter;

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder When(string expression)
    {
        retryWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder ExceptWhen(string expression)
    {
        retryExceptWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Limit(RetryPolicyLimitDefinition limits)
    {
        retryLimit = limits;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Limit(Action<IRetryPolicyLimitDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new RetryPolicyLimitDefinitionBuilder();
        setup(builder);
        return Limit(builder.Build());
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Delay(Duration duration)
    {
        retryDelay = duration;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Backoff(BackoffStrategyDefinition backoff)
    {
        retryBackoff = backoff;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Backoff(Action<IBackoffStrategyDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new BackoffStrategyDefinitionBuilder();
        setup(builder);
        return Backoff(builder.Build());
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Jitter(JitterDefinition jitter)
    {
        retryJitter = jitter;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Jitter(Action<IJitterDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new JitterDefinitionBuilder();
        setup(builder);
        return Jitter(builder.Build());
    }

    /// <inheritdoc/>
    public RetryPolicyDefinition Build() => new()
    {
        When = retryWhen,
        ExceptWhen = retryExceptWhen,
        Limit = retryLimit,
        Delay = retryDelay,
        Backoff = retryBackoff,
        Jitter = retryJitter
    };

}
