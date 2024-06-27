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

using Neuroglia;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationSchemeDefinitionBuilder"/> interface
/// </summary>
public class OAuth2AuthenticationSchemeDefinitionBuilder
    : AuthenticationSchemeDefinitionBuilder<OAuth2AuthenticationSchemeDefinition>, IOAuth2AuthenticationSchemeDefinitionBuilder
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
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithAuthority(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        this.Authority = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithGrantType(string grantType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(grantType);
        this.GrantType = grantType;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithClient(OAuth2AuthenticationClientDefinition client)
    {
        ArgumentNullException.ThrowIfNull(client);
        this.Client = client;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithClient(Action<IOAuth2AuthenticationClientDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OAuth2AuthenticationClientDefinitionBuilder();
        setup(builder);
        this.Client = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithAudiences(params string[] audiences)
    {
        ArgumentNullException.ThrowIfNull(audiences);
        this.Audiences = new(audiences);
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithScopes(params string[] scopes)
    {
        this.Scopes = new(scopes);
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithActor(OAuth2TokenDefinition actor)
    {
        ArgumentNullException.ThrowIfNull(actor);
        this.Actor = actor;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithUsername(string username)
    {
        this.Username = username;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithPassword(string password)
    {
        this.Password = password;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationSchemeDefinitionBuilder WithSubject(OAuth2TokenDefinition subject)
    {
        this.Subject = subject;
        return this;
    }

    /// <inheritdoc/>
    public override OAuth2AuthenticationSchemeDefinition Build()
    {
        if (this.Authority == null) throw new NullReferenceException("The authority must be set");
        if (string.IsNullOrWhiteSpace(this.GrantType)) throw new NullReferenceException("The grant type must be set");
        if (this.Client == null) throw new NullReferenceException("The client must be set");
        return new()
        {
            Use = this.Secret,
            Authority = this.Authority,
            Grant = this.GrantType,
            Client = this.Client,
            Audiences = this.Audiences,
            Scopes = this.Scopes,
            Actor = this.Actor,
            Username = this.Username,
            Password = this.Password,
            Subject = this.Subject
        };
    }

    AuthenticationSchemeDefinition IAuthenticationSchemeDefinitionBuilder.Build() => this.Build();
}
