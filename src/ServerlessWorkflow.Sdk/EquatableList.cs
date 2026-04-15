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
/// Represents a list that can be compared for equality by comparing its items
/// </summary>
/// <typeparam name="T">The type of the items contained in the list</typeparam>
[CollectionDataContract]
public sealed class EquatableList<T>
    : List<T>, IEquatable<EquatableList<T>>
{

    /// <inheritdoc/>
    public EquatableList() : base() { }

    /// <inheritdoc/>
    public EquatableList(IEnumerable<T> collection) : base(collection) { }

    /// <inheritdoc/>
    public bool Equals(EquatableList<T>? other) => other is not null && this.SequenceEqual(other);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as EquatableList<T>);

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var item in this) hash.Add(item);
        return hash.ToHashCode();
    }
}
