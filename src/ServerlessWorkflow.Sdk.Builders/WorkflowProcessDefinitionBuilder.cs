namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowProcessDefinitionBuilder"/> interface
/// </summary>
public sealed partial class WorkflowProcessDefinitionBuilder
    : ProcessDefinitionBuilder<WorkflowProcessDefinition>, IWorkflowProcessDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the namespace of the workflow to run
    /// </summary>
    protected string? Namespace { get; set; }

    /// <summary>
    /// Gets/sets the name of the workflow to run
    /// </summary>
    protected string? Name { get; set; }

    /// <summary>
    /// Gets/sets the version of the workflow to run. Defaults to 'latest'
    /// </summary>
    protected string Version { get; set; } = "latest";

    /// <summary>
    /// Gets/sets the data, if any, to pass as input to the workflow to execute
    /// </summary>
    protected JsonObject? Input { get; set; }

    /// <inheritdoc/>
    public IWorkflowProcessDefinitionBuilder WithNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        if (!DnsLabelRegex().IsMatch(@namespace)) throw new ArgumentException($"The specified value '{@namespace}' is not a valid RFC1123 DNS label name", nameof(@namespace));
        Namespace = @namespace;
        IWorkflowProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IWorkflowProcessDefinitionBuilder WithName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (!DnsLabelRegex().IsMatch(name)) throw new ArgumentException($"The specified value '{name}' is not a valid RFC1123 DNS label name", nameof(name));
        Name = name;
        IWorkflowProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IWorkflowProcessDefinitionBuilder WithVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        Version = version;
        IWorkflowProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IWorkflowProcessDefinitionBuilder WithInput(object input)
    {
        Input = input;
        IWorkflowProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public override WorkflowProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new NullReferenceException("The name of the workflow to run must be set");
        if (string.IsNullOrWhiteSpace(Version)) throw new NullReferenceException("The version of the workflow to run must be set");
        return new()
        {
            Namespace = string.IsNullOrWhiteSpace(Namespace) ? WorkflowDefinitionMetadata.DefaultNamespace : Namespace,
            Name = Name,
            Version = Version,
            Input = Input
        };
    }

    [GeneratedRegex(@"^[a-z0-9]([a-z0-9\-]{0,61}[a-z0-9])?$", RegexOptions.Compiled)]
    private static partial Regex DnsLabelRegex();
}
