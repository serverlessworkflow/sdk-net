namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure the lifetime of a container
/// </summary>
[Description("Represents an object used to configure the lifetime of a container")]
[DataContract]
public sealed record ContainerLifetimeDefinition
{

    /// <summary>
    /// Gets/sets the cleanup policy to use.<para></para>
    /// See <see cref="ContainerCleanupPolicy"/><para></para>
    /// Defaults to <see cref="ContainerCleanupPolicy.Never"/>
    /// </summary>
    [Description("The cleanup policy to use. See ContainerCleanupPolicy. Defaults to ContainerCleanupPolicy.Never")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "cleanup"), JsonPropertyOrder(1), JsonPropertyName("cleanup")]
    public required string Cleanup { get; init; }

    /// <summary>
    /// Gets/sets the duration, if any, after which to delete the container once executed.<para></para>
    /// Required if <see cref="Cleanup"/> has been set to <see cref="ContainerCleanupPolicy.Eventually"/>, otherwise ignored.
    /// </summary>
    [Description("The duration, if any, after which to delete the container once executed. Required if Cleanup has been set to ContainerCleanupPolicy.Eventually, otherwise ignored.")]
    [DataMember(Order = 2, Name = "duration"), JsonPropertyOrder(2), JsonPropertyName("duration")]
    public Duration? Duration { get; init; }
}
