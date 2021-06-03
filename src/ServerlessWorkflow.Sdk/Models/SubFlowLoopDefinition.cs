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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the definition of a subflow's loop
    /// </summary>
    public class SubFlowLoopDefinition
    {

        /// <summary>
        /// Gets/sets the expression evaluated against subflow data. Subflow will repeat execution as long as this expression is true or until the max property count is reached
        /// </summary>
        public virtual string Expression { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to perform a check before looping. If true, the expression is evaluated before each repeat execution, if false the expression is evaluated after each repeat execution. Defaults to True.
        /// </summary>
        public virtual bool CheckBefore { get; set; } = true;

        /// <summary>
        /// Gets/sets the maximum amount of repeat executions
        /// </summary>
        [Range(0, int.MaxValue)]
        public virtual int? Max { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to continue on error. If true, repeats executions in a case unhandled errors propagate from the sub-workflow to this state. Defaults to false.
        /// </summary>
        public virtual bool ContinueOnError { get; set; } = false;

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the names of all the <see cref="EventDefinition"/>s to stop looping upon consumption of
        /// </summary>
        public virtual List<string> StopOnEvents { get; set; } = new List<string>();

    }

}
