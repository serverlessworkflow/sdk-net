namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IHttpCallDefinitionBuilder"/> interface
/// </summary>
public sealed class HttpCallDefinitionBuilder
    : IHttpCallDefinitionBuilder
{

    string? method;
    EndpointDefinition? endpoint;
    EquatableDictionary<string, string>? headers;
    EquatableDictionary<string, string>? cookies;
    JsonNode? body;
    string? outputFormat;

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithMethod(string method)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(method);
        this.method = method;
        return this;
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithUri(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        endpoint = new() { Uri = uri };
        return this;
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithEndpoint(EndpointDefinition endpoint)
    {
        ArgumentNullException.ThrowIfNull(endpoint);
        this.endpoint = endpoint;
        return this;
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithEndpoint(Action<IEndpointDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new EndpointDefinitionBuilder();
        setup(builder);
        return WithEndpoint(builder.Build());
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithHeader(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        headers ??= [];
        headers[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithHeaders(IDictionary<string, string> headers)
    {
        this.headers = headers == null ? null : new(headers);
        return this;
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithCookie(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        cookies ??= [];
        cookies[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithCookies(IDictionary<string, string> cookies)
    {
        this.cookies = cookies == null ? null : new(cookies);
        return this;
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithBody(JsonNode body)
    {
        this.body = body;
        return this;
    }

    /// <inheritdoc/>
    public IHttpCallDefinitionBuilder WithOutputFormat(string format)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(format);
        outputFormat = format;
        return this;
    }

    /// <inheritdoc/>
    public HttpCallDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(method)) throw new NullReferenceException("The HTTP method must be set");
        if (endpoint == null) throw new NullReferenceException("The HTTP endpoint must be set");
        return new()
        {
            Method = method,
            Endpoint = endpoint,
            Headers = headers,
            Body = body,
            Output = outputFormat
        };
    }

}