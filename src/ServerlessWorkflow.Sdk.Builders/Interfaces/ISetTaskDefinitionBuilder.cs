// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="SetTaskDefinition"/>s
/// </summary>
public interface ISetTaskDefinitionBuilder
    : ITaskDefinitionBuilder<ISetTaskDefinitionBuilder, SetTaskDefinition>
{

    /// <summary>
    /// Sets the specified variable
    /// </summary>
    /// <param name="name">The name of the variable to set</param>
    /// <param name="value">The value of the variable to set. Supports runtime expressions</param>
    /// <returns>The configured <see cref="ISetTaskDefinitionBuilder"/></returns>
    ISetTaskDefinitionBuilder Set(string name, object value);

    /// <summary>
    /// Configures the task to set the specified variable
    /// </summary>
    /// <param name="variables">A name/value mapping of the variables to set</param>
    /// <returns>The configured <see cref="ISetTaskDefinitionBuilder"/></returns>
    ISetTaskDefinitionBuilder Set(IDictionary<string, object> variables);

}
