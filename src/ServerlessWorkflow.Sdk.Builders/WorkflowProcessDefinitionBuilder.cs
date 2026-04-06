namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowProcessDefinitionBuilder"/> interface
/// </summary>
public sealed partial class WorkflowProcessDefinitionBuilder
    : ProcessDefinitionBuilder<WorkflowProcessDefinition>, IWorkflowProcessDefinitionBuilder
{

    string? @namespace;
    string? name;
    string version = "latest";
    JsonObject? input;

    /// <inheritdoc/>
    public IWorkflowProcessDefinitionBuilder WithNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        if (!DnsLabelRegex().IsMatch(@namespace)) throw new ArgumentException($"The specified value '{@namespace}' is not a valid RFC1123 DNS label name", nameof(@namespace));
        this.@namespace = @namespace;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowProcessDefinitionBuilder WithName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (!DnsLabelRegex().IsMatch(name)) throw new ArgumentException($"The specified value '{name}' is not a valid RFC1123 DNS label name", nameof(name));
        this.name = name;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowProcessDefinitionBuilder WithVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        this.version = version;
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowProcessDefinitionBuilder WithInput(JsonObject input)
    {
        this.input = input;
        return this;
    }

    /// <inheritdoc/>
    public override WorkflowProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(name)) throw new NullReferenceException("The name of the workflow to run must be set");
        if (string.IsNullOrWhiteSpace(version)) throw new NullReferenceException("The version of the workflow to run must be set");
        return new()
        {
            Namespace = string.IsNullOrWhiteSpace(@namespace) ? WorkflowDefinitionMetadata.DefaultNamespace : @namespace,
            Name = name,
            Version = version,
            Input = input
        };
    }

    [GeneratedRegex(@"^[a-z0-9]([a-z0-9\-]{0,61}[a-z0-9])?$", RegexOptions.Compiled)]
    private static partial Regex DnsLabelRegex();
}
