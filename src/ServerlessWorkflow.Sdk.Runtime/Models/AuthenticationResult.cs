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

namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents the result of an authentication operation
/// </summary>
/// <param name="Scheme">The authentication scheme</param>
/// <param name="Value">The authentication value</param>
[Description("Represents the result of an authentication operation")]
[DataContract]
public sealed record AuthenticationResult(string Scheme, string Value)
    : IAuthenticationResult
{

    /// <inheritdoc/>
    [Description("The OAUTH2 token")]
    [DataMember(Order = 1, Name = "token"), JsonPropertyOrder(1), JsonPropertyName("token")]
    public string Scheme { get; init; } = Scheme;

    /// <inheritdoc/>
    [Description("The OAUTH2 token")]
    [DataMember(Order = 2, Name = "token"), JsonPropertyOrder(2), JsonPropertyName("token")]
    public string Value { get; init; } = Value;

}
