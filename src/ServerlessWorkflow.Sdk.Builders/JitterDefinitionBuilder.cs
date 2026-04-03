namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IJitterDefinitionBuilder"/> interface
/// </summary>
public sealed class JitterDefinitionBuilder(Duration? from = null, Duration? to = null)
    : IJitterDefinitionBuilder
{

    /// <summary>
    /// Gets the minimum duration of the jitter range
    /// </summary>
    protected Duration? JitterFrom { get; set; } = from;

    /// <summary>
    /// Gets the maximum duration of the jitter range
    /// </summary>
    protected Duration? JitterTo { get; set; } = to;

    /// <inheritdoc/>
    public IJitterDefinitionBuilder From(Duration from)
    {
        ArgumentNullException.ThrowIfNull(from);
        JitterFrom = from;
        IJitterDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IJitterDefinitionBuilder To(Duration to)
    {
        ArgumentNullException.ThrowIfNull(to);
        JitterTo = to;
        IJitterDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public JitterDefinition Build()
    {
        if (JitterFrom == null) throw new NullReferenceException("The jitter range's minimum duration must be set");
        if (JitterTo == null) throw new NullReferenceException("The jitter range's maximum duration must be set");
        return new()
        {
            From = JitterFrom,
            To = JitterTo,
        };
    }

}
