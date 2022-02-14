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
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Enumerates all types of workflow states
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [JsonConverter(typeof(System.Text.Json.Serialization.StringEnumConverterFactory))]
    public enum StateType
    {
        /// <summary>
        /// Indicates an operation state
        /// </summary>
        [EnumMember(Value = "operation")]
        Operation,
        /// <summary>
        /// Indicates a sleep state
        /// </summary>
        [EnumMember(Value = "sleep")]
        Sleep,
        /// <summary>
        /// Indicates an event state
        /// </summary>
        [EnumMember(Value = "event")]
        Event,
        /// <summary>
        /// Indicates a parallel state
        /// </summary>
        [EnumMember(Value = "parallel")]
        Parallel,
        /// <summary>
        /// Indicates a switch state
        /// </summary>
        [EnumMember(Value = "switch")]
        Switch,
        /// <summary>
        /// Indicates an inject state
        /// </summary>
        [EnumMember(Value = "inject")]
        Inject,
        /// <summary>
        /// Indicates a foreach state
        /// </summary>
        [EnumMember(Value = "forEach")]
        ForEach,
        /// <summary>
        /// Indicates a callback state
        /// </summary>
        [EnumMember(Value = "callback")]
        Callback
    }

}
