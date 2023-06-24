namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object that can be of one of the specified types
/// </summary>
/// <typeparam name="T1">A first type alternative</typeparam>
/// <typeparam name="T2">A second type alternative</typeparam>
[DataContract]
public class OneOf<T1, T2>
    : IOneOf
{

    /// <summary>
    /// Initializes a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    public OneOf() { }

    /// <summary>
    /// Initializes a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <param name="value">The value of the <see cref="OneOf{T1, T2}"/></param>
    public OneOf(T1 value)
        : this()
    {
        this.T1Value = value;
    }

    /// <summary>
    /// Initializes a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <param name="value">The value of the <see cref="OneOf{T1, T2}"/></param>
    public OneOf(T2 value)
         : this()
    {
        this.T2Value = value;
    }

    /// <summary>
    /// Gets the first possible value
    /// </summary>
    [DataMember(Order = 1), JsonIgnore, YamlIgnore]
    public T1? T1Value { get; set; }

    /// <summary>
    /// Gets the second possible value
    /// </summary>
    [DataMember(Order = 2), JsonIgnore, YamlIgnore]
    public T2? T2Value { get; set; }

    /// <inheritdoc/>
    public virtual object? GetValue() => this.T1Value == null ? this.T2Value : this.T1Value;

    /// <summary>
    /// Implicitly convert the specified value into a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <param name="value">The value to convert</param>
    public static implicit operator OneOf<T1, T2>(T1 value) => new(value);

    /// <summary>
    /// Implicitly convert the specified value into a new <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <param name="value">The value to convert</param>
    public static implicit operator OneOf<T1, T2>(T2 value) => new(value);

    /// <summary>
    /// Implicitly convert the specified <see cref="OneOf{T1, T2}"/> into a new value
    /// </summary>
    /// <param name="value">The <see cref="OneOf{T1, T2}"/> to convert</param>
    public static implicit operator T1?(OneOf<T1, T2> value) => value.T1Value;

    /// <summary>
    /// Implicitly convert the specified <see cref="OneOf{T1, T2}"/> into a new value
    /// </summary>
    /// <param name="value">The <see cref="OneOf{T1, T2}"/> to convert</param>
    public static implicit operator T2?(OneOf<T1, T2> value) => value.T2Value;

}
