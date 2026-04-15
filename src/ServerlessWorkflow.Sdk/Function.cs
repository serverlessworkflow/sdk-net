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
/// Exposes ServerlessWorkflow default functions
/// </summary>
public static class Function
{

    /// <summary>
    /// The function used to perform an AsyncAPI call
    /// </summary>
    public const string AsyncApi = "asyncapi";
    /// <summary>
    /// The function used to perform a GRPC call
    /// </summary>
    public const string Grpc = "grpc";
    /// <summary>
    /// The function used to perform an HTTP call
    /// </summary>
    public const string Http = "http";
    /// <summary>
    /// The function used to perform an OpenAPI call
    /// </summary>
    public const string OpenApi = "openapi";

    /// <summary>
    /// Enumerates all default functions
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all default functions</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return AsyncApi;
        yield return Grpc;
        yield return Http;
        yield return OpenApi;
    }

}
