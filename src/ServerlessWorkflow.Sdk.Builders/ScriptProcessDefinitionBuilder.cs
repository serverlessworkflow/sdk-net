using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IScriptProcessDefinitionBuilder"/> interface
/// </summary>
public sealed class ScriptProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ScriptProcessDefinition>, IScriptProcessDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the language of the script to run
    /// </summary>
    protected string? Language { get; set; }

    /// <summary>
    /// Gets/sets the script's code
    /// </summary>
    protected string? Code { get; set; }

    /// <summary>
    /// Gets/sets the script's source
    /// </summary>
    protected ExternalResourceDefinition? Source { get; set; }

    /// <summary>
    /// Gets/sets the uri that references the script's source
    /// </summary>
    protected Uri? SourceUri { get; set; }

    /// <summary>
    /// Gets the arguments, if any, of the command to execute
    /// </summary>
    protected EquatableDictionary<string, object>? Arguments { get; set; }

    /// <summary>
    /// Gets/sets the environment variables, if any, of the shell command to execute
    /// </summary>
    protected EquatableDictionary<string, string>? Environment { get; set; }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithLanguage(string language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        Language = language;
        IScriptProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithCode(string code)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        Code = code;
        IScriptProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithSource(Uri source)
    {
        ArgumentNullException.ThrowIfNull(source);
        SourceUri = source;
        IScriptProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithSource(Action<IExternalResourceDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ExternalResourceDefinitionBuilder();
        setup(builder);
        Source = builder.Build();
        IScriptProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithArgument(string name, object value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Arguments ??= [];
        Arguments[name] = value;
        IScriptProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithArguments(IDictionary<string, object> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        Arguments = new(arguments);
        IScriptProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Environment ??= [];
        Environment[name] = value;
        IScriptProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        Environment = new(environment);
        IScriptProcessDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public override ScriptProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(Language)) throw new NullReferenceException("The language in which the script to run is expressed must be set");
        if (string.IsNullOrWhiteSpace(Code) && Source == null && SourceUri == null) throw new NullReferenceException("Either the code or the source properties must be set");
        ExternalResourceDefinition? source = Source;
        if (source == null && SourceUri != null) source = new() { Endpoint = SourceUri };
        return new()
        {
            Language = Language,
            Code = Code,
            Source = source,
            Arguments = Arguments,
            Environment = Environment
        };
    }

}
