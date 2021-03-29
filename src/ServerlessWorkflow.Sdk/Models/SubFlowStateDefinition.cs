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
using ServerlessWorkflow.Sdk.Serialization;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a workflow state used to execute a workflow
    /// </summary>
    [DiscriminatorValue(StateType.SubFlow)]
    public class SubFlowStateDefinition
        : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="SubFlowStateDefinition"/>
        /// </summary>
        public SubFlowStateDefinition()
            : base(StateType.SubFlow)
        {

        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to wait for the instantiated workflow to complete before transitioning to next state
        /// </summary>
        public virtual bool WaitForCompletion { get; set; }

        /// <summary>
        /// Gets/sets the unique identifier of the workflow to execute
        /// </summary>
        public virtual string WorkflowId { get; set; }

        /// <summary>
        /// Gets/sets an object used to define repetition (looping)
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "repeat")]
        [System.Text.Json.Serialization.JsonPropertyName("repeat")]
        [YamlMember(Alias = "repeat")]
        public virtual SubFlowLoopDefinition Loop { get; set; }

    }

}
