namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ISubscriptionIteratorDefinitionBuilder"/> interface
/// </summary>
public sealed class SubscriptionIteratorDefinitionBuilder
    : ISubscriptionIteratorDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the item variable name
    /// </summary>
    protected string? ItemValue { get; set; }

    /// <summary>
    /// Gets/sets the index variable name
    /// </summary>
    protected string? AtValue { get; set; }

    /// <summary>
    /// Gets/sets the tasks to execute
    /// </summary>
    protected Map<string, TaskDefinition>? DoTasks { get; set; }

    /// <summary>
    /// Gets/sets the output definition
    /// </summary>
    protected OutputDataModelDefinition? OutputValue { get; set; }

    /// <summary>
    /// Gets/sets the export definition
    /// </summary>
    protected OutputDataModelDefinition? ExportValue { get; set; }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Item(string item)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(item);
        ItemValue = item;
        ISubscriptionIteratorDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder At(string at)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(at);
        AtValue = at;
        ISubscriptionIteratorDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        DoTasks = builder.Build();
        ISubscriptionIteratorDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Output(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        OutputValue = builder.Build();
        ISubscriptionIteratorDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Export(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        ExportValue = builder.Build();
        ISubscriptionIteratorDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public SubscriptionIteratorDefinition Build() => new()
    {
        Item = ItemValue,
        At = AtValue,
        Do = DoTasks,
        Output = OutputValue,
        Export = ExportValue
    };

}
