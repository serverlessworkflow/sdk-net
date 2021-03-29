﻿/*
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
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents the object used to configure how to filter the states data input and output
    /// </summary>
    public class StateDataFilter
    {

        /// <summary>
        /// Gets/sets an expression to filter the states data input
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "input")]
        [System.Text.Json.Serialization.JsonPropertyName("input")]
        [YamlMember(Alias = "input")]
        public virtual string Input { get; set; }

        /// <summary>
        /// Gets/sets an expression that filters the states data output
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "output")]
        [System.Text.Json.Serialization.JsonPropertyName("output")]
        [YamlMember(Alias = "output")]
        public virtual string Output { get; set; }

    }

}