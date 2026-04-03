namespace ServerlessWorkflow.Sdk.Models.Processes;

/// <summary>
/// Represents the definition of a shell process
/// </summary>
[Description("Represents the definition of a shell process")]
[DataContract]
public sealed record ShellProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the shell command to run
    /// </summary>
    [Description("The shell command to run")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "command"), JsonPropertyOrder(1), JsonPropertyName("command")]
    public required string Command { get; init; }

    /// <summary>
    /// Gets/sets the arguments of the shell command to run
    /// </summary>
    [Description("The arguments of the shell command to run")]
    [DataMember(Order = 2, Name = "arguments"), JsonPropertyOrder(2), JsonPropertyName("arguments")]
    public EquatableList<string>? Arguments { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the environment variables, if any, to use when running the configured process
    /// </summary>
    [Description("A key/value mapping of the environment variables, if any, to use when running the configured process")]
    [DataMember(Order = 3, Name = "environment"), JsonPropertyOrder(3), JsonPropertyName("environment")]
    public EquatableDictionary<string, string>? Environment { get; init; }

}