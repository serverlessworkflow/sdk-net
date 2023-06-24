namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a CRON expression definition
/// </summary>
[DataContract]
public class CronDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the repeating interval (cron expression) describing when the workflow instance should be created
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "expression", IsRequired = true), JsonPropertyName("expression"), YamlMember(Alias = "expression")]
    public virtual string Expression { get; set; } = null!;

    /// <summary>
    /// Gets/sets the date and time when the cron expression invocation is no longer valid
    /// </summary>
    [DataMember(Order = 2, Name = "validUntil", IsRequired = true), JsonPropertyName("validUntil"), YamlMember(Alias = "validUntil")]
    public virtual DateTime? ValidUntil { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

}
