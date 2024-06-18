// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Represents an ordered key/value map
/// </summary>
/// <typeparam name="TKey">The type of keys to use</typeparam>
/// <typeparam name="TValue">The type of values to use</typeparam>
public record Map<TKey, TValue>
    : ICollection<MapEntry<TKey, TValue>>
    where TKey : notnull
{

    readonly Dictionary<TKey, TValue> _entries = [];

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

    /// <inheritdoc/>
    public virtual void Add(MapEntry<TKey, TValue> item) => this._entries[item.Key] = item.Value;

    /// <inheritdoc/>
    public virtual void Clear() => this._entries.Clear();

    /// <inheritdoc/>
    public virtual bool Contains(MapEntry<TKey, TValue> item) => this._entries.ContainsKey(item.Key);

    /// <inheritdoc/>
    public virtual void CopyTo(MapEntry<TKey, TValue>[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentOutOfRangeException.ThrowIfLessThan(arrayIndex, 0);
        if (arrayIndex + this.Count > array.Length) throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
        foreach (var entry in this)  array[arrayIndex++] = entry;
    }

    /// <inheritdoc/>
    public virtual bool Remove(MapEntry<TKey, TValue> item) => this._entries.Remove(item.Key);

    /// <summary>
    /// Attempts to get the value with the specified key
    /// </summary>
    /// <param name="key">The kye of the value to get</param>
    /// <param name="value">The value at the specified key, if any</param>
    /// <returns>A boolean indicating whether or not the map contains the specified key</returns>
    public virtual bool TryGetValue(TKey key, out TValue? value) => this._entries.TryGetValue(key, out value);

    /// <inheritdoc/>
    public virtual IEnumerator<MapEntry<TKey, TValue>> GetEnumerator()
    {
        foreach (var kvp in this._entries) yield return new(kvp.Key, kvp.Value);
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

}
