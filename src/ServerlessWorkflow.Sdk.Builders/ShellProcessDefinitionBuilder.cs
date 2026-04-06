namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IShellProcessDefinitionBuilder"/> interface
/// </summary>
public sealed class ShellProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ShellProcessDefinition>, IShellProcessDefinitionBuilder
{

    string? command;
    EquatableList<string>? arguments;
    EquatableDictionary<string, string>? environment;

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithCommand(string command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);
        this.command = command;
        return this;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithArgument(string argument)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(argument);
        this.arguments ??= [];
        this.arguments.Add(argument);
        return this;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithArguments(IEnumerable<string> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.arguments = [.. arguments];
        return this;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.environment ??= [];
        this.environment[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        this.environment = [.. environment];
        return this;
    }

    /// <inheritdoc/>
    public override ShellProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.command)) throw new NullReferenceException("The shell command to execute must be set");
        return new()
        {
            Command = this.command,
            Arguments = this.arguments,
            Environment = this.environment
        };
    }

}
