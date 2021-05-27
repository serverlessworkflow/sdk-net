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
using CloudNative.CloudEvents;
using ServerlessWorkflow.Sdk.Models;
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="ActionDefinition"/>s of type <see cref="ActionType.Trigger"/>
    /// </summary>
    public interface IEventTriggerActionBuilder
    {

        /// <summary>
        /// Configures the <see cref="ActionDefinition"/> to produce the specified <see cref="EventDefinition"/> when triggered
        /// </summary>
        /// <param name="e">The reference name of the <see cref="EventDefinition"/> to produce. Requires the referenced <see cref="EventDefinition"/> to have been previously defined.</param>
        /// <returns>The configured <see cref="IEventTriggerActionBuilder"/></returns>
        IEventTriggerActionBuilder ThenProduce(string e);

        /// <summary>
        /// Adds the specified context attribute to the <see cref="CloudEvent"/> produced as a result of the trigger
        /// </summary>
        /// <param name="name">The name of the <see cref="CloudEvent"/> context attribute to add</param>
        /// <param name="value">The value of the <see cref="CloudEvent"/> context attribute to add</param>
        /// <returns>The configured <see cref="IEventTriggerActionBuilder"/></returns>
        IEventTriggerActionBuilder WithContextAttribute(string name, string value);

        /// <summary>
        /// Adds the specified context attribute to the <see cref="CloudEvent"/> produced as a result of the trigger
        /// </summary>
        /// <param name="contextAttributes">An <see cref="IDictionary{TKey, TValue}"/> containing the context attributes to add to the <see cref="CloudEvent"/>e produced as a result of the trigger</param>
        /// <returns>The configured <see cref="IEventTriggerActionBuilder"/></returns>
        IEventTriggerActionBuilder WithContextAttribute(IDictionary<string, string> contextAttributes);

        /// <summary>
        /// Builds the <see cref="ActionDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="ActionDefinition"/></returns>
        ActionDefinition Build();

    }

}
