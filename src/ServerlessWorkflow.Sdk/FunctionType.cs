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
/// Enumerates all types of functions
/// </summary>
public static class FunctionType
{

    /// <summary>
    /// Indicates an Async API function
    /// </summary>
    public const string AsyncApi = "asyncapi";

    /// <summary>
    /// Indicates an expression function
    /// </summary>
    public const string Expression = "expression";

    /// <summary>
    /// Indicates a GraphQL function
    /// </summary>
    public const string GraphQL = "graphql";

    /// <summary>
    /// Indicates an OData function
    /// </summary>
    public const string OData = "odata";

    /// <summary>
    /// Indicates a REST function
    /// </summary>
    public const string Rest = "rest";

    /// <summary>
    /// Indicates an Remote Procedure Call (RPC)
    /// </summary>
    public const string Rpc = "rpc";

    /// <summary>
    /// Gets all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> GetValues()
    {
        yield return Rest;
        yield return Rpc;
        yield return GraphQL;
        yield return OData;
        yield return Expression;
        yield return AsyncApi;
    }

}
