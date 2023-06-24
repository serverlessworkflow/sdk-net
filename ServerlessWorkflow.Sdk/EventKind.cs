namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all kinds of workflow events
/// </summary>
public static class EventKind
{

    /// <summary>
    /// Indicates an event to consume
    /// </summary>
    public const string Consumed = "consumed";

    /// <summary>
    /// Indicates an event to produce
    /// </summary>
    public const string Produced = "produced";

}
