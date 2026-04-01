namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a retry backoff strategy
/// </summary>
[Description("Represents the definition of a retry backoff strategy")]
[DataContract]
public sealed record BackoffStrategyDefinition
{

    /// <summary>
    /// Gets/sets the definition of the constant backoff to use, if any
    /// </summary>
    [Description("The definition of the constant backoff to use, if any")]
    [DataMember(Order = 1, Name = "constant"), JsonPropertyOrder(1), JsonPropertyName("constant")]
    public ConstantBackoffDefinition? Constant { get; set; }

    /// <summary>
    /// Gets/sets the definition of the exponential backoff to use, if any
    /// </summary>
    [Description("The definition of the exponential backoff to use, if any")]
    [DataMember(Order = 2, Name = "exponential"), JsonPropertyOrder(2), JsonPropertyName("exponential")]
    public ExponentialBackoffDefinition? Exponential { get; set; }

    /// <summary>
    /// Gets/sets the definition of the linear backoff to use, if any
    /// </summary>
    [Description("The definition of the linear backoff to use, if any")]
    [DataMember(Order = 3, Name = "linear"), JsonPropertyOrder(3), JsonPropertyName("linear")]
    public LinearBackoffDefinition? Linear { get; set; }

}
