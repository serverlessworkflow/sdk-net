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
/// Represents the default implementation of the <see cref="IRetryStrategyBuilder"/> interface
/// </summary>
public class RetryStrategyBuilder
    : IRetryStrategyBuilder
{

    /// <summary>
    /// Gets the <see cref="RetryDefinition"/> to configure
    /// </summary>
    protected RetryDefinition Strategy { get; } = new RetryDefinition();

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        this.Strategy.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithNoDelay()
    {
        this.Strategy.Delay = null;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithDelayOf(TimeSpan duration)
    {
        this.Strategy.Delay = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithDelayIncrement(TimeSpan duration)
    {
        this.Strategy.Increment = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithDelayMultiplier(float multiplier)
    {
        this.Strategy.Multiplier = multiplier;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithMaxDelay(TimeSpan duration)
    {
        this.Strategy.MaxDelay = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder MaxAttempts(uint maxAttempts)
    {
        this.Strategy.MaxAttempts = maxAttempts;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithJitterDuration(TimeSpan duration)
    {
        this.Strategy.JitterDuration = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithJitterMultiplier(float multiplier)
    {
        this.Strategy.JitterMultiplier = multiplier;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithExtensionProperty(string name, object value)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Strategy.ExtensionData ??= new Dictionary<string, object>();
        this.Strategy.ExtensionData[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryStrategyBuilder WithExtensionProperties(IDictionary<string, object> properties)
    {
        this.Strategy.ExtensionData = properties ?? throw new ArgumentNullException(nameof(properties));
        return this;
    }

    /// <inheritdoc/>
    public virtual RetryDefinition Build() => this.Strategy;

}
