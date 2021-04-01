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

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents a workflow state that executes <see cref="BranchDefinition"/>es in parallel
    /// </summary>
    [DiscriminatorValue(StateType.Parallel)]
    public class ParallelStateDefinition
        : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="ParallelStateDefinition"/>
        /// </summary>
        public ParallelStateDefinition()
            : base(StateType.Parallel)
        {

        }

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> containing the <see cref="BranchDefinition"/>es executed by the <see cref="ParallelStateDefinition"/>
        /// </summary>
        public virtual List<BranchDefinition> Branches { get; set; } = new List<BranchDefinition>();

        /// <summary>
        /// Gets/sets a value that configures the way the <see cref="ParallelStateDefinition"/> completes. Defaults to 'And'
        /// </summary>
        public virtual ParallelCompletionType CompletionType { get; set; } = ParallelCompletionType.And;

        /// <summary>
        /// Gets/sets a value that represents the amount of <see cref="BranchDefinition"/>es to complete for completing the state, when <see cref="CompletionType"/> is set to <see cref="ParallelCompletionType.N"/>
        /// </summary>
        public virtual uint? N { get; set; }

    }

}
