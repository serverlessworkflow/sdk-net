namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IRetryAttemptLimitDefinitionBuilder"/> interface
/// </summary>
public sealed class RetryAttemptLimitDefinitionBuilder
    : IRetryAttemptLimitDefinitionBuilder
{

    uint? attemptCount;
    Duration? attemptDuration;

    /// <inheritdoc/>
    public IRetryAttemptLimitDefinitionBuilder Count(uint count)
    {
        attemptCount = count;
        return this;
    }

    /// <inheritdoc/>
    public IRetryAttemptLimitDefinitionBuilder Duration(Duration duration)
    {
        attemptDuration = duration;
        return this;
    }

    /// <inheritdoc/>
    public RetryAttemptLimitDefinition Build() => new()
    {
        Count = attemptCount,
        Duration = attemptDuration
    };

}
