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
[Description("Represents the definition of a digest authentication scheme")]
[DataContract]
public sealed record DigestAuthenticationSchemeDefinition
    : AuthenticationSchemeDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Scheme => AuthenticationScheme.Digest;

    /// <summary>
    /// Gets/sets the username used for authentication
    /// </summary>
    [Description("The username used for authentication")]
    [DataMember(Order = 1, Name = "username"), JsonPropertyOrder(1), JsonPropertyName("username")]
    public string? Username { get; init; }

    /// <summary>
    /// Gets/sets the password used for authentication
    /// </summary>
    [Description("The password used for authentication")]
    [DataMember(Order = 2, Name = "password"), JsonPropertyOrder(2), JsonPropertyName("password")]
    public string? Password { get; init; }

}