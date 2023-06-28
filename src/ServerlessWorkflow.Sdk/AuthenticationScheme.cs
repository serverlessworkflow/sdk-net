// Copyright © 2023-Present The Serverless Workflow Specification Authors
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
/// Enumerates all supported authentication schemes
/// </summary>
public static class AuthenticationScheme
{

    /// <summary>
    /// Gets the 'basic' authentication scheme
    /// </summary>
    public const string Basic = "basic";
    /// <summary>
    /// Gets the 'bearer' authentication scheme
    /// </summary>
    public const string Bearer = "bearer";
    /// <summary>
    /// Gets the 'oauth2' authentication scheme
    /// </summary>
    public const string OAuth2 = "oauth2";

}
