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
