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
using System.Linq;

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents a workflow state that executes a set of defined actions or workflows for each element of a data array
    /// </summary>
    [DiscriminatorValue(StateType.ForEach)]
    [ProtoContract]
    [DataContract]
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
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string InputCollection { get; set; }

        /// <summary>
        /// Gets/sets an expression specifying an array element of the states data to add the results of each iteration
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string OutputCollection { get; set; }

        /// <summary>
        /// Gets/sets the name of the iteration parameter that can be referenced in actions/workflow. For each parallel iteration, this param should contain an unique element of the array referenced by the  <see cref="InputCollection"/> expression
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "iterationParam")]
        [System.Text.Json.Serialization.JsonPropertyName("iterationParam")]
        [YamlMember(Alias = "iterationParam")]
        [ProtoMember(3, Name = "iterationParam")]
        [DataMember(Order = 3, Name = "iterationParam")]
        public virtual string IterationParameter { get; set; }

        /// <summary>
        /// Gets/sets a uint that specifies how upper bound on how many iterations may run in parallel
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        public virtual uint? Max { get; set; }

        /// <summary>
        /// Gets/sets a value used to configure the way the actions of each iterations should be executed
        /// </summary>
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        public virtual ActionExecutionMode ActionMode { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> of actions to be executed for each of the elements of the <see cref="InputCollection"/>
        /// </summary>
        [ProtoMember(6)]
        [DataMember(Order = 6)]
        public virtual List<ActionDefinition> Actions { get; set; } = new List<ActionDefinition>();

        /// <summary>
        /// Gets/sets the unique Id of a workflow to be executed for each of the elements of <see cref="InputCollection"/>
        /// </summary>
        [ProtoMember(7)]
        [DataMember(Order = 7)]
        public virtual string WorkflowId { get; set; }

        /// <summary>
        /// Gets the <see cref="ActionDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="ActionDefinition"/> to get</param>
        /// <returns>The <see cref="ActionDefinition"/> with the specified name</returns>
        public virtual ActionDefinition GetAction(string name)
        {
            return this.Actions.FirstOrDefault(s => s.Name == name);
        }

        /// <summary>
        /// Attempts to get the <see cref="ActionDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="ActionDefinition"/> to get</param>
        /// <param name="action">The <see cref="ActionDefinition"/> with the specified name</param>
        /// <returns>A boolean indicating whether or not a <see cref="ActionDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetAction(string name, out ActionDefinition action)
        {
            action = this.GetAction(name);
            return action != null;
        }

        /// <summary>
        /// Attempts to get the next <see cref="ActionDefinition"/> in the pipeline
        /// </summary>
        /// <param name="previousActionName">The name of the <see cref="ActionDefinition"/> to get the next <see cref="ActionDefinition"/> for</param>
        /// <param name="action">The next <see cref="ActionDefinition"/>, if any</param>
        /// <returns>A boolean indicating whether or not there is a next <see cref="ActionDefinition"/> in the pipeline</returns>
        public virtual bool TryGetNextAction(string previousActionName, out ActionDefinition action)
        {
            action = null;
            ActionDefinition previousAction = this.Actions.FirstOrDefault(a => a.Name == previousActionName);
            int previousActionIndex = this.Actions.ToList().IndexOf(previousAction);
            int nextIndex = previousActionIndex + 1;
            if (nextIndex >= this.Actions.Count)
                return false;
            action = this.Actions.ElementAt(nextIndex);
            return true;
        }

    }

}
