namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a linear backoff
/// </summary>
[Description("Represents the definition of a linear backoff")]
[DataContract]
public sealed record LinearBackoffDefinition
    : BackoffDefinition
{

    /// <summary>
    /// Gets/sets the linear incrementation to the delay between retry attempts
    /// </summary>
    [Description("The linear incrementation to the delay between retry attempts")]
    [DataMember(Order = 1, Name = "increment"), JsonPropertyOrder(1), JsonPropertyName("increment")]
    public Duration? Increment { get; init; }

}