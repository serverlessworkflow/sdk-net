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
/// Exposes all default authentication schemes
/// </summary>
public static class AuthenticationScheme
{

    /// <summary>
    /// Gets the 'Basic' authentication scheme
    /// </summary>
    public const string Basic = "Basic";
    /// <summary>
    /// Gets the 'Bearer' authentication scheme
    /// </summary>
    public const string Bearer = "Bearer";
    /// <summary>
    /// Gets the 'Certificate' authentication scheme
    /// </summary>
    public const string Certificate = "Certificate";
    /// <summary>
    /// Gets the 'Digest' authentication scheme
    /// </summary>
    public const string Digest = "Digest";
    /// <summary>
    /// Gets the 'OAUTH2' authentication scheme
    /// </summary>
    public const string OAuth2 = "OAuth2";
    /// <summary>
    /// Gets the 'OpenIDConnect' authentication scheme
    /// </summary>
    public const string OpenIDConnect = "OpenIDConnect";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing the authentication schemes supported by default
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the authentication schemes supported by default</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Basic;
        yield return Bearer;
        yield return Certificate;
        yield return Digest;
        yield return OAuth2;
        yield return OpenIDConnect;
    }

}
