namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IRetryAttemptLimitDefinitionBuilder"/> interface
/// </summary>
public sealed class RetryAttemptLimitDefinitionBuilder
    : IRetryAttemptLimitDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the maximum attempts count
    /// </summary>
    protected uint? AttemptCount { get; set; }

    /// <summary>
    /// Gets/sets the duration limit, if any, for all retry attempts
    /// </summary>
    protected Duration? AttemptDuration { get; set; }

    /// <inheritdoc/>
    public IRetryAttemptLimitDefinitionBuilder Count(uint count)
    {
        AttemptCount = count;
        IRetryAttemptLimitDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IRetryAttemptLimitDefinitionBuilder Duration(Duration duration)
    {
        AttemptDuration = duration;
        IRetryAttemptLimitDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public RetryAttemptLimitDefinition Build() => new()
    {
        Count = AttemptCount,
        Duration = AttemptDuration
    };

}
