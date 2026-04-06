namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IJitterDefinitionBuilder"/> interface
/// </summary>
public sealed class JitterDefinitionBuilder(Duration? from = null, Duration? to = null)
    : IJitterDefinitionBuilder
{

    Duration? jitterFrom = from;
    Duration? jitterTo = to;

    /// <inheritdoc/>
    public IJitterDefinitionBuilder From(Duration from)
    {
        ArgumentNullException.ThrowIfNull(from);
        jitterFrom = from;
        return this;
    }

    /// <inheritdoc/>
    public IJitterDefinitionBuilder To(Duration to)
    {
        ArgumentNullException.ThrowIfNull(to);
        jitterTo = to;
        return this;
    }

    /// <inheritdoc/>
    public JitterDefinition Build()
    {
        if (jitterFrom == null) throw new NullReferenceException("The jitter range's minimum duration must be set");
        if (jitterTo == null) throw new NullReferenceException("The jitter range's maximum duration must be set");
        return new()
        {
            From = jitterFrom,
            To = jitterTo,
        };
    }

}
