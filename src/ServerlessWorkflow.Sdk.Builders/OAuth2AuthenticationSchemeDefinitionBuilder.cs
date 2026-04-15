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

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/> interface
/// </summary>
public abstract class OAuth2AuthenticationSchemeDefinitionBuilder<TDefinition, TBuilder>
    : AuthenticationSchemeDefinitionBuilder<TDefinition>, IOAuth2AuthenticationSchemeDefinitionBuilder<TDefinition, TBuilder>
    where TDefinition : OAuth2AuthenticationSchemeDefinitionBase
    where TBuilder : IOAuth2AuthenticationSchemeDefinitionBuilder<TDefinition, TBuilder>
{

    /// <summary>
    /// Gets/sets the uri that references the OAUTH2 authority to use
    /// </summary>
    protected Uri? Authority { get; set; }

    /// <summary>
    /// Gets/sets the grant type to use
    /// </summary>
    protected string? GrantType { get; set; }

    /// <summary>
    /// Gets/sets the definition of the client to use
    /// </summary>
    protected OAuth2AuthenticationClientDefinition? Client { get; set; }

    /// <summary>
    /// Gets/sets the configuration of the authentication request to perform
    /// </summary>
    protected OAuth2AuthenticationRequestDefinition Request { get; set; } = new();

    /// <summary>
    /// Gets/sets a list, if any, that contains valid issuers that will be used to check against the issuer of generated tokens
    /// </summary>
    protected EquatableList<string>? Issuers { get; set; }

    /// <summary>
    /// Gets/sets the scopes, if any, to request the token for
    /// </summary>
    protected EquatableList<string>? Scopes { get; set; }

    /// <summary>
    /// Gets/sets the audiences, if any, to request the token for
    /// </summary>
    protected EquatableList<string>? Audiences { get; set; }

    /// <summary>
    /// Gets/sets the username to use. Used only if <see cref="GrantType"/> is <see cref="OAuth2GrantType.Password"/>
    /// </summary>
    protected string? Username { get; set; }

    /// <summary>
    /// Gets/sets the password to use. Used only if <see cref="GrantType"/> is <see cref="OAuth2GrantType.Password"/>
    /// </summary>
    protected string? Password { get; set; }

    /// <summary>
    /// Gets/sets the security token that represents the identity of the party on behalf of whom the request is being made. Used only if <see cref="GrantType"/> is <see cref="OAuth2GrantType.TokenExchange"/>, in which case it is required
    /// </summary>
    protected OAuth2TokenDefinition? Subject { get; set; }

    /// <summary>
    /// Gets/sets the security token that represents the identity of the acting party. Typically, this will be the party that is authorized to use the requested security token and act on behalf of the subject.
    /// Used only if <see cref="GrantType"/> is <see cref="OAuth2GrantType.TokenExchange"/>, in which case it is required
    /// </summary>
    protected OAuth2TokenDefinition? Actor { get; set; }

    /// <inheritdoc/>
    public virtual TBuilder WithAuthority(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        Authority = uri;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithGrantType(string grantType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(grantType);
        GrantType = grantType;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithClient(OAuth2AuthenticationClientDefinition client)
    {
        ArgumentNullException.ThrowIfNull(client);
        Client = client;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithClient(Action<IOAuth2AuthenticationClientDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OAuth2AuthenticationClientDefinitionBuilder();
        setup(builder);
        Client = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithRequest(OAuth2AuthenticationRequestDefinition request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Request = request;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithRequest(Action<IOAuth2AuthenticationRequestDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OAuth2AuthenticationRequestDefinitionBuilder();
        setup(builder);
        Request = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithIssuers(params string[] issuers)
    {
        ArgumentNullException.ThrowIfNull(issuers);
        Issuers = new(issuers);
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithAudiences(params string[] audiences)
    {
        ArgumentNullException.ThrowIfNull(audiences);
        Audiences = new(audiences);
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithScopes(params string[] scopes)
    {
        Scopes = new(scopes);
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithActor(OAuth2TokenDefinition actor)
    {
        ArgumentNullException.ThrowIfNull(actor);
        Actor = actor;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithUsername(string username)
    {
        Username = username;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithPassword(string password)
    {
        Password = password;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithSubject(OAuth2TokenDefinition subject)
    {
        Subject = subject;
        return (TBuilder)(object)this;
    }

    AuthenticationSchemeDefinition IAuthenticationSchemeDefinitionBuilder.Build() => Build();

}

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/> interface
/// </summary>
public class OAuth2AuthenticationSchemeDefinitionBuilder
    : OAuth2AuthenticationSchemeDefinitionBuilder<OAuth2AuthenticationSchemeDefinition, IOAuth2AuthenticationSchemeDefinitionBuilder>, IOAuth2AuthenticationSchemeDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the configuration of the OAUTH2 endpoints to use
    /// </summary>
    protected OAuth2AuthenticationEndpointsDefinition Endpoints { get; set; } = new();

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithEndpoints(OAuth2AuthenticationEndpointsDefinition endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        Endpoints = endpoints;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithEndpoints(Action<IOAuth2AuthenticationEndpointsDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OAuth2AuthenticationEndpointsDefinitionBuilder();
        setup(builder);
        Endpoints = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public override OAuth2AuthenticationSchemeDefinition Build()
    {
        if (Authority == null) throw new NullReferenceException("The authority must be set");
        if (string.IsNullOrWhiteSpace(GrantType)) throw new NullReferenceException("The grant type must be set");
        return new()
        {
            Use = Secret,
            Authority = Authority,
            Endpoints = Endpoints,
            Grant = GrantType,
            Client = Client,
            Request = Request,
            Issuers = Issuers,
            Audiences = Audiences,
            Scopes = Scopes,
            Actor = Actor,
            Username = Username,
            Password = Password,
            Subject = Subject
        };
    }

}
