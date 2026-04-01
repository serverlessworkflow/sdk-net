namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskLifeCycleEvent"/>
/// </summary>
/// <param name="Type">The <see cref="ITaskLifeCycleEvent"/>'s type</param>
/// <param name="Data">The <see cref="ITaskLifeCycleEvent"/>'s data, if any</param>
[Description("Represents the default implementation of the ITaskLifeCycleEvent")]
[DataContract]
public sealed record TaskLifeCycleEvent(string Type, JsonObject? Data = null)
    : ITaskLifeCycleEvent
{

    /// <inheritdoc/>
    [Description("The task life cycle event's type")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "type"), JsonPropertyOrder(1), JsonPropertyName("type")]
    public string Type { get; init; } = Type;

    /// <inheritdoc/>
    [Description("The task life cycle event's data, if any")]
    [DataMember(Order = 2, Name = "data"), JsonPropertyOrder(2), JsonPropertyName("data")]
    public JsonObject? Data { get; init; } = Data;

}
