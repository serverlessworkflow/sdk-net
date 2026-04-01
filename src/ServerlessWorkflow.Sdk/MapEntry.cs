namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Represents a map entry
/// </summary>
/// <typeparam name="TKey">The type of the entry's key</typeparam>
/// <typeparam name="TValue">The type of the entry's value</typeparam>
[JsonConverter(typeof(MapEntryJsonConverterFactory))]
public sealed record MapEntry<TKey, TValue>
{

    /// <summary>
    /// Initializes a new <see cref="MapEntry{TKey, TValue}"/>
    /// </summary>
    public MapEntry() { }

    /// <summary>
    /// Initializes a new <see cref="MapEntry{TKey, TValue}"/>
    /// </summary>
    /// <param name="key">The entry key</param>
    /// <param name="value">The entry value</param>
    public MapEntry(TKey key, TValue value)
    {
        this.Key = key;
        this.Value = value;
    }

    /// <summary>
    /// Gets/sets the entry key
    /// </summary>
    public TKey Key { get; init; } = default!;

    /// <summary>
    /// Gets/sets the entry value
    /// </summary>
    public TValue Value { get; init; } = default!;

}