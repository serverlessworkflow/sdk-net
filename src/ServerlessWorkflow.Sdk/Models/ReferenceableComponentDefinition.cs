namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the base class for all ServerlessWorkflow referenceable workflow components
/// </summary>
[Description("Represents the base class for all ServerlessWorkflow referenceable workflow components")]
[DataContract]
public abstract record ReferenceableComponentDefinition
    : ComponentDefinition, IReferenceable
{

    /// <summary>
    /// Gets/sets an URI, if any, that reference the component's definition
    /// </summary>
    [Description("An URI, if any, that reference the component's definition")]
    [DataMember(Order = 1, Name = "ref"), JsonPropertyOrder(1), JsonPropertyName("ref")]
    public Uri? Ref { get; init; }

}