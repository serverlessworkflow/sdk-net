namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IListenerDefinitionBuilder"/> interface
/// </summary>
public sealed class ListenerDefinitionBuilder(EventConsumptionStrategyDefinition? to = null)
    : ListenerTargetDefinitionBuilder, IListenerDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the initial target value
    /// </summary>
    readonly EventConsumptionStrategyDefinition? initialTo = to;

    /// <summary>
    /// Gets/sets the read mode
    /// </summary>
    string? readMode;

    /// <inheritdoc/>
    public IListenerDefinitionBuilder Read(string readMode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(readMode);
        this.readMode = readMode;
        IListenerDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public new ListenerDefinition Build()
    {
        var target = initialTo ?? base.Build() ?? throw new NullReferenceException("The listener's target must be set");
        return new()
        {
            To = target,
            Read = readMode
        };
    }

}
