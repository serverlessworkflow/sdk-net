namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to describe a datetime
/// </summary>
[Description("Represents an object used to describe a datetime.")]
[DataContract]
public sealed record DateTimeDescriptor
{

    /// <summary>
    /// Gets/sets the ISO 8601 representation of the described datetime
    /// </summary>
    [Description("The ISO 8601 representation of the described datetime.")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "iso8601"), JsonPropertyOrder(1), JsonPropertyName("iso8601")]
    public required string Iso8601 { get; init; }

    /// <summary>
    /// Gets/sets the duration elapsed between the described datetime and midnight of 1970-01-01 UTC
    /// </summary>
    [Description("The duration elapsed between the described datetime and midnight of 1970-01-01 UTC.")]
    [Required]
    [DataMember(Order = 2, Name = "epoch"), JsonPropertyOrder(2), JsonPropertyName("epoch")]
    public required Epoch Epoch { get; init; }

}