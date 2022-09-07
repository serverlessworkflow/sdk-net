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
namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Enumerates all types of functions
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.StringEnumConverterFactory))]
    public enum FunctionType
    {
        /// <summary>
        /// Indicates a REST function
        /// </summary>
        [EnumMember(Value = "rest")]
        Rest = 1,
        /// <summary>
        /// Indicates an Remote Procedure Call (RPC)
        /// </summary>
        [EnumMember(Value = "rpc")]
        Rpc = 2,
        /// <summary>
        /// Indicates a GraphQL function
        /// </summary>
        [EnumMember(Value = "graphql")]
        GraphQL = 4,
        /// <summary>
        /// Indicates an OData function
        /// </summary>
        [EnumMember(Value = "odata")]
        OData = 8,
        /// <summary>
        /// Indicates an expression function
        /// </summary>
        [EnumMember(Value = "expression")]
        Expression = 16,
        /// <summary>
        /// Indicates an Async API function
        /// </summary>
        [EnumMember(Value = "asyncapi")]
        AsyncApi = 32
    }

}
