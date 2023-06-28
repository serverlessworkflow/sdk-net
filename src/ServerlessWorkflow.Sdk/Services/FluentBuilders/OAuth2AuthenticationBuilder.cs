// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationBuilder"/>
/// </summary>
public class OAuth2AuthenticationBuilder
    : AuthenticationDefinitionBuilder, IOAuth2AuthenticationBuilder
{

    /// <summary>
    /// Initializes a new <see cref="OAuth2AuthenticationBuilder"/>
    /// </summary>
    public OAuth2AuthenticationBuilder() : base(new AuthenticationDefinition() { Scheme = AuthenticationScheme.OAuth2, Properties = new OAuth2AuthenticationProperties() }) { }

    /// <summary>
    /// Gets the <see cref="OAuth2AuthenticationProperties"/> of the authentication definition to build
    /// </summary>
    protected OAuth2AuthenticationProperties Properties =>  (OAuth2AuthenticationProperties)this.AuthenticationDefinition.Properties!;

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder UseAudiences(params string[] audiences)
    {
        if (audiences == null) throw new ArgumentNullException(nameof(audiences));
        this.Properties.Audience = string.Join(" ", audiences);
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder UseGrantType(string grantType)
    {
        this.Properties.GrantType = grantType;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder UseScopes(params string[] scopes)
    {
        if (scopes == null) throw new ArgumentNullException(nameof(scopes));
        this.Properties.Audience = string.Join(" ", scopes);
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder WithAuthority(Uri authority)
    {
        this.Properties.Authority = authority ?? throw new ArgumentNullException(nameof(authority));
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder WithClientId(string clientId)
    {
        if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
        this.Properties.ClientId = clientId;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder WithClientSecret(string clientSecret)
    {
        if (string.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));
        this.Properties.ClientSecret = clientSecret;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder WithUserName(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
        this.Properties.Username = username;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder WithPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
        this.Properties.Password = password;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder WithSubjectToken(string tokenType, string token)
    {
        if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));
        this.Properties.SubjectTokenType = tokenType;
        this.Properties.SubjectToken = token;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationBuilder WithActorToken(string tokenType, string token)
    {
        if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));
        this.Properties.ActorTokenType = tokenType;
        this.Properties.ActorToken = token;
        return this;
    }

}
