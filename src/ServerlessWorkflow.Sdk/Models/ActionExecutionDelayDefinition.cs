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

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents an object used to configure an <see cref="ActionDefinition"/>'s execution delay
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class ActionExecutionDelayDefinition
    {

        /// <summary>
        /// Gets/sets the amount of time to wait before executing the configured <see cref="ActionDefinition"/>
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual TimeSpan? Before { get; set; }

        /// <summary>
        /// Gets/sets the amount of time to wait after having executed the configured <see cref="ActionDefinition"/>
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual TimeSpan? After { get; set; }

    }

}