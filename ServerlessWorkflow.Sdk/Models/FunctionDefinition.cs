namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to define a reusable function
/// </summary>
[DataContract]
public class FunctionDefinition
    : IMetadata, IExtensible
{

    /// <summary>
    /// Gets/sets a unique function name
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "name", IsRequired = true), JsonPropertyName("name"), YamlMember(Alias = "name")]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets the operation. If type '<see cref="FunctionType.Rest"/>', combination of the function/service OpenAPI definition URI and the operationID of the operation that needs to be invoked, separated by a '#'. 
    /// If type is `<see cref="FunctionType.Expression"/>` defines the workflow expression.
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 2, Name = "operation", IsRequired = true), JsonPropertyName("operation"), YamlMember(Alias = "operation")]
    public virtual string Operation { get; set; } = null!;

    /// <summary>
    /// Gets/sets the type of the defined function. Defaults to '<see cref="FunctionType.Rest"/>'
    /// </summary>
    [DataMember(Order = 3, Name = "type", IsRequired = true), JsonPropertyName("type"), YamlMember(Alias = "type")]
    public virtual string Type { get; set; } = FunctionType.Rest;

    /// <summary>
    /// Gets/sets the reference to the authentication definition to use when invoking the function. Ignored when <see cref="Type"/> has been set to <see cref="FunctionType.Expression"/>
    /// </summary>
    [DataMember(Order = 4, Name = "authRef", IsRequired = true), JsonPropertyName("authRef"), YamlMember(Alias = "authRef")]
    public virtual string? AuthRef { get; set; }

    /// <summary>
    /// Gets/sets the function's metadata
    /// </summary>
    [DataMember(Order = 5, Name = "metadata", IsRequired = true), JsonPropertyName("metadata"), YamlMember(Alias = "metadata")]
    public virtual IDictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="FunctionDefinition"/>'s extension properties
    /// </summary>
    [DataMember(Order = 6, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => this.Name;

}
