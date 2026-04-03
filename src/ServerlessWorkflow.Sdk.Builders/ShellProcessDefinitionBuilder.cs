using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IShellProcessDefinitionBuilder"/> interface
/// </summary>
public sealed class ShellProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ShellProcessDefinition>, IShellProcessDefinitionBuilder
{

    /// <summary>
    /// Gets the command to execute
    /// </summary>
    protected string? Command { get; set; }

    /// <summary>
    /// Gets the arguments, if any, of the command to execute
    /// </summary>
    protected EquatableList<string>? Arguments { get; set; }

    /// <summary>
    /// Gets/sets the environment variables, if any, of the shell command to execute
    /// </summary>
    protected EquatableDictionary<string, string>? Environment { get; set; }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithCommand(string command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);
        Command = command;
        IShellProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithArgument(string argument)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(argument);
        Arguments ??= [];
        Arguments.Add(argument);
        IShellProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithArguments(IEnumerable<string> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        Arguments = [.. arguments];
        IShellProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Environment ??= [];
        Environment[name] = value;
        IShellProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        Environment = new(environment);
        IShellProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public override ShellProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(Command)) throw new NullReferenceException("The shell command to execute must be set");
        return new()
        {
            Command = Command,
            Arguments = Arguments,
            Environment = Environment
        };
    }

}
