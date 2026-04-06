namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IScriptProcessDefinitionBuilder"/> interface
/// </summary>
public sealed class ScriptProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ScriptProcessDefinition>, IScriptProcessDefinitionBuilder
{

    string? language;
    string? code;
    ExternalResourceDefinition? source;
    Uri? sourceUri;
    EquatableDictionary<string, object>? arguments;
    EquatableDictionary<string, string>? environment;

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithLanguage(string language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        this.language = language;
        return this;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithCode(string code)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        this.code = code;
        return this;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithSource(Uri source)
    {
        ArgumentNullException.ThrowIfNull(source);
        sourceUri = source;
        return this;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithSource(Action<IExternalResourceDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ExternalResourceDefinitionBuilder();
        setup(builder);
        source = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithArgument(string name, object value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        arguments ??= [];
        arguments[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithArguments(IDictionary<string, object> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.arguments = [.. arguments];
        return this;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        environment ??= [];
        environment[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public IScriptProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        this.environment = [.. environment];
        return this;
    }

    /// <inheritdoc/>
    public override ScriptProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(language)) throw new NullReferenceException("The language in which the script to run is expressed must be set");
        if (string.IsNullOrWhiteSpace(code) && this.source == null && sourceUri == null) throw new NullReferenceException("Either the code or the source properties must be set");
        ExternalResourceDefinition? source = this.source;
        if (source == null && sourceUri != null) source = new() 
        { 
            Endpoint = sourceUri 
        };
        return new()
        {
            Language = language,
            Code = code,
            Source = source,
            Arguments = arguments,
            Environment = environment
        };
    }

}
