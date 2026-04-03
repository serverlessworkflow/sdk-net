using System.Collections;
using System.Collections.Concurrent;

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Represents a concurrent <see cref="HashSet{T}"/>
/// </summary>
/// <typeparam name="T">The type of value contained by the <see cref="ConcurrentHashSet{T}"/></typeparam>
public sealed class ConcurrentHashSet<T>
    : ICollection<T>
    where T : notnull
{

    readonly ConcurrentDictionary<T, bool> _dictionary = new();

    /// <inheritdoc/>
    public int Count => _dictionary.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public bool Contains(T item) => _dictionary.ContainsKey(item);

    /// <inheritdoc/>
    public void Add(T item)
    {
        if (!_dictionary.TryAdd(item, false)) throw new InvalidOperationException("The specified item already exists in the collection");
    }

    /// <summary>
    /// Attempts to add the specified item to the hashset
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <returns>A boolean indicating whether or not the item has been added or if it was already present in the set</returns>
    public bool TryAdd(T item) => _dictionary.TryAdd(item, false);

    /// <inheritdoc/>
    public bool Remove(T item) => _dictionary.Remove(item, out _);

    /// <inheritdoc/>
    public void Clear() => _dictionary.Clear();

    /// <inheritdoc/>
    public void CopyTo(T[] array, int arrayIndex) => _dictionary.Keys.CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator() => _dictionary.Keys.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
