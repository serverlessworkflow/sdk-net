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

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Represents a record implementation of the <see cref="IDictionary{TKey, TValue}"/> interface, equatable based on its record keys and values
/// </summary>
/// <typeparam name="TKey">The type of keys contained by the dictionary</typeparam>
/// <typeparam name="TValue">The type of values contained by the dictionary</typeparam>
[CollectionDataContract]
public sealed record EquatableDictionary<TKey, TValue>
    : IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
    where TKey : notnull
{

    /// <summary>
    /// Initializes a new <see cref="EquatableDictionary{TKey, TValue}"/>
    /// </summary>
    public EquatableDictionary() : this(new Dictionary<TKey, TValue>()) { }

    /// <summary>
    /// Initializes a new <see cref="EquatableDictionary{TKey, TValue}"/>
    /// </summary>
    /// <param name="items">An <see cref="IEnumerable"/> containing the items the <see cref="EquatableDictionary{TKey, TValue}"/> is made out of</param>
    public EquatableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items) => this.Items = items.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    /// <summary>
    /// Gets the underlying <see cref="IDictionary"/>
    /// </summary>
    IDictionary<TKey, TValue> Items { get; init; }

    /// <inheritdoc/>
    public TValue this[TKey key] { get => this.Items[key]; set => this.Items[key] = value; }

    /// <inheritdoc/>
    public ICollection<TKey> Keys => this.Items.Keys;

    /// <inheritdoc/>
    public ICollection<TValue> Values => this.Items.Values;

    /// <inheritdoc/>
    public int Count => this.Items.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => this.Items.IsReadOnly;

    /// <inheritdoc/>
    public object? this[object key] { get => this[(TKey)key]; set => this[(TKey)key] = (TValue)value!; }

    /// <inheritdoc/>
    public void Add(TKey key, TValue value) => this.Items.Add(key, value);

    /// <inheritdoc/>
    public void Add(KeyValuePair<TKey, TValue> item) => this.Items.Add(item);

    /// <inheritdoc/>
    public bool Contains(KeyValuePair<TKey, TValue> item) => this.Items.Contains(item);

    /// <inheritdoc/>
    public bool ContainsKey(TKey key) => this.Items.ContainsKey(key);

    /// <inheritdoc/>
    public bool TryGetValue(TKey key, out TValue value) => this.Items.TryGetValue(key, out value);

    /// <inheritdoc/>
    public bool Remove(TKey key) => this.Items.Remove(key);

    /// <inheritdoc/>
    public bool Remove(KeyValuePair<TKey, TValue> item) => this.Items.Remove(item);

    /// <inheritdoc/>
    public void Clear() => this.Items.Clear();

    /// <inheritdoc/>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => this.Items.CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.Items.GetEnumerator();

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var someHashValue = -234897289;
        foreach (var item in this.Items)
        {
            someHashValue ^= item!.GetHashCode();
        }
        return someHashValue;
    }

    /// <inheritdoc/>
    public bool Equals(EquatableDictionary<TKey, TValue>? other)
    {
        if (other == null || other.Count != this.Count) return false;
        for (var i = 0; i < this.Count; i++)
        {
            var record = other.ElementAt(i);
            if (record.Equals(default) || !record.Equals(this.ElementAt(i))) return false;
        }
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

}
