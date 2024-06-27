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

namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the definition of an Open ID Connect authentication scheme
/// </summary>
[DataContract]
public record OAuth2AuthenticationSchemeDefinition
    : AuthenticationSchemeDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public override string Scheme => AuthenticationScheme.OAuth2;

    /// <summary>
    /// Gets/sets the uri that references the OAUTH2 authority to use
    /// </summary>
    [DataMember(Name = "authority", Order = 1), JsonPropertyName("authority"), JsonPropertyOrder(1), YamlMember(Alias = "authority", Order = 1)]
    public required virtual Uri Authority { get; set; }

    /// <summary>
    /// Gets/sets the grant type to use
    /// </summary>
    [DataMember(Name = "grant", Order = 2), JsonPropertyName("grant"), JsonPropertyOrder(2), YamlMember(Alias = "grant", Order = 2)]
    public required virtual string Grant { get; set; }

    /// <summary>
    /// Gets/sets the definition of the client to use
    /// </summary>
    [DataMember(Name = "client", Order = 3), JsonPropertyName("client"), JsonPropertyOrder(3), YamlMember(Alias = "client", Order = 3)]
    public required virtual OAuth2AuthenticationClientDefinition Client { get; set; }

    /// <summary>
    /// Gets/sets the scopes, if any, to request the token for
    /// </summary>
    [DataMember(Name = "scopes", Order = 4), JsonPropertyName("scopes"), JsonPropertyOrder(4), YamlMember(Alias = "scopes", Order = 4)]
    public virtual EquatableList<string>? Scopes { get; set; }

    /// <summary>
    /// Gets/sets the audiences, if any, to request the token for
    /// </summary>
    [DataMember(Name = "audiences", Order = 5), JsonPropertyName("audiences"), JsonPropertyOrder(5), YamlMember(Alias = "audiences", Order = 5)]
    public virtual EquatableList<string>? Audiences { get; set; }

    /// <summary>
    /// Gets/sets the username to use. Used only if <see cref="Grant"/> is <see cref="OAuth2GrantType.Password"/>
    /// </summary>
    [DataMember(Name = "username", Order = 6), JsonPropertyName("username"), JsonPropertyOrder(6), YamlMember(Alias = "username", Order = 6)]
    public virtual string? Username { get; set; }

    /// <summary>
    /// Gets/sets the password to use. Used only if <see cref="Grant"/> is <see cref="OAuth2GrantType.Password"/>
    /// </summary>
    [DataMember(Name = "password", Order = 7), JsonPropertyName("password"), JsonPropertyOrder(7), YamlMember(Alias = "password", Order = 7)]
    public virtual string? Password { get; set; }

    /// <summary>
    /// Gets/sets the security token that represents the identity of the party on behalf of whom the request is being made. Used only if <see cref="Grant"/> is <see cref="OAuth2GrantType.TokenExchange"/>, in which case it is required
    /// </summary>
    [DataMember(Name = "subject", Order = 8), JsonPropertyName("subject"), JsonPropertyOrder(8), YamlMember(Alias = "subject", Order = 8)]
    public virtual OAuth2TokenDefinition? Subject { get; set; }

    /// <summary>
    /// Gets/sets the security token that represents the identity of the acting party. Typically, this will be the party that is authorized to use the requested security token and act on behalf of the subject.
    /// Used only if <see cref="Grant"/> is <see cref="OAuth2GrantType.TokenExchange"/>, in which case it is required
    /// </summary>
    [DataMember(Name = "actor", Order = 9), JsonPropertyName("actor"), JsonPropertyOrder(9), YamlMember(Alias = "actor", Order = 9)]
    public virtual OAuth2TokenDefinition? Actor { get; set; }

}
