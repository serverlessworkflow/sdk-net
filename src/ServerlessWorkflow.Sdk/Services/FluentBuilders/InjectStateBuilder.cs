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
using Neuroglia;
using ServerlessWorkflow.Sdk.Models;
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IInjectStateBuilder"/> interface
    /// </summary>
    public class InjectStateBuilder
        : StateBuilder<InjectStateDefinition>, IInjectStateBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="InjectStateBuilder"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
        public InjectStateBuilder(IPipelineBuilder pipeline)
            : base(pipeline)
        {

        }

        /// <inheritdoc/>
        public virtual IInjectStateBuilder Data(object data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            this.State.Data = data is Any any ? any : new(data.ToDictionary());
            return this;
        }

    }

}
