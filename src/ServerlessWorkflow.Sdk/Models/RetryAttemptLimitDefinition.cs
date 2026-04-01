namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of the limits for all retry attempts of a given policy
/// </summary>
[Description("Represents the definition of the limits for all retry attempts of a given policy")]
[DataContract]
public sealed record RetryAttemptLimitDefinition
{

    /// <summary>
    /// Gets/sets the maximum attempts count
    /// </summary>
    [Description("The maximum attempts count")]
    [DataMember(Order = 1, Name = "count"), JsonPropertyOrder(1), JsonPropertyName("count")]
    public uint? Count { get; init; }

    /// <summary>
    /// Gets/sets the duration limit, if any, for all retry attempts
    /// </summary>
    [Description("The duration limit, if any, for all retry attempts")]
    [DataMember(Order = 2, Name = "duration"), JsonPropertyOrder(2), JsonPropertyName("duration")]
    public Duration? Duration { get; init; }

}