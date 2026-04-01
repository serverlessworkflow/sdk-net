namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the configuration of the limits of a retry policy
/// </summary>
[Description("Represents the configuration of the limits of a retry policy")]
[DataContract]
public sealed record RetryPolicyLimitDefinition
{

    /// <summary>
    /// Gets/sets the definition of the limits for all retry attempts of a given policy
    /// </summary>
    [Description("The definition of the limits for all retry attempts of a given policy")]
    [DataMember(Order = 1, Name = "attempt"), JsonPropertyOrder(1), JsonPropertyName("attempt")]
    public RetryAttemptLimitDefinition? Attempt { get; init; }

    /// <summary>
    /// Gets/sets the maximum duration, if any, during which to retry a given task
    /// </summary>
    [Description("The maximum duration, if any, during which to retry a given task")]
    [DataMember(Order = 2, Name = "duration"), JsonPropertyOrder(2), JsonPropertyName("duration")]
    public Duration? Duration { get; init; }

}
