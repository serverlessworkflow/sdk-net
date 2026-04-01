using Json.Schema;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Represents an ordered key/value map
/// </summary>
/// <typeparam name="TKey">The type of keys to use</typeparam>
/// <typeparam name="TValue">The type of values to use</typeparam>
[CollectionDataContract]
public sealed record Map<TKey, TValue>
    : ICollection<MapEntry<TKey, TValue>>
    where TKey : notnull
{

    readonly Dictionary<TKey, TValue> _entries = [];

    /// <summary>
    /// Gets an <see cref="IReadOnlyList{T}"/> that contains all the map's keys
    /// </summary>
    public IReadOnlyList<TKey> Keys => [.. this._entries.Keys];

    /// <summary>
    /// Gets an <see cref="IReadOnlyList{T}"/> that contains all the map's values
    /// </summary>
    public IReadOnlyList<TValue> Values => [.. this._entries.Values];

    /// <inheritdoc/>
    public int Count => this._entries.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => ((IDictionary<TKey, TValue>)this._entries).IsReadOnly;

    /// <summary>
    /// Gets/sets the value with the specified key
    /// </summary>
    /// <param name="key">Tje key of the value to set</param>
    /// <returns>The value at the specified key</returns>
    public TValue this[TKey key]
    {
        get
        {
            if (!_entries.TryGetValue(key, out TValue? value)) throw new KeyNotFoundException($"The key '{key}' was not found in the map.");
            return value;
        }
        set
        {
            if (!_entries.TryAdd(key, value)) this._entries[key] = value;
        }
    }

    /// <summary>
    /// Gets the <see cref="MapEntry{TKey, TValue}"/> with the specified key
    /// </summary>
    /// <param name="key">The key of the <see cref="MapEntry{TKey, TValue}"/> to get</param>
    /// <returns>The <see cref="MapEntry{TKey, TValue}"/> with the specified key</returns>
    public MapEntry<TKey, TValue>? GetEntry(TKey key)
    {
        var kvp = this._entries.FirstOrDefault(e => e.Key.Equals(key));
        if (kvp.Key.Equals(default(TKey))) return null;
        else return new(kvp.Key, kvp.Value);
    }

    /// <inheritdoc/>
    public void Add(MapEntry<TKey, TValue> item) => this._entries[item.Key] = item.Value;

    /// <inheritdoc/>
    public void Clear() => this._entries.Clear();

    /// <inheritdoc/>
    public bool Contains(MapEntry<TKey, TValue> item) => this._entries.ContainsKey(item.Key);

    /// <inheritdoc/>
    public void CopyTo(MapEntry<TKey, TValue>[] array, int arrayIndex)
    {
        if (array is null) throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex), "arrayIndex must be a non-negative integer.");
        if (arrayIndex + this.Count > array.Length) throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
        foreach (var entry in this) array[arrayIndex++] = entry;
    }

    /// <inheritdoc/>
    public bool Remove(MapEntry<TKey, TValue> item) => this._entries.Remove(item.Key);

    /// <summary>
    /// Attempts to get the value with the specified key
    /// </summary>
    /// <param name="key">The kye of the value to get</param>
    /// <param name="value">The value at the specified key, if any</param>
    /// <returns>A boolean indicating whether or not the map contains the specified key</returns>
    public bool TryGetValue(TKey key, out TValue? value) => this._entries.TryGetValue(key, out value);

    /// <inheritdoc/>
    public IEnumerator<MapEntry<TKey, TValue>> GetEnumerator()
    {
        foreach (var kvp in this._entries) yield return new(kvp.Key, kvp.Value);
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

}