namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a <see href="https://github.com/serverlessworkflow/specification/blob/main/specification.md#extensions">Serverless Workflow extension</see>
/// </summary>
[DataContract]
public class ExtensionDefinition
    : IExtensible
{

    /// <summary>
    /// Initializes a new <see cref="ExtensionDefinition"/>
    /// </summary>
    public ExtensionDefinition() { }

    /// <summary>
    /// Initializes a new <see cref="ExtensionDefinition"/>
    /// </summary>
    /// <param name="extensionId">The id that uniquely identifies the extension to import</param>
    /// <param name="resource">The uri that references the extension resource</param>
    public ExtensionDefinition(string extensionId, Uri resource)
    {
        if(string.IsNullOrWhiteSpace(extensionId)) throw new ArgumentNullException(nameof(extensionId));
        this.ExtensionId = extensionId;
        this.Resource = resource ?? throw new ArgumentNullException(nameof(resource));
    }

    /// <summary>
    /// Gets/sets the extension's unique id
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "extensionId", IsRequired = true), JsonPropertyName("extensionsId"), YamlMember(Alias = "extensionId")]
    public virtual string ExtensionId { get; set; } = null!;

    /// <summary>
    /// Gets/sets an uri to a resource containing the workflow extension definition (json or yaml)
    /// </summary>
    [DataMember(Order = 2, Name = "resource", IsRequired = true), JsonPropertyName("resource"), YamlMember(Alias = "resource")]
    public virtual Uri Resource { get; set; } = null!;

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

}
