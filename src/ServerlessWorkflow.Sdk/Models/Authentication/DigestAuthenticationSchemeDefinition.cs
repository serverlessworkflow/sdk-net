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
/// Represents the definition of a digest authentication scheme
/// </summary>
[DataContract]
public record DigestAuthenticationSchemeDefinition
    : AuthenticationSchemeDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public override string Scheme => AuthenticationScheme.Digest;

    /// <summary>
    /// Gets/sets the username used for authentication
    /// </summary>
    [DataMember(Name = "username", Order = 1), JsonPropertyName("username"), JsonPropertyOrder(1), YamlMember(Alias = "username", Order = 1)]
    public virtual string Username { get; set; } = null!;

    /// <summary>
    /// Gets/sets the password used for authentication
    /// </summary>
    [DataMember(Name = "password", Order = 2), JsonPropertyName("password"), JsonPropertyOrder(2), YamlMember(Alias = "password", Order = 2)]
    public virtual string Password { get; set; } = null!;

}
