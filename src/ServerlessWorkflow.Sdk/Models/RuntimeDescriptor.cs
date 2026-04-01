namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an runtime expression argument used to describe the current runtime
/// </summary>
[Description("Represents an runtime expression argument used to describe the current runtime.")]
[DataContract]
public sealed record RuntimeDescriptor
{

    /// <summary>
    /// Gets/sets the runtime's name
    /// </summary>
    [Description("The runtime's name.")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets/sets the runtime's version
    /// </summary>
    [Description("The runtime's version.")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1), SemanticVersion]
    [DataMember(Order = 2, Name = "version"), JsonPropertyOrder(2), JsonPropertyName("version")]
    public required string Version { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the runtime's metadata, if any
    /// </summary>
    [Description("A key/value mapping of the runtime's metadata, if any.")]
    [DataMember(Order = 3, Name = "metadata"), JsonPropertyOrder(3), JsonPropertyName("metadata")]
    public JsonObject? Metadata { get; init; }

}
