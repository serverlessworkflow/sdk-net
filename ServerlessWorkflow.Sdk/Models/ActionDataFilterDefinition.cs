namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the object used to configure how actions filter the state data for both input and output
/// </summary>
[DataContract]
public class ActionDataFilterDefinition
{

    /// <summary>
    /// Gets/sets an expression that filters state data that can be used by the action
    /// </summary>
    [DataMember(Order = 1, Name = "fromStateData"), JsonPropertyOrder(1), JsonPropertyName("fromStateData"), YamlMember(Alias = "fromStateData", Order = 1)]
    public virtual string? FromStateData { get; set; }

    /// <summary>
    /// Gets/sets an expression that filters the actions data results
    /// </summary>
    [DataMember(Order = 2, Name = "results"), JsonPropertyOrder(2), JsonPropertyName("results"), YamlMember(Alias = "results", Order = 2)]
    public virtual string? Results { get; set; }

    /// <summary>
    /// Gets/sets an expression that selects a state data element to which the action results should be added/merged into. If not specified denotes the top-level state data element
    /// </summary>
    [DataMember(Order = 3, Name = "toStateData"), JsonPropertyOrder(3), JsonPropertyName("toStateData"), YamlMember(Alias = "toStateData", Order = 3)]
    public virtual string? ToStateData { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to merge the action's data into state data.<para></para> If set to false, action data results are not added/merged to state data. In this case 'results' and 'toStateData' should be ignored. Defaults to true.
    /// </summary>
    [DataMember(Order = 4, Name = "useResults"), JsonPropertyOrder(4), JsonPropertyName("useResults"), YamlMember(Alias = "useResults", Order = 4)]
    [DefaultValue(true)]
    public virtual bool UseResults { get; set; } = true;

}