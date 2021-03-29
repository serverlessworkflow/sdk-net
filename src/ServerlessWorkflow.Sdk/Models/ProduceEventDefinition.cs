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
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents the object used to configure an event o produce
    /// </summary>
    public class ProduceEventDefinition
    {

        /// <summary>
        /// Gets/sets the name of a defined event to produce
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        public string EventReference { get; set; }

        /// <summary>
        /// Gets/sets the data to pass to the cloud event to produce. If String, expression which selects parts of the states data output to become the data of the produced event. If object a custom object to become the data of produced event.
        /// </summary>
        public JToken Data { get; set; }

    }

}
