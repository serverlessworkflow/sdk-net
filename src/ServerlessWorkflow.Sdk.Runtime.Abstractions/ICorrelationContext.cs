namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a correlation context
/// </summary>
public interface ICorrelationContext
{

    /// <summary>
    /// Gets the context's unique identifier
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the context's status
    /// </summary>
    string Status { get; }

    /// <summary>
    /// Gets a key/value mapping of the context's correlation keys
    /// </summary>
    EquatableDictionary<string, string> Keys { get; }

    /// <summary>
    /// Gets a key/value mapping of all correlated events, with the key being the index of the matched correlation filter
    /// </summary>
    EquatableDictionary<int, ICloudEvent> Events { get; }

    /// <summary>
    /// Gets the offset that serves as the index of the event being processed by the consumer, if streaming has been enabled for the correlation associated with the context.
    /// </summary>
    uint? Offset { get; init; }

}