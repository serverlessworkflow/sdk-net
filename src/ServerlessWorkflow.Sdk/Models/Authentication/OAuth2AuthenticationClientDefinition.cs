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
/// Represents the definition of an OAUTH2 client
/// </summary>
[DataContract]
public record OAuth2AuthenticationClientDefinition
{

    /// <summary>
    /// Gets/sets the OAUTH2 `client_id` to use. Required if 'Authentication' has NOT been set to 'none'.
    /// </summary>
    [DataMember(Name = "id", Order = 1), JsonPropertyName("id"), JsonPropertyOrder(1), YamlMember(Alias = "id", Order = 1)]
    public virtual string? Id { get; set; }

    /// <summary>
    /// Gets/sets the OAUTH2 `client_secret` to use, if any
    /// </summary>
    [DataMember(Name = "secret", Order = 2), JsonPropertyName("secret"), JsonPropertyOrder(2), YamlMember(Alias = "secret", Order = 2)]
    public virtual string? Secret { get; set; }

    /// <summary>
    /// Gets/sets a JWT containing a signed assertion with the application credentials
    /// </summary>
    [DataMember(Name = "assertion", Order = 3), JsonPropertyName("assertion"), JsonPropertyOrder(3), YamlMember(Alias = "assertion", Order = 3)]
    public virtual string? Assertion { get; set; }

    /// <summary>
    /// Gets/sets the authentication method to use to authenticate the client. Defaults to 'client_secret_post'. See <see cref="OAuth2ClientAuthenticationMethod"/>
    /// </summary>
    [DataMember(Name = "authentication", Order = 4), JsonPropertyName("authentication"), JsonPropertyOrder(4), YamlMember(Alias = "authentication", Order = 4)]
    public virtual string? Authentication { get; set; }

}