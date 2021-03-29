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
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to explicitly define execution completion of a workflow instance or workflow execution path.
    /// </summary>
    public class EndDefinition
    {

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to terminate the executing workflow. If true, completes all execution flows in the given workflow instance. Defaults to false.
        /// </summary>
        public virtual bool Terminate { get; set; } = false;

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the events that should be produced
        /// </summary>
        public virtual IEnumerable<ProduceEventDefinition> ProduceEvents { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not the state should trigger compensation. Default is false.
        /// </summary>
        public virtual bool Compensate { get; set; } = false;

    }

}