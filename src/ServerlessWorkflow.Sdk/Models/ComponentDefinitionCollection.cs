namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a collection of workflow components
/// </summary>
[Description("Represents a collection of workflow components.")]
[DataContract]
public sealed record ComponentDefinitionCollection
{

    /// <summary>
    /// Gets/sets a name/value mapping of the workflow's reusable authentication policies
    /// </summary>
    [Description("A name/value mapping of the workflow's reusable authentication policies.")]
    [DataMember(Order = 1, Name = "authentications"), JsonPropertyOrder(1), JsonPropertyName("authentications")]
    public EquatableDictionary<string, AuthenticationPolicyDefinition>? Authentications { get; init; }

    /// <summary>
    /// Gets/sets a name/value mapping of the catalogs, if any, from which to import reusable components used within the workflow
    /// </summary>
    [Description("A name/value mapping of the catalogs, if any, from which to import reusable components used within the workflow.")]
    [DataMember(Order = 2, Name = "catalogs"), JsonPropertyOrder(2), JsonPropertyName("catalogs")]
    public EquatableDictionary<string, CatalogDefinition>? Catalogs { get; init; }

    /// <summary>
    /// Gets/sets a name/value mapping of the workflow's errors, if any
    /// </summary>
    [Description("A name/value mapping of the workflow's errors, if any.")]
    [DataMember(Order = 3, Name = "errors"), JsonPropertyOrder(3), JsonPropertyName("errors")]
    public EquatableDictionary<string, ErrorDefinition>? Errors { get; init; }

    /// <summary>
    /// Gets/sets a name/value mapping of the workflow's extensions, if any
    /// </summary>
    [Description("A name/value mapping of the workflow's extensions, if any.")]
    [DataMember(Order = 4, Name = "extensions"), JsonPropertyOrder(4), JsonPropertyName("extensions")]
    public EquatableDictionary<string, ExtensionDefinition>? Extensions { get; init; }

    /// <summary>
    /// Gets/sets a name/value mapping of the workflow's reusable functions
    /// </summary>
    [Description("A name/value mapping of the workflow's reusable functions.")]
    [DataMember(Order = 5, Name = "functions"), JsonPropertyOrder(5), JsonPropertyName("functions")]
    public EquatableDictionary<string, TaskDefinition>? Functions { get; init; }

    /// <summary>
    /// Gets/sets a name/value mapping of the workflow's reusable retry policies
    /// </summary>
    [Description("A name/value mapping of the workflow's reusable retry policies.")]
    [DataMember(Order = 6, Name = "retries"), JsonPropertyOrder(6), JsonPropertyName("retries")]
    public EquatableDictionary<string, RetryPolicyDefinition>? Retries { get; init; }

    /// <summary>
    /// Gets/sets a list containing the workflow's secrets
    /// </summary>
    [Description("A list containing the workflow's secrets.")]
    [DataMember(Order = 7, Name = "secrets"), JsonPropertyOrder(7), JsonPropertyName("secrets")]
    public EquatableList<string>? Secrets { get; init; }

    /// <summary>
    /// Gets/sets a name/value mapping of the workflow's reusable timeouts
    /// </summary>
    [Description("A name/value mapping of the workflow's reusable timeouts.")]
    [DataMember(Order = 8, Name = "timeouts"), JsonPropertyOrder(8), JsonPropertyName("timeouts")]
    public EquatableDictionary<string, TimeoutDefinition>? Timeouts { get; init; }

}
