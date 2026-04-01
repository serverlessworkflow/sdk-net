namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a case within a switch task, defining a condition and corresponding tasks to execute if the condition is met
/// </summary>
[Description("Represents the definition of a case within a switch task, defining a condition and corresponding tasks to execute if the condition is met")]
[DataContract]
public sealed record SwitchCaseDefinition
{

    /// <summary>
    /// Gets/sets the condition that determines whether or not the case should be executed in a switch task
    /// </summary>
    [Description("The condition that determines whether or not the case should be executed in a switch task")]
    [DataMember(Order = 1, Name = "when"), JsonPropertyOrder(1), JsonPropertyName("when")]
    public string? When { get; init; }

    /// <summary>
    /// Gets/sets the transition to perform when the case matches
    /// </summary>
    [Description("The transition to perform when the case matches")]
    [DataMember(Order = 2, Name = "then"), JsonPropertyOrder(2), JsonPropertyName("then")]
    public string? Then { get; init; }

}