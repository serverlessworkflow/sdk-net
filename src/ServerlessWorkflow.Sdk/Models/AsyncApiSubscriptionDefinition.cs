namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure an AsyncAPI subscription
/// </summary>
[Description("Represents an object used to configure an AsyncAPI subscription")]
[DataContract]
public sealed record AsyncApiSubscriptionDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to filter consumed messages
    /// </summary>
    [Description("A runtime expression, if any, used to filter consumed messages")]
    [DataMember(Order = 1, Name = "filter"), JsonPropertyOrder(1), JsonPropertyName("filter")]
    public string? Filter { get; init; }

    /// <summary>
    /// Gets/sets an object used to configure the subscription's lifetime.
    /// </summary>
    [Description("An object used to configure the subscription's lifetime")]
    [Required]
    [DataMember(Order = 2, Name = "consume"), JsonPropertyOrder(2), JsonPropertyName("consume")]
    public required AsyncApiSubscriptionLifetimeDefinition Consume { get; init; }

    /// <summary>
    /// Gets/sets the configuration of the iterator, if any, used to process each consumed message
    /// </summary>
    [Description("The configuration of the iterator, if any, used to process each consumed message")]
    [DataMember(Order = 3, Name = "foreach"), JsonPropertyOrder(3), JsonPropertyName("foreach")]
    public SubscriptionIteratorDefinition? Foreach { get; init; }

}
