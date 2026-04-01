namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an epoch, which is the duration elapsed between a datetime and midnight of 1970-01-01 UTC
/// </summary>
[Description("Represents an epoch, which is the duration elapsed between a datetime and midnight of 1970-01-01 UTC.")]
[DataContract]
public sealed record Epoch
{

    /// <summary>
    /// Gets/sets the epoch's total milliseconds
    /// </summary>
    [Description("The epoch's total milliseconds.")]
    [DataMember(Order = 1, Name = "ms"), JsonPropertyOrder(1), JsonPropertyName("ms")]
    public required ulong Milliseconds { get; init; }

}