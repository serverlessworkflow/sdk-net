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
    public virtual IRetryStrategyBuilder WithDelayIncrementation(TimeSpan duration)
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
    public virtual RetryDefinition Build() => this.Strategy;

}
