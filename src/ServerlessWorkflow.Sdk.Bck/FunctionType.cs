/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all types of functions
/// </summary>
public static class FunctionType
{

    /// <summary>
    /// Indicates a REST function
    /// </summary>
    public const string Rest = "rest";

    /// <summary>
    /// Indicates an Remote Procedure Call (RPC)
    /// </summary>
    public const string Rpc = "rpc";

    /// <summary>
    /// Indicates a GraphQL function
    /// </summary>
    public const string GraphQL = "graphql";

    /// <summary>
    /// Indicates an OData function
    /// </summary>
    public const string OData = "odata";
    /// <summary>
    /// Indicates an expression function
    /// </summary>
    public const string Expression = "expression";

    /// <summary>
    /// Indicates an Async API function
    /// </summary>
    public const string AsyncApi = "asyncapi";

}
