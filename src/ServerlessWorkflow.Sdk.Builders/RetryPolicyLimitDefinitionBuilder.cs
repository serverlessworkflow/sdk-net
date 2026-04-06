namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IRetryPolicyLimitDefinitionBuilder"/> interface
/// </summary>
public sealed class RetryPolicyLimitDefinitionBuilder
    : IRetryPolicyLimitDefinitionBuilder
{

    RetryAttemptLimitDefinitionBuilder? limitAttempt;
    Duration? limitDuration;

    /// <inheritdoc/>
    public IRetryAttemptLimitDefinitionBuilder Attempt()
    {
        limitAttempt = new RetryAttemptLimitDefinitionBuilder();
        return limitAttempt;
    }

    /// <inheritdoc/>
    public IRetryPolicyLimitDefinitionBuilder Duration(Duration duration)
    {
        limitDuration = duration;
        return this;
    }

    /// <inheritdoc/>
    public RetryPolicyLimitDefinition Build() => new()
    {
        Attempt = limitAttempt?.Build(),
        Duration = limitDuration,
    };

}
