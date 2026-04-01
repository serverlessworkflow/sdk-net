namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an exponential backoff
/// </summary>
[Description("Represents the definition of an exponential backoff")]
[DataContract]
public sealed record ExponentialBackoffDefinition
    : BackoffDefinition
{



}
