namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an AsyncAPI message
/// </summary>
[Description("Represents the definition of an AsyncAPI message")]
[DataContract]
public sealed record AsyncApiMessageDefinition
{

    /// <summary>
    /// Gets/sets the message's payload, if any
    /// </summary>
    [Description("The message's payload, if any")]
    [DataMember(Order = 1, Name = "payload"), JsonPropertyOrder(1), JsonPropertyName("payload")]
    public object? Payload { get; init; }

    /// <summary>
    /// Gets/sets the message's headers, if any
    /// </summary>
    [Description("The message's headers, if any")]
    [DataMember(Order = 2, Name = "headers"), JsonPropertyOrder(2), JsonPropertyName("headers")]
    public object? Headers { get; init; }

}
