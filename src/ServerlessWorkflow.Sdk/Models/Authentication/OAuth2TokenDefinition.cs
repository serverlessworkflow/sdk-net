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
/// Represents the definition of an OAUTH2 token
/// </summary>
[DataContract]
public record OAuth2TokenDefinition
{

    /// <summary>
    /// Gets/sets the security token to use
    /// </summary>
    [DataMember(Name = "token", Order = 1), JsonPropertyName("token"), JsonPropertyOrder(1), YamlMember(Alias = "token", Order = 1)]
    public required virtual string Token { get; set; }

    /// <summary>
    /// Gets/sets the type of security token to use
    /// </summary>
    [DataMember(Name = "type", Order = 2), JsonPropertyName("type"), JsonPropertyOrder(2), YamlMember(Alias = "type", Order = 2)]
    public required virtual string Type { get; set; }

}