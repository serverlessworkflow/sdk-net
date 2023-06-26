namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to define events and their correlations
/// </summary>
[DataContract]
public class EventDefinition
    : IMetadata, IExtensible
{

    /// <summary>
    /// Gets/sets the Unique event name
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "name", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Alias = "name", Order = 1)]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets the cloud event source
    /// </summary>
    [DataMember(Order = 2, Name = "source"), JsonPropertyOrder(2), JsonPropertyName("source"), YamlMember(Alias = "source", Order = 2)]
    public virtual Uri? Source { get; set; }

    /// <summary>
    /// Gets/sets the cloud event type
    /// </summary>
    [DataMember(Order = 3, Name = "type"), JsonPropertyOrder(3), JsonPropertyName("type"), YamlMember(Alias = "type", Order = 3)]
    public virtual string? Type { get; set; } = null!;

    /// <summary>
    /// Gets/sets a value that defines the CloudEvent as either '<see cref="EventKind.Consumed"/>' or '<see cref="EventKind.Produced"/>' by the workflow. Default is '<see cref="EventKind.Consumed"/>'.
    /// </summary>
    [DefaultValue(EventKind.Consumed)]
    [DataMember(Order = 4, Name = "kind"), JsonPropertyOrder(4), JsonPropertyName("kind"), YamlMember(Alias = "kind", Order = 4)]
    public virtual string Kind { get; set; } = EventKind.Consumed;

    /// <summary>
    /// Gets/sets anlist containing the <see cref="EventCorrelationDefinition"/>s used to define the way the cloud event is correlated
    /// </summary>
    [DataMember(Order = 5, Name = "correlation"), JsonPropertyOrder(5), JsonPropertyName("correlation"), YamlMember(Alias = "correlation", Order = 5)]
    public virtual List<EventCorrelationDefinition>? Correlations { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to use the event's data only (thus making data the top level element, instead of including all context attributes at top level). Defaults to true.
    /// </summary>
    [DefaultValue(true)]
    [DataMember(Order = 6, Name = "dataOnly"), JsonPropertyOrder(6), JsonPropertyName("dataOnly"), YamlMember(Alias = "dataOnly", Order = 6)]

    public virtual bool DataOnly { get; set; } = true;

    /// <summary>
    /// Gets/sets the <see cref="EventDefinition"/>'s metadata
    /// </summary>
    [DataMember(Order = 7, Name = "metadata"), JsonPropertyOrder(7), JsonPropertyName("metadata"), YamlMember(Alias = "metadata", Order = 7)]
    public virtual DynamicMapping? Metadata { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="EventDefinition"/>'s extension properties
    /// </summary>
    [DataMember(Order = 8, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => this.Name;

}
