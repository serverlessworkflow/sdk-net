namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a loop that iterates over a range of values
/// </summary>
[Description("Represents the definition of a loop that iterates over a range of values")]
[DataContract]
public sealed record ForLoopDefinition
{

    /// <summary>
    /// Gets/sets the name of the variable that represents each element in the collection during iteration
    /// </summary>
    [Description("The name of the variable that represents each element in the collection during iteration")]
    [Required]
    [DataMember(Order = 1, Name = "each"), JsonPropertyOrder(1), JsonPropertyName("each")]
    public required string Each { get; init; }

    /// <summary>
    /// Gets/sets the runtime expression used to get the collection to iterate over
    /// </summary>
    [Description("The runtime expression used to get the collection to iterate over")]
    [DataMember(Order = 2, Name = "in"), JsonPropertyOrder(2), JsonPropertyName("in")]
    public required string In { get; init; }

    /// <summary>
    /// Gets/sets the name of the variable used to hold the index of each element in the collection during iteration
    /// </summary>
    [Description("The name of the variable used to hold the index of each element in the collection during iteration")]
    [DataMember(Order = 3, Name = "index"), JsonPropertyOrder(3), JsonPropertyName("index")]
    public string? At { get; init; }

    /// <summary>
    /// Gets/sets the definition of the data, if any, to pass to iterations to run
    /// </summary>
    [Description("The definition of the data, if any, to pass to iterations to run")]
    [DataMember(Order = 4, Name = "input"), JsonPropertyOrder(4), JsonPropertyName("input")]
    public InputDataModelDefinition? Input { get; init; }

}