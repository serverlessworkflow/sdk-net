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

using ServerlessWorkflow.Sdk.Models.Authentication;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an authentication policy
/// </summary>
[DataContract]
public record AuthenticationPolicyDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets the configured authentication scheme
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string Scheme => this.Basic?.Scheme ?? this.Bearer?.Scheme ?? this.Certificate?.Scheme ?? this.Digest?.Scheme ?? this.OAuth2?.Scheme ?? this.Oidc?.Scheme ?? throw new NullReferenceException();

    /// <summary>
    /// Gets/sets the name of the top level authentication policy to use, if any
    /// </summary>
    [DataMember(Name = "use", Order = 1), JsonPropertyName("use"), JsonPropertyOrder(1), YamlMember(Alias = "use", Order = 1)]
    public virtual string? Use { get; set; }

    /// <summary>
    /// Gets/sets the `basic` authentication scheme to use, if any
    /// </summary>
    [DataMember(Name = "basic", Order = 1), JsonPropertyName("basic"), JsonPropertyOrder(1), YamlMember(Alias = "basic", Order = 1)]
    public virtual BasicAuthenticationSchemeDefinition? Basic { get; set; }

    /// <summary>
    /// Gets/sets the `Bearer` authentication scheme to use, if any
    /// </summary>
    [DataMember(Name = "bearer", Order = 2), JsonPropertyName("bearer"), JsonPropertyOrder(2), YamlMember(Alias = "bearer", Order = 2)]
    public virtual BearerAuthenticationSchemeDefinition? Bearer { get; set; }

    /// <summary>
    /// Gets/sets the `Certificate` authentication scheme to use, if any
    /// </summary>
    [DataMember(Name = "certificate", Order = 3), JsonPropertyName("certificate"), JsonPropertyOrder(3), YamlMember(Alias = "certificate", Order = 3)]
    public virtual CertificateAuthenticationSchemeDefinition? Certificate { get; set; }

    /// <summary>
    /// Gets/sets the `Digest` authentication scheme to use, if any
    /// </summary>
    [DataMember(Name = "digest", Order = 4), JsonPropertyName("digest"), JsonPropertyOrder(4), YamlMember(Alias = "digest", Order = 4)]
    public virtual DigestAuthenticationSchemeDefinition? Digest { get; set; }

    /// <summary>
    /// Gets/sets the `OAUTH2` authentication scheme to use, if any
    /// </summary>
    [DataMember(Name = "oauth2", Order = 5), JsonPropertyName("oauth2"), JsonPropertyOrder(5), YamlMember(Alias = "oauth2", Order = 5)]
    public virtual OAuth2AuthenticationSchemeDefinition? OAuth2 { get; set; }

    /// <summary>
    /// Gets/sets the `OIDC` authentication scheme to use, if any
    /// </summary>
    [DataMember(Name = "oidc", Order = 6), JsonPropertyName("oidc"), JsonPropertyOrder(6), YamlMember(Alias = "oidc", Order = 6)]
    public virtual OpenIDConnectSchemeDefinition? Oidc { get; set; }

}
