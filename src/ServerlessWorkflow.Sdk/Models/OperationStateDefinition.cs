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
using ServerlessWorkflow.Sdk.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a workflow state that defines a set of actions to be performed in sequence or in parallel. Once all actions have been performed, a transition to another state can occur.
    /// </summary>
    [DiscriminatorValue(StateType.Operation)]
    public class OperationStateDefinition
        : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="OperationStateDefinition"/>
        /// </summary>
        public OperationStateDefinition()
            : base(StateType.Operation)
        {

        }

        /// <summary>
        /// Gets/sets a value that specifies how actions are to be performed (in sequence of parallel). Defaults to sequential
        /// </summary>
        public virtual ActionExecutionMode ActionMode { get; set; } = ActionExecutionMode.Sequential;

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> of actions to be performed if expression matches
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        public virtual List<ActionDefinition> Actions { get; set; } = new List<ActionDefinition>();

    }

}
