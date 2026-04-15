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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an authentication policy
/// </summary>
[Description("Represents the definition of an authentication policy")]
[DataContract]
public sealed record AuthenticationPolicyDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets the configured authentication scheme
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public string Scheme => Basic?.Scheme ?? Bearer?.Scheme ?? Certificate?.Scheme ?? Digest?.Scheme ?? OAuth2?.Scheme ?? Oidc?.Scheme ?? throw new NullReferenceException();

    /// <summary>
    /// Gets/sets the name of the top level authentication policy to use, if any
    /// </summary>
    [Description("The name of the top level authentication policy to use, if any")]
    [DataMember(Order = 1, Name = "use"), JsonPropertyOrder(1), JsonPropertyName("use")]
    public string? Use { get; init; }

    /// <summary>
    /// Gets/sets the `basic` authentication scheme to use, if any
    /// </summary>
    [Description("The `basic` authentication scheme to use, if any)")]
    [DataMember(Order = 2, Name = "basic"), JsonPropertyOrder(2), JsonPropertyName("basic")]
    public BasicAuthenticationSchemeDefinition? Basic { get; init; }

    /// <summary>
    /// Gets/sets the `Bearer` authentication scheme to use, if any
    /// </summary>
    [Description("The `Bearer` authentication scheme to use, if any)")]
    [DataMember(Order = 3, Name = "bearer"), JsonPropertyOrder(3), JsonPropertyName("bearer")]
    public BearerAuthenticationSchemeDefinition? Bearer { get; init; }

    /// <summary>
    /// Gets/sets the `Certificate` authentication scheme to use, if any
    /// </summary>
    [Description("The `Certificate` authentication scheme to use, if any)")]
    [DataMember(Order = 4, Name = "certificate"), JsonPropertyOrder(4), JsonPropertyName("certificate")]
    public CertificateAuthenticationSchemeDefinition? Certificate { get; init; }

    /// <summary>
    /// Gets/sets the `Digest` authentication scheme to use, if any
    /// </summary>
    [Description("The `Digest` authentication scheme to use, if any'")]
    [DataMember(Order = 5, Name = "digest"), JsonPropertyOrder(5), JsonPropertyName("digest")]
    public DigestAuthenticationSchemeDefinition? Digest { get; init; }

    /// <summary>
    /// Gets/sets the `OAUTH2` authentication scheme to use, if any
    /// </summary>
    [Description("The `OAUTH2` authentication scheme to use, if any)")]
    [DataMember(Order = 6, Name = "oauth2"), JsonPropertyOrder(6), JsonPropertyName("oauth2")]
    public OAuth2AuthenticationSchemeDefinition? OAuth2 { get; init; }

    /// <summary>
    /// Gets/sets the `OIDC` authentication scheme to use, if any
    /// </summary>
    [Description("The `OIDC` authentication scheme to use, if any)")]
    [DataMember(Order = 7, Name = "oidc"), JsonPropertyOrder(7), JsonPropertyName("oidc")]
    public OpenIDConnectSchemeDefinition? Oidc { get; init; }

}
