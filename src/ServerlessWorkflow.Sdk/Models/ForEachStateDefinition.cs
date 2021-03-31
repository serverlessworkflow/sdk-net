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
using System.Collections.Generic;
using ServerlessWorkflow.Sdk.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents a workflow state that executes a set of defined actions or workflows for each element of a data array
    /// </summary>
    [DiscriminatorValue(StateType.ForEach)]
    public class ForEachStateDefinition
        : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="ForEachStateDefinition"/>
        /// </summary>
        public ForEachStateDefinition()
            : base(StateType.ForEach)
        {

        }

        /// <summary>
        /// gets/sets an expression selecting an array element of the states data
        /// </summary>
        public virtual string InputCollection { get; set; }

        /// <summary>
        /// Gets/sets an expression specifying an array element of the states data to add the results of each iteration
        /// </summary>
        public virtual string OutputCollection { get; set; }

        /// <summary>
        /// Gets/sets the name of the iteration parameter that can be referenced in actions/workflow. For each parallel iteration, this param should contain an unique element of the array referenced by the  <see cref="InputCollection"/> expression
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "iterationParam")]
        [System.Text.Json.Serialization.JsonPropertyName("iterationParam")]
        [YamlMember(Alias = "iterationParam")]
        public virtual string IterationParameter { get; set; }

        /// <summary>
        /// Gets/sets a uint that specifies how upper bound on how many iterations may run in parallel
        /// </summary>
        public virtual uint? Max { get; set; }

        /// <summary>
        /// Gets/sets a value used to configure the way the actions of each iterations should be executed
        /// </summary>
        public virtual ActionExecutionMode ActionMode { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> of actions to be executed for each of the elements of the <see cref="InputCollection"/>
        /// </summary>
        public virtual List<ActionDefinition> Actions { get; set; } = new List<ActionDefinition>();

        /// <summary>
        /// Gets/sets the unique Id of a workflow to be executed for each of the elements of <see cref="InputCollection"/>
        /// </summary>
        public virtual string WorkflowId { get; set; }

    }

}
