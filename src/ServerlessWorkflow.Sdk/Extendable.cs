namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Represents the base class of extendable objects
/// </summary>
[Description("Represents the base class of extendable objects")]
[DataContract]
public abstract record Extendable
    : IExtendable
{

    /// <summary>
    /// Gets/sets a key/value mapping of the object's extension data, if any
    /// </summary>
    [Description("A key/value mapping of the object's extension data, if any")]
    [DataMember(Order = 100, Name = "extensions"), JsonPropertyOrder(100), JsonPropertyName("extensions")]
    public virtual JsonObject? Extensions { get; set; }

}