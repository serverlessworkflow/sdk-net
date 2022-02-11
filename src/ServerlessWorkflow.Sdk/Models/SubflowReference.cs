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
    [ProtoContract]
    [DataContract]
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
        /// <param name="invocationMode">The subflow's <see cref="Sdk.InvocationMode"/>. Defaults to <see cref="InvocationMode.Synchronous"/>.</param>
        public SubflowReference(string workflowId, string version, InvocationMode invocationMode = InvocationMode.Synchronous)
            : this()
        {
            if (string.IsNullOrWhiteSpace(workflowId))
                throw new ArgumentNullException(nameof(workflowId));
            this.WorkflowId = workflowId;
            this.Version = version;
            this.InvocationMode = invocationMode;
        }

        /// <summary>
        /// Initializes a new <see cref="SubflowReference"/>
        /// </summary>
        /// <param name="workflowId">The id of the <see cref="WorkflowDefinition"/> to run</param>
        /// <param name="invocationMode">The subflow's <see cref="Sdk.InvocationMode"/>. Defaults to <see cref="InvocationMode.Synchronous"/>.</param>
        public SubflowReference(string workflowId, InvocationMode invocationMode = InvocationMode.Synchronous)
            : this(workflowId, null!, invocationMode)
        {

        }

        /// <summary>
        /// Gets/sets the id of the <see cref="WorkflowDefinition"/> to run
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(1)]
        [DataMember(Order = 1, IsRequired = true)]
        public virtual string WorkflowId { get; set; } = null!;

        /// <summary>
        /// Gets/sets the version of the <see cref="WorkflowDefinition"/> to run. Defaults to 'latest'
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string? Version { get; set; } = "latest";

        /// <summary>
        /// Gets/sets the subflow's <see cref="Sdk.InvocationMode"/>. Defaults to <see cref="InvocationMode.Synchronous"/>.
        /// </summary>
        /// <remarks>
        /// Default value of this property is sync, meaning that workflow execution should wait until the subflow completes.<para></para>
        /// If set to async, workflow execution should just invoke the subflow and not wait for its results. Note that in this case the action does not produce any results, and the associated actions actionDataFilter as well as its retry definition, if defined, should be ignored.<para></para>
        /// Subflows that are invoked async do not propagate their errors to the associated action definition and the workflow state, meaning that any errors that happen during their execution cannot be handled in the workflow states onErrors definition.<para></para>
        /// Note that errors raised during subflows that are invoked async should not fail workflow execution.
        /// </remarks>
        [Newtonsoft.Json.JsonProperty(PropertyName = "invoke")]
        [System.Text.Json.Serialization.JsonPropertyName("invoke")]
        [YamlMember(Alias = "invoke")]
        [ProtoMember(3, Name = "invoke")]
        [DataMember(Order = 3, Name = "invoke")]
        public virtual InvocationMode InvocationMode { get; set; } = InvocationMode.Synchronous;

        /// <summary>
        /// Parses the specified input into a new <see cref="SubflowReference"/>
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <returns>A new <see cref="SubflowReference"/></returns>
        public static SubflowReference Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            var components = input.Split(":", StringSplitOptions.RemoveEmptyEntries);
            var workflowId = components.First();
            var version = null as string;
            if (components.Length > 1)
                version = components.Last();
            return new SubflowReference(workflowId, version!);
        }

    }

}