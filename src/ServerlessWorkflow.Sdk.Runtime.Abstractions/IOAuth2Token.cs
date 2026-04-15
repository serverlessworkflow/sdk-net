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

using System.Reflection.Metadata;

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of an OAUTH2 token
/// </summary>
public interface IOAuth2Token
{

    /// <summary>
    /// Gets the UTC date and time at which the <see cref="IOAuth2Token"/> has been created
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the OAUTH2 token type
    /// </summary>
    string? TokenType { get; }

    /// <summary>
    /// Gets the OAUTH2 token id
    /// </summary>
    string? TokenId { get; }

    /// <summary>
    /// Gets the OAUTH2 access token
    /// </summary>
    string? AccessToken { get; }

    /// <summary>
    /// Gets the OAUTH2 refresh token
    /// </summary>
    string? RefreshToken { get; }

    /// <summary>
    /// Gets the <see cref="IOAuth2Token"/> Time To Live, in seconds
    /// </summary>
    int Ttl { get; }

    /// <summary>
    /// Gets the UTC date and time at which the <see cref="IOAuth2Token"/> expires
    /// </summary>
    DateTime? ExpiresAt { get; }

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="IOAuth2Token"/> has expired
    /// </summary>
    bool HasExpired { get; }

}
