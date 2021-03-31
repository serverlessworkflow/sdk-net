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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="ISwitchCaseBuilder{TBuilder}"/> interface
    /// </summary>
    public abstract class SwitchCaseBuilder<TBuilder, TCase>
        : StateOutcomeBuilder, ISwitchCaseBuilder<TBuilder>
        where TBuilder : class, ISwitchCaseBuilder<TBuilder>
        where TCase : SwitchCaseDefinition, new()
    {

        /// <summary>
        /// Initializes a new <see cref="SwitchCaseBuilder{TBuilder, TCase}"/>
        /// </summary>
        /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="SwitchCaseBuilder{TBuilder, TCase}"/> belongs to</param>
        public SwitchCaseBuilder(IPipelineBuilder pipeline)
            : base(pipeline)
        {
            
        }

        /// <summary>
        /// Gets the <see cref="SwitchCaseDefinition"/> to configure
        /// </summary>
        protected TCase Case { get; } = new TCase();

        /// <inheritdoc/>
        public virtual TBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Case.Name = name;
            return (TBuilder)(object)this;
        }

    }
}
