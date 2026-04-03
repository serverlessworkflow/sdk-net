namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure the lifetime of an AsyncAPI subscription
/// </summary>
[Description("Represents an object used to configure the lifetime of an AsyncAPI subscription")]
[DataContract]
public sealed record AsyncApiSubscriptionLifetimeDefinition
{

    /// <summary>
    /// Gets/sets the duration that defines for how long to consume messages
    /// </summary>
    [Description("The duration that defines for how long to consume messages")]
    [DataMember(Order = 1, Name = "for"), JsonPropertyOrder(1), JsonPropertyName("for")]
    public Duration? For { get; init; }

    /// <summary>
    /// Gets/sets the amount of messages to consume.<para></para>
    /// Required if <see cref="While"/> and <see cref="Until"/> have not been set.
    /// </summary>
    [Description("The amount of messages to consume. Required if While and Until have not been set.")]
    [DataMember(Order = 2, Name = "amount"), JsonPropertyOrder(2), JsonPropertyName("amount")]
    public int? Amount { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to determine whether or not to keep consuming messages.<para></para>
    /// Required if <see cref="Amount"/> and <see cref="Until"/> have not been set.
    /// </summary>
    [Description("A runtime expression, if any, used to determine whether or not to keep consuming messages. Required if Amount and Until have not been set.")]
    [DataMember(Order = 3, Name = "while"), JsonPropertyOrder(3), JsonPropertyName("while")]
    public string? While { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to determine until when to consume messages..<para></para>
    /// Required if <see cref="Amount"/> and <see cref="While"/> have not been set.
    /// </summary>
    [Description("A runtime expression, if any, used to determine until when to consume messages. Required if Amount and While have not been set.")]
    [DataMember(Order = 4, Name = "until"), JsonPropertyOrder(4), JsonPropertyName("until")]
    public string? Until { get; init; }

}