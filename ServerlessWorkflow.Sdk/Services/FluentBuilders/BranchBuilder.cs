namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IBranchBuilder"/> interface
/// </summary>
public class BranchBuilder
    : IBranchBuilder
{

    /// <summary>
    /// Initializes a new <see cref="BranchBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="BranchBuilder"/> belongs to</param>
    public BranchBuilder(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="BranchBuilder"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; set; }

    /// <summary>
    /// Gets the <see cref="BranchDefinition"/> to configure
    /// </summary>
    protected BranchDefinition Branch { get; } = new BranchDefinition();

    /// <inheritdoc/>
    public virtual IBranchBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Branch.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IBranchBuilder WithExtensionProperty(string name, object value)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Branch.ExtensionData ??= new Dictionary<string, object>();
        this.Branch.ExtensionData[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IBranchBuilder WithExtensionProperties(IDictionary<string, object> properties)
    {
        this.Branch.ExtensionData = properties ?? throw new ArgumentNullException(nameof(properties));
        return this;
    }

    /// <inheritdoc/>
    public virtual IBranchBuilder Execute(ActionDefinition action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));
        this.Branch.Actions.Add(action);
        return this;
    }

    /// <inheritdoc/>
    public virtual IBranchBuilder Execute(Action<IActionBuilder> actionSetup)
    {
        if (actionSetup == null) throw new ArgumentNullException(nameof(actionSetup));
        var actionBuilder = new ActionBuilder(this.Pipeline);
        actionSetup(actionBuilder);
        this.Branch.Actions.Add(actionBuilder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IBranchBuilder Execute(string name, Action<IActionBuilder> actionSetup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (actionSetup == null) throw new ArgumentNullException(nameof(actionSetup));
        return this.Execute(a =>
        {
            actionSetup(a);
            a.WithName(name);
        });
    }

    /// <inheritdoc/>
    public virtual IBranchBuilder Concurrently()
    {
        this.Branch.ActionMode = ActionExecutionMode.Parallel;
        return this;
    }

    /// <inheritdoc/>
    public virtual IBranchBuilder Sequentially()
    {
        this.Branch.ActionMode = ActionExecutionMode.Sequential;
        return this;
    }

    /// <inheritdoc/>
    public virtual BranchDefinition Build() => this.Branch;

}
