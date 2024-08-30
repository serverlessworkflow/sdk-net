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

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes the default OAUTH2 grant types
/// </summary>
public static class OAuth2GrantType
{

    /// <summary>
    /// Gets the 'authorization_code' OAUTH2 grant types
    /// </summary>
    public const string AuthorizationCode = "authorization_code";
    /// <summary>
    /// Gets the 'client_credentials' OAUTH2 grant types
    /// </summary>
    public const string ClientCredentials = "client_credentials";
    /// <summary>
    /// Gets the 'implicit' OAUTH2 grant types
    /// </summary>
    public const string Implicit = "implicit";
    /// <summary>
    /// Gets the 'password' OAUTH2 grant types
    /// </summary>
    public const string Password = "password";
    /// <summary>
    /// Gets the 'refresh_token' OAUTH2 grant types
    /// </summary>
    public const string RefreshToken = "refresh_token";
    /// <summary>
    /// Gets the 'token_exchange' OAUTH2 grant types
    /// </summary>
    public const string TokenExchange = "urn:ietf:params:oauth:grant-type:token-exchange";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing the OAUTH2 grant types supported by default
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the OAUTH2 grant types supported by default</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return AuthorizationCode;
        yield return ClientCredentials;
        yield return Implicit;
        yield return Password;
        yield return RefreshToken;
        yield return TokenExchange;
    }

}
