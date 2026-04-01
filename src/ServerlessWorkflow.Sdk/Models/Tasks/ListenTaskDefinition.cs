namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the configuration of a task used to listen to specific events
/// </summary>
[Description("Represents the configuration of a task used to listen to specific events")]
[DataContract]
public sealed record ListenTaskDefinition
    : TaskDefinition
{

    /// <summary>
    /// Gets/sets the configuration of the listener to use
    /// </summary>
    [Description("The configuration of the listener to use")]
    [Required]
    [DataMember(Order = 1, Name = "listen"), JsonPropertyOrder(1), JsonPropertyName("listen")]
    public required ListenerDefinition Listen { get; init; }

    /// <summary>
    /// Gets/sets the configuration of the iterator, if any, used to process each consumed event
    /// </summary>
    [Description("The configuration of the iterator, if any, used to process each consumed event")]
    [DataMember(Order = 2, Name = "foreach"), JsonPropertyOrder(2), JsonPropertyName("foreach")]
    public SubscriptionIteratorDefinition? Foreach { get; init; }

}
