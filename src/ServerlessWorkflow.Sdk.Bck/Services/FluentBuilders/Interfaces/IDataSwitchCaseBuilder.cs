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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build data-based <see cref="SwitchCaseDefinition"/>
    /// </summary>
    public interface IDataSwitchCaseBuilder
        : ISwitchCaseBuilder<IDataSwitchCaseBuilder>
    {

        /// <summary>
        /// Sets the <see cref="SwitchCaseDefinition"/>'s workflow expression used to evaluate the data
        /// </summary>
        /// <param name="expression">The workflow expression used to evaluate the data</param>
        /// <returns>The configured <see cref="ISwitchStateBuilder"/></returns>
        IDataSwitchCaseBuilder WithExpression(string expression);

        /// <summary>
        /// Builds the <see cref="DataCaseDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="DataCaseDefinition"/></returns>
        new DataCaseDefinition Build();

    }

}
