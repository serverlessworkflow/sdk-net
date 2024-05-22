// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ServerlessWorkflow.Sdk.Models.Calls;
using Neuroglia;
using System.Runtime.Serialization;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IHttpCallDefinitionBuilder"/> interface
/// </summary>
[DataContract]
public class HttpCallDefinitionBuilder
    : IHttpCallDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the HTTP method of the request to perform
    /// </summary>
    protected string? Method { get; set; }

    /// <summary>
    /// Gets/sets the definition of the endpoint to request
    /// </summary>
    protected EndpointDefinition? Endpoint { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the headers, if any, of the HTTP request to perform
    /// </summary>
    protected EquatableDictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the cookies, if any, of the HTTP request to perform
    /// </summary>
    protected EquatableDictionary<string, string>? Cookies { get; set; }

    /// <summary>
    /// Gets/sets the body, if any, of the HTTP request to perform
    /// </summary>
    protected object? Body { get; set; }

    /// <summary>
    /// Gets/sets the http call output format. Defaults to <see cref="HttpOutputFormat.Content"/>.
    /// </summary>
    protected string? OutputFormat { get; set; }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithMethod(string method)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(method);
        this.Method = method;
        return this;
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithUri(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        this.Endpoint = new() { Uri = uri };
        return this;
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithEndpoint(EndpointDefinition endpoint)
    {
        ArgumentNullException.ThrowIfNull(endpoint);
        this.Endpoint = endpoint;
        return this;
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithEndpoint(Action<IEndpointDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = (IEndpointDefinitionBuilder)new ExternalResourceDefinitionBuilder();
        setup(builder);
        return this.WithEndpoint(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithHeader(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        this.Headers ??= [];
        this.Headers[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithHeaders(IDictionary<string, string> headers)
    {
        this.Headers = headers == null ? null : new(headers);
        return this;
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithCookie(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        this.Cookies ??= [];
        this.Cookies[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithCookies(IDictionary<string, string> cookies)
    {
        this.Cookies = cookies == null ? null : new(cookies);
        return this;
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithBody(object body)
    {
        this.Body = body;
        return this;
    }

    /// <inheritdoc/>
    public virtual IHttpCallDefinitionBuilder WithOutputFormat(string format)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(format);
        this.OutputFormat = format;
        return this;
    }

    /// <inheritdoc/>
    public virtual HttpCallDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Method)) throw new NullReferenceException("The HTTP method must be set");
        if (this.Endpoint == null) throw new NullReferenceException("The HTTP endpoint must be set");
        return new()
        {
            Method = this.Method,
            Endpoint = this.Endpoint,
            Headers = this.Headers,
            Body = this.Body,
            Output = this.OutputFormat
        };
    }

}