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
using System;
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to define the execution timeout for a workflow instance
    /// </summary>
    public class ExecutionTimeoutDefinition
    {

        /// <summary>
        /// Gets/sets the timeout interval
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public virtual TimeSpan Interval { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to terminate the workflow execution.
        /// </summary>
        public virtual bool Interrupt { get; set; } = false;

        /// <summary>
        /// Gets/sets the name of a workflow state to be executed before workflow instance is terminated
        /// </summary>
        public virtual string RunBefore { get; set; }

    }

}
