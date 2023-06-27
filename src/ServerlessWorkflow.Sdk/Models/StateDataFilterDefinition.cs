namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a state's data filter
/// </summary>
[DataContract]
public class StateDataFilterDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets an expression to filter the states data input
    /// </summary>
    [DataMember(Order = 1, Name = "input"), JsonPropertyName("input"), YamlMember(Alias = "input")]
    public virtual string? Input { get; set; }

    /// <summary>
    /// Gets/sets an expression that filters the states data output
    /// </summary>
    [DataMember(Order = 2), JsonPropertyName("output"), YamlMember(Alias = "output")]
    public virtual string? Output { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

}