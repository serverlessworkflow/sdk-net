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
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerlessWorkflow.Sdk
{
    /// <summary>
    /// Defines extensions for <see cref="WorkflowDefinition"/>s
    /// </summary>
    public static class WorkflowDefinitionExtensions
    {

        /// <summary>
        /// Gets all the <see cref="ActionDefinition"/>s of the specified type declared in the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to query</param>
        /// <param name="type">The type of <see cref="ActionDefinition"/>s to get. A null value gets all <see cref="ActionDefinition"/>s</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="ActionDefinition"/>s of the specified type declared in the <see cref="WorkflowDefinition"/></returns>
        public static IEnumerable<ActionDefinition> GetActions(this WorkflowDefinition workflow, string? type = null)
        {
            var actions = workflow.States.SelectMany(s => s switch
            {
                CallbackStateDefinition callbackState => new ActionDefinition[] { callbackState.Action! },
                EventStateDefinition eventState => eventState.Triggers.SelectMany(t => t.Actions),
                ForEachStateDefinition foreachState => foreachState.Actions,
                OperationStateDefinition operationState => operationState.Actions,
                ParallelStateDefinition parallelState => parallelState.Branches.SelectMany(b => b.Actions),
                _ => Array.Empty<ActionDefinition>()
            });
            if (!string.IsNullOrWhiteSpace(type)) actions = actions.Where(a => a.Type == type);
            return actions;
        }

        /// <summary>
        /// Gets all the <see cref="FunctionReference"/>s declared in the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to query</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="FunctionReference"/>s declared in the <see cref="WorkflowDefinition"/></returns>
        public static IEnumerable<FunctionReference> GetFunctionReferences(this WorkflowDefinition workflow)
        {
            return workflow.GetActions(ActionType.Function).Select(a => a.Function)!;
        }

        /// <summary>
        /// Gets all the <see cref="EventReference"/>s declared in the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to query</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="EventReference"/>s declared in the <see cref="WorkflowDefinition"/></returns>
        public static IEnumerable<EventReference> GetEventReferences(this WorkflowDefinition workflow)
        {
            return workflow.GetActions(ActionType.Trigger).Select(a => a.Event)!;
        }

        /// <summary>
        /// Gets all the <see cref="SubflowReference"/>s declared in the <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to query</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="SubflowReference"/>s declared in the <see cref="WorkflowDefinition"/></returns>
        public static IEnumerable<SubflowReference> GetSubflowReferences(this WorkflowDefinition workflow)
        {
            return workflow.GetActions(ActionType.Subflow).Select(a => a.Subflow)!;
        }

    }

}
