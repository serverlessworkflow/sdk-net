namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a data-based <see cref="SwitchCaseDefinition"/>
/// </summary>
[DataContract]
public class DataCaseDefinition
    : SwitchCaseDefinition
{

    /// <summary>
    /// Gets/sets an expression evaluated against state data. True if results are not empty
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "condition", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("condition"), YamlMember(Alias = "condition", Order = 1)]
    public virtual string Condition { get; set; } = null!;

}
