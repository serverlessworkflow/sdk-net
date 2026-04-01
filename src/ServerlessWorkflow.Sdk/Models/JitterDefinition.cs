namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of the parameters that control the randomness or variability of a delay, typically between retry attempts
/// </summary>
[Description("Represents the definition of the parameters that control the randomness or variability of a delay, typically between retry attempts")]
[DataContract]
public sealed record JitterDefinition
{

    /// <summary>
    /// Gets/sets the minimum duration of the jitter range
    /// </summary>
    [Description("The minimum duration of the jitter range")]
    [DataMember(Order = 1, Name = "from"), JsonPropertyOrder(1), JsonPropertyName("from")]
    public required Duration From { get; init; }

    /// <summary>
    /// Gets/sets the maximum duration of the jitter range
    /// </summary>
    [Description("The maximum duration of the jitter range")]
    [DataMember(Order = 2, Name = "to"), JsonPropertyOrder(2), JsonPropertyName("to")]
    public required Duration To { get; init; }

}