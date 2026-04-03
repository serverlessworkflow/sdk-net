using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IContainerProcessDefinitionBuilder"/> interface
/// </summary>
public sealed class ContainerProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ContainerProcessDefinition>, IContainerProcessDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the name of the container image to run
    /// </summary>
    protected string? Image { get; set; }

    /// <summary>
    /// Gets/sets the name of the container to run
    /// </summary>
    protected string? Name { get; set; }

    /// <summary>
    /// Gets/sets the command, if any, to execute on the container
    /// </summary>
    protected string? Command { get; set; }

    /// <summary>
    /// Gets/sets a list containing the container's port mappings, if any
    /// </summary>
    protected EquatableDictionary<ushort, ushort>? Ports { get; set; }

    /// <summary>
    /// Gets/sets the volumes mapping for the container, if any
    /// </summary>
    protected EquatableDictionary<string, string>? Volumes { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping of the environment variables, if any, to use when running the configured process
    /// </summary>
    protected EquatableDictionary<string, string>? Environment { get; set; }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithImage(string image)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(image);
        Image = image;
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithCommand(string command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);
        Command = command;
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithPort(ushort hostPort, ushort containerPort)
    {
        Ports ??= [];
        Ports[hostPort] = containerPort;
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithPorts(IDictionary<ushort, ushort> portMapping)
    {
        ArgumentNullException.ThrowIfNull(portMapping);
        Ports = new(portMapping);
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithVolume(string key, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        Volumes ??= [];
        Volumes[key] = value;
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithVolumes(IDictionary<string, string> volumes)
    {
        ArgumentNullException.ThrowIfNull(volumes);
        Volumes = new(volumes);
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        Environment ??= [];
        Environment[name] = value;
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IContainerProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        Environment = new(environment);
        IContainerProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public override ContainerProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(Image)) throw new NullReferenceException("The image of the container to run must be set");
        return new()
        {
            Image = Image,
            Name = Name,
            Command = Command,
            Ports = Ports,
            Volumes = Volumes,
            Environment = Environment
        };
    }

}
