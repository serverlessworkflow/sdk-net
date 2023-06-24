namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to define the way to correlate a cloud event
/// </summary>
[DataContract]
public class EventCorrelationDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the cloud event Extension Context Attribute name
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "contextAttributeName", IsRequired = true), JsonPropertyName("contextAttributeName"), YamlMember(Alias = "contextAttributeName")]
    public virtual string ContextAttributeName { get; set; } = null!;

    /// <summary>
    /// Gets/sets the cloud event Extension Context Attribute value
    /// </summary>
    [DataMember(Order = 2, Name = "contextAttributeValue", IsRequired = true), JsonPropertyName("contextAttributeValue"), YamlMember(Alias = "contextAttributeValue")]
    public virtual string? ContextAttributeValue { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="EventCorrelationDefinition"/>'s extension properties
    /// </summary>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

}