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
/// Represents the configuration of an OAUTH2 authentication request
/// </summary>
[Description("Represents the configuration of an OAUTH2 authentication request")]
[DataContract]
public sealed record OAuth2AuthenticationRequestDefinition
{

    /// <summary>
    /// Gets/sets the encoding of the authentication request. Defaults to 'application/x-www-form-urlencoded'. See <see cref="OAuth2RequestEncoding"/>
    /// </summary>
    [Description("The encoding of the authentication request. Defaults to 'application/x-www-form-urlencoded'. See OAuth2RequestEncoding")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "encoding"), JsonPropertyOrder(1), JsonPropertyName("encoding")]
    public string Encoding { get; init; } = OAuth2RequestEncoding.FormUrl;

}