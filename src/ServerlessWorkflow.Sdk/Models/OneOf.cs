namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a value that can be one of two possible types.
/// </summary>
/// <typeparam name="T1">The first possible type.</typeparam>
/// <typeparam name="T2">The second possible type.</typeparam>
public sealed record OneOf<T1, T2>
{

    readonly byte tag;
    readonly T1? t1;
    readonly T2? t2;

    /// <summary>
    /// Initializes a new <see cref="OneOf{T1, T2}"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public OneOf(T1 value)
    {
        tag = 1;
        t1 = value;
        t2 = default;
    }

    /// <summary>
    /// Initializes a new <see cref="OneOf{T1, T2}"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public OneOf(T2 value)
    {
        tag = 2;
        t1 = default;
        t2 = value;
    }

    /// <summary>
    /// Attempts to get the value as <typeparamref name="T1"/>.
    /// </summary>
    /// <param name="value">The output value.</param>
    /// <returns>A boolean indicating whether the value was of type <typeparamref name="T1"/>.</returns>
    public bool TryGetAsT1(out T1? value)
    {
        value = t1;
        if (tag is not 1) return false;
        return true;
    }

    /// <summary>
    /// Attempts to get the value as <typeparamref name="T2"/>.
    /// </summary>
    /// <param name="value">The output value.</param>
    /// <returns>A boolean indicating whether the value was of type <typeparamref name="T2"/>.</returns>
    public bool TryGetAsT2(out T2? value)
    {
        value = t2;
        if (tag is not 2) return false;

        return true;
    }

    /// <summary>
    /// Matches the value and invokes the corresponding function.
    /// </summary>
    /// <typeparam name="T">The return type of the functions.</typeparam>
    /// <param name="f1">The function to invoke if the value is of type <typeparamref name="T1"/>.</param>
    /// <param name="f2">The function to invoke if the value is of type <typeparamref name="T2"/>.</param>
    /// <returns>TA value of type <typeparamref name="T"/>.</returns>
    public T Match<T>(Func<T1, T> f1, Func<T2, T> f2) => tag switch
    {
        1 => f1(t1!),
        2 => f2(t2!),
        _ => throw new InvalidOperationException("Invalid OneOf state."),
    };

    /// <summary>
    /// Matches the value and invokes the corresponding function.
    /// </summary>
    /// <typeparam name="T">The return type of the functions.</typeparam>
    /// <param name="f1">The function to invoke if the value is of type <typeparamref name="T1"/>.</param>
    /// <param name="f2">The function to invoke if the value is of type <typeparamref name="T2"/>.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>TA value of type <typeparamref name="T"/>.</returns>
    public async Task<T> MatchAsync<T>(Func<T1, CancellationToken, Task<T>> f1, Func<T2, CancellationToken, Task<T>> f2, CancellationToken cancellationToken = default) => tag switch
    {
        1 => await f1(t1!, cancellationToken),
        2 => await f2(t2!, cancellationToken),
        _ => throw new InvalidOperationException("Invalid OneOf state."),
    };

    /// <summary>
    /// Switches the value and invokes the corresponding action.
    /// </summary>
    /// <param name="a1">The action to invoke if the value is of type <typeparamref name="T1"/>.</param>
    /// <param name="a2">The action to invoke if the value is of type <typeparamref name="T2"/>.</param>
    public void Switch(Action<T1> a1, Action<T2> a2)
    {
        switch (tag)
        {
            case 1:
                a1(t1!);
                break;
            case 2:
                a2(t2!);
                break;
            default:
                throw new InvalidOperationException("Invalid OneOf state.");
        }
    }

    /// <summary>
    /// Implicitly converts a <typeparamref name="T1"/> to a <see cref="OneOf{T1, T2}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator OneOf<T1, T2>(T1 value) => new(value);

    /// <summary>
    /// Implicitly converts a <typeparamref name="T2"/> to a <see cref="OneOf{T1, T2}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator OneOf<T1, T2>(T2 value) => new(value);

}
