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
/// Represents the configuration of OAUTH2 endpoints
/// </summary>
[DataContract]
public record OAuth2AuthenticationEndpointsDefinition
{

    /// <summary>
    /// Gets/sets the relative path to the token endpoint. Defaults to `/oauth2/token`
    /// </summary>
    [Required]
    [DataMember(Name = "token", Order = 1), JsonPropertyName("token"), JsonPropertyOrder(1), YamlMember(Alias = "token", Order = 1)]
    public virtual Uri Token { get; set; } = new("/oauth2/token", UriKind.RelativeOrAbsolute);

    /// <summary>
    /// Gets/sets the relative path to the revocation endpoint. Defaults to `/oauth2/revoke`
    /// </summary>
    [Required]
    [DataMember(Name = "revocation", Order = 2), JsonPropertyName("revocation"), JsonPropertyOrder(2), YamlMember(Alias = "revocation", Order = 2)]
    public virtual Uri Revocation { get; set; } = new("/oauth2/revoke", UriKind.RelativeOrAbsolute);

    /// <summary>
    /// Gets/sets the relative path to the introspection endpoint. Defaults to `/oauth2/introspect`
    /// </summary>
    [Required]
    [DataMember(Name = "introspection", Order = 3), JsonPropertyName("introspection"), JsonPropertyOrder(3), YamlMember(Alias = "introspection", Order = 3)]
    public virtual Uri Introspection { get; set; } = new("/oauth2/introspect", UriKind.RelativeOrAbsolute);

}
