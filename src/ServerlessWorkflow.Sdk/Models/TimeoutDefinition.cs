namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a timeout
/// </summary>
[Description("Represents the definition of a timeout")]
[DataContract]
public sealed record TimeoutDefinition
{

    /// <summary>
    /// Gets/sets the duration after which to timeout
    /// </summary>
    [Description("The duration after which to timeout")]
    [Required]
    [DataMember(Order = 1, Name = "after"), JsonPropertyOrder(1), JsonPropertyName("after"), JsonConverter(typeof(OneOfJsonConverter<Duration, string>))]
    public required OneOf<Duration, string> After { get; init; }

}