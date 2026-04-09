namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a reference to a <see cref="WorkflowDefinition"/>
/// </summary>
[Description("Represents a reference to a workflow definition.")]
[DataContract]
public sealed record WorkflowDefinitionReference
{

    /// <summary>
    /// Gets/sets the name of the referenced workflow definition
    /// </summary>
    [Description("The name of the referenced workflow definition.")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Gets/sets the namespace of the referenced workflow definition
    /// </summary>
    [Description("The namespace of the referenced workflow definition.")]
    [Required, MinLength(1)]
    [DataMember(Order = 2, Name = "namespace"), JsonPropertyOrder(2), JsonPropertyName("namespace")]
    public required string Namespace { get; set; }

    /// <summary>
    /// Gets/sets the semantic version of the referenced workflow definition
    /// </summary>
    [Description("The semantic version of the referenced workflow definition.")]
    [Required, MinLength(1)]
    [DataMember(Order = 3, Name = "version"), JsonPropertyOrder(3), JsonPropertyName("version")]
    public required string Version { get; set; }

    /// <inheritdoc/>
    public override string ToString() => $"{this.Name}.{this.Namespace}:{this.Version}";

    /// <summary>
    /// Parses the specified input into a new <see cref="WorkflowDefinitionReference"/>
    /// </summary>
    /// <param name="input">The input to parse</param>
    /// <returns>The parsed <see cref="WorkflowDefinitionReference"/></returns>
    public static WorkflowDefinitionReference Parse(string input)
    {
        if(string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));
        var components = input.Trim().Split(':');
        var qualifiedName = components[0];
        var version = components[1];
        components = qualifiedName.Split('.');
        var @namespace = components[0];
        var name = components[1];
        return new()
        {
            Name = name,
            Namespace = @namespace,
            Version = version
        };
    }

    /// <summary>
    /// Attempts to parse the specified input into a new <see cref="WorkflowDefinitionReference"/>
    /// </summary>
    /// <param name="input">The input to parse</param>
    /// <param name="reference">The The parsed <see cref="WorkflowDefinitionReference"/>, if any</param>
    /// <returns>A boolean indicating whether or not the specified input could be parsed into a new <see cref="WorkflowDefinitionReference"/></returns>
    public static bool TryParse(string input, out WorkflowDefinitionReference? reference)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));
        reference = null;
        try
        {
            reference = Parse(input);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Implicitly converts the specified reference into string
    /// </summary>
    /// <param name="reference">The reference to convert</param>
    public static implicit operator string(WorkflowDefinitionReference reference) => reference.ToString();

    /// <summary>
    /// Implicitly parses the specified string into a new reference 
    /// </summary>
    /// <param name="reference">The string to parse</param>
    public static implicit operator WorkflowDefinitionReference(string reference) => Parse(reference);

}