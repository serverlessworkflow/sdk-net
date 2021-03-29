/*
 * Copyright 2020-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Enumerates all kinds of workflow events
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.StringEnumConverterFactory))]
    public enum EventKind
    {
        /// <summary>
        /// Indicates an event to consume
        /// </summary>
        [EnumMember(Value = "consumed")]
        Consumed,
        /// <summary>
        /// Indicates an event to produce
        /// </summary>
        [EnumMember(Value = "produced")]
        Produced
    }

}
