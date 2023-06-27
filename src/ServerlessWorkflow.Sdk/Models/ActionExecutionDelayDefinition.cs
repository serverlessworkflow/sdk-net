namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure an <see cref="ActionDefinition"/>'s execution delay
/// </summary>
[DataContract]
public class ActionExecutionDelayDefinition
{

    /// <summary>
    /// Gets/sets the amount of time to wait before executing the configured <see cref="ActionDefinition"/>
    /// </summary>
    [DataMember(Order = 1, Name = "before"), JsonPropertyOrder(1), JsonPropertyName("before"), YamlMember(Alias = "before", Order = 1)]
    public virtual TimeSpan? Before { get; set; }

    /// <summary>
    /// Gets/sets the amount of time to wait after having executed the configured <see cref="ActionDefinition"/>
    /// </summary>
    [DataMember(Order = 2, Name = "after"), JsonPropertyOrder(2), JsonPropertyName("after"), YamlMember(Alias = "after", Order = 2)]
    public virtual TimeSpan? After { get; set; }

}