namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a reference to a <see cref="FunctionDefinition"/>
/// </summary>
[DataContract]
public class FunctionReference
    : IExtensible
{

    /// <summary>
    /// Gets/sets the referenced function's name
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "refName", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("refName"), YamlMember(Alias = "refName", Order = 1)]
    public virtual string RefName { get; set; } = null!;

    /// <summary>
    /// Gets/sets a name/value mapping of the parameters to invoke the configured function with
    /// </summary>
    [DataMember(Order = 2, Name = "arguments"), JsonPropertyOrder(2), JsonPropertyName("arguments"), YamlMember(Alias = "arguments", Order = 2)]
    public virtual IDictionary<string, object>? Arguments { get; set; }

    /// <summary>
    /// Gets/sets a <see href="https://spec.graphql.org/June2018/#sec-Selection-Sets">GraphQL selection set</see>
    /// </summary>
    [DataMember(Order = 3, Name = "selectionSet"), JsonPropertyOrder(3), JsonPropertyName("selectionSet"), YamlMember(Alias = "selectionSet", Order = 3)]
    public virtual string? SelectionSet { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="ActionDefinition"/>'s extension properties
    /// </summary>
    [DataMember(Order = 4, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => this.RefName;

}