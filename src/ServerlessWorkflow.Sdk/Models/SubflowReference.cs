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
using YamlDotNet.Serialization;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents a reference to a sub <see cref="WorkflowDefinition"/>
    /// </summary>
    public class SubflowReference
    {

        /// <summary>
        /// Initializes a new <see cref="SubflowReference"/>
        /// </summary>
        public SubflowReference()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="SubflowReference"/>
        /// </summary>
        /// <param name="workflowId">The id of the <see cref="WorkflowDefinition"/> to run</param>
        /// <param name="version">The version of the <see cref="WorkflowDefinition"/> to run. Defaults to 'latest'</param>
        /// <param name="waitForCompletion">A boolean indicating whether or not to wait for the completion of the <see cref="WorkflowDefinition"/> to run. Defaults to true</param>
        public SubflowReference(string workflowId, string version, bool waitForCompletion = true)
            : this()
        {
            if (string.IsNullOrWhiteSpace(workflowId))
                throw new ArgumentNullException(nameof(workflowId));
            this.WorkflowId = workflowId;
            this.Version = version;
            this.WaitForCompletion = waitForCompletion;
        }

        /// <summary>
        /// Initializes a new <see cref="SubflowReference"/>
        /// </summary>
        /// <param name="workflowId">The id of the <see cref="WorkflowDefinition"/> to run</param>
        /// <param name="waitForCompletion">A boolean indicating whether or not to wait for the completion of the <see cref="WorkflowDefinition"/> to run. Defaults to true</param>
        public SubflowReference(string workflowId, bool waitForCompletion = true)
            : this(workflowId, null, waitForCompletion)
        {

        }

        /// <summary>
        /// Gets/sets the id of the <see cref="WorkflowDefinition"/> to run
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty(PropertyName = "workflowId"), Newtonsoft.Json.JsonRequired]
        [System.Text.Json.Serialization.JsonPropertyName("workflowId")]
        [YamlMember(Alias = "workflowId")]
        public virtual string WorkflowId { get; set; }

        /// <summary>
        /// Gets/sets the version of the <see cref="WorkflowDefinition"/> to run. Defaults to 'latest'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "version"), Newtonsoft.Json.JsonRequired]
        [System.Text.Json.Serialization.JsonPropertyName("version")]
        [YamlMember(Alias = "version")]
        public virtual string Version { get; set; } = "latest";

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to wait for the completion of the <see cref="WorkflowDefinition"/> to run. Defaults to true
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "waitForCompletion"), Newtonsoft.Json.JsonRequired]
        [System.Text.Json.Serialization.JsonPropertyName("waitForCompletion")]
        [YamlMember(Alias = "waitForCompletion")]
        public virtual bool WaitForCompletion { get; set; } = true;

        /// <summary>
        /// Parses the specified input into a new <see cref="SubflowReference"/>
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <returns>A new <see cref="SubflowReference"/></returns>
        public static SubflowReference Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            string[] components = input.Split(":", StringSplitOptions.RemoveEmptyEntries);
            string workflowId = components.First();
            string version = null;
            if (components.Length > 1)
                version = components.Last();
            return new SubflowReference(workflowId, version);
        }

    }

}