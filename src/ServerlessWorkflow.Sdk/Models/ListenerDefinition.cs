namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the configuration of an event listener
/// </summary>
[Description("Represents the configuration of an event listener")]
[DataContract]
public sealed record ListenerDefinition
{

    /// <summary>
    /// Gets/sets the listener's target
    /// </summary>
    [Description("The listener's target")]
    [Required]
    [DataMember(Order = 1, Name = "to"), JsonPropertyOrder(1), JsonPropertyName("to")]
    public required EventConsumptionStrategyDefinition To { get; init; }

    /// <summary>
    /// Gets/sets a string that specifies how events are read during the listen operation<para></para>
    /// See <see cref="EventReadMode"/>. Defaults to <see cref="EventReadMode.Data"/>
    /// </summary>
    [Description("A string that specifies how events are read during the listen operation. See EventReadMode. Defaults to EventReadMode.Data")]
    [DataMember(Order = 2, Name = "read"), JsonPropertyOrder(2), JsonPropertyName("read")]
    public string? Read { get; init; }

}
