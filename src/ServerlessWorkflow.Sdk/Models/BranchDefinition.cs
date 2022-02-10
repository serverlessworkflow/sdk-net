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
using System.Collections.Generic;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a workflow execution branch
    /// </summary>
    [DataContract]
    [ProtoContract]
    public class BranchDefinition
    {

        /// <summary>
        /// gets/sets the branch's name
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the unique id of a workflow to be executed in this branch
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string WorkflowId { get; set; }

        /// <summary>
        /// Gets/sets a value that specifies how actions are to be performed (in sequence of parallel)
        /// </summary>
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual ActionExecutionMode ActionMode { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> containing the actions to be executed in this branch
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        public virtual List<ActionDefinition> Actions { get; set; } = new List<ActionDefinition>();

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
            if (nextIndex >= this.Actions.Count())
                return false;
            action = this.Actions.ElementAt(nextIndex);
            return true;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}