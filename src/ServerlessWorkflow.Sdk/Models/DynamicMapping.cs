// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

using ServerlessWorkflow.Sdk.Serialization;
using System.Collections;
using System.Dynamic;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a dynamic dictionary
/// </summary>
[CollectionDataContract]
[JsonConverter(typeof(DynamicMappingConverter))]
public class DynamicMapping
    : DynamicObject, IDictionary<string, object>
{

    /// <summary>
    /// Initializes a new <see cref="DynamicMapping"/>
    /// </summary>
    public DynamicMapping() { }

    /// <summary>
    /// Initializes a new <see cref="DynamicMapping"/>
    /// </summary>
    /// <param name="properties">A name/value mapping of the <see cref="DynamicMapping"/>'s properties</param>
    public DynamicMapping(IDictionary<string, object> properties)
    {
        this.Properties = properties ?? throw new ArgumentNullException(nameof(properties));
    }

    /// <summary>
    /// Gets a name/value mapping of the <see cref="DynamicMapping"/>'s properties
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

    ICollection<string> IDictionary<string, object>.Keys => this.Properties.Keys;

    ICollection<object> IDictionary<string, object>.Values => this.Properties.Values;

    int ICollection<KeyValuePair<string, object>>.Count => this.Properties.Count;

    bool ICollection<KeyValuePair<string, object>>.IsReadOnly => this.Properties.IsReadOnly;

    /// <inheritdoc/>
    public object this[string key] { get => this.Properties[key]; set => this.Properties[key] = value; }

    /// <inheritdoc/>
    public override bool TryGetMember(GetMemberBinder binder, out object? result) => this.Properties.TryGetValue(binder.Name, out result);

    /// <inheritdoc/>
    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        this.Properties[binder.Name] = value!;
        return true;
    }

    /// <inheritdoc/>
    public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
    {
        if (indexes[0] is int index)
        {
            result = this.Properties.ElementAtOrDefault(index);
            return result != null;
        }
        else if (indexes[0] is string key) return this.Properties.TryGetValue(key, out result);
        else throw new NotSupportedException($"The specified index type '{indexes[0].GetType().Name}' is not supported");
    }

    /// <inheritdoc/>
    public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object? value)
    {
        if (indexes[0] is int index)
        {
            if (index >= this.Properties.Keys.Count) throw new IndexOutOfRangeException(nameof(index));
            var kvp = this.Properties.ElementAt(index);
            this.Properties[kvp.Key] = value!;
            return kvp.Key != default;
        }
        else if (indexes[0] is string key)
        {
            this.Properties[key] = value!;
            return true;
        }
        else throw new NotSupportedException($"The specified index type '{indexes[0].GetType().Name}' is not supported");
    }

    void IDictionary<string, object>.Add(string key, object value) => this.Properties.Add(key, value);

    bool IDictionary<string, object>.ContainsKey(string key) => this.Properties.ContainsKey(key);

    bool IDictionary<string, object>.Remove(string key) => this.Properties.Remove(key);

    bool IDictionary<string, object>.TryGetValue(string key, out object value) => this.Properties.TryGetValue(key, out value!);

    void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item) => this.Properties.Add(item);

    void ICollection<KeyValuePair<string, object>>.Clear() => this.Properties.Clear();

    bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item) => this.Properties.Contains(item);

    void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => ((IDictionary<string, object>)this.Properties).CopyTo(array, arrayIndex);

    bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item) => ((IDictionary<string, object>)this.Properties).Remove(item);

    IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() => this.Properties.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IDictionary<string, object>)this).GetEnumerator();

}
