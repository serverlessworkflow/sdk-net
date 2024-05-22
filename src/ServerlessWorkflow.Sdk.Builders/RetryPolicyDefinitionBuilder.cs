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
public class RetryPolicyDefinitionBuilder
    : IRetryPolicyDefinitionBuilder
{

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to retry running the task, in a given context
    /// </summary>
    protected string? RetryWhen { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to retry running the task, in a given context
    /// </summary>
    protected string? RetryExceptWhen { get; set; }

    /// <summary>
    /// Gets/sets the parameters, if any, that control the randomness or variability of the delay between retry attempts
    /// </summary>
    protected RetryPolicyLimitDefinition? RetryLimit { get; set; }

    /// <summary>
    /// Gets/sets the delay duration between retry attempts
    /// </summary>
    protected Duration? RetryDelay { get; set; }

    /// <summary>
    /// Gets/sets the limits, if any, of the retry policy to build
    /// </summary>
    protected BackoffStrategyDefinition? RetryBackoff { get; set; }

    /// <summary>
    /// Gets/sets the backoff strategy to use, if any
    /// </summary>
    protected JitterDefinition? RetryJitter { get; set; }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder When(string expression)
    {
        this.RetryWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder ExceptWhen(string expression)
    {
        this.RetryExceptWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder Limit(RetryPolicyLimitDefinition limits)
    {
        this.RetryLimit = limits;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder Limit(Action<IRetryPolicyLimitDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new RetryPolicyLimitDefinitionBuilder();
        setup(builder);
        return this.Limit(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder Delay(Duration duration)
    {
        this.RetryDelay = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder Backoff(BackoffStrategyDefinition backoff)
    {
        this.RetryBackoff = backoff;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder Backoff(Action<IBackoffStrategyDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new BackoffStrategyDefinitionBuilder();
        setup(builder);
        return this.Backoff(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder Jitter(JitterDefinition jitter)
    {
        this.RetryJitter = jitter;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyDefinitionBuilder Jitter(Action<IJitterDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new JitterDefinitionBuilder();
        setup(builder);
        return this.Jitter(builder.Build());
    }

    /// <inheritdoc/>
    public virtual RetryPolicyDefinition Build() => new()
    {
        When = this.RetryWhen,
        ExceptWhen = this.RetryExceptWhen,
        Limit = this.RetryLimit,
        Delay = this.RetryDelay,
        Backoff = this.RetryBackoff,
        Jitter = this.RetryJitter
    };

}
