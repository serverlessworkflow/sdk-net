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
/// Exposes all supported request encodings for OAUTH2 requests
/// </summary>
public static class OAuth2RequestEncoding
{
    /// <summary>
    /// Represents the "application/x-www-form-urlencoded" content type
    /// </summary>
    public const string FormUrl = "application/x-www-form-urlencoded";
    /// <summary>
    /// Represents the "application/json" content type
    /// </summary>
    public const string Json = "application/json";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return FormUrl;
        yield return Json;
    }

}