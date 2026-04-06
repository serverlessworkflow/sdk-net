namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ISubscriptionIteratorDefinitionBuilder"/> interface
/// </summary>
public sealed class SubscriptionIteratorDefinitionBuilder
    : ISubscriptionIteratorDefinitionBuilder
{

    string? itemValue;
    string? atValue;
    Map<string, TaskDefinition>? doTasks;
    OutputDataModelDefinition? outputValue;
    OutputDataModelDefinition? exportValue;

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Item(string item)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(item);
        itemValue = item;
        return this;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder At(string at)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(at);
        atValue = at;
        return this;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        doTasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Output(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        outputValue = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Export(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        exportValue = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public SubscriptionIteratorDefinition Build() => new()
    {
        Item = itemValue,
        At = atValue,
        Do = doTasks,
        Output = outputValue,
        Export = exportValue
    };

}
