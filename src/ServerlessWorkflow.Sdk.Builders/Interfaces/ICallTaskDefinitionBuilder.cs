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
/// Defines the fundamentals of a service used to build <see cref="CallTaskDefinition"/>s
/// </summary>
public interface ICallTaskDefinitionBuilder
{

    /// <summary>
    /// Configures the task to call the specified function
    /// </summary>
    /// <param name="name">The name of the function to call</param>
    /// <returns>The configured <see cref="ICallTaskDefinitionBuilder"/></returns>
    ICallTaskDefinitionBuilder Function(string name);

    /// <summary>
    /// Adds a new argument to call the function with
    /// </summary>
    /// <param name="name">The argument's name</param>
    /// <param name="value">The argument's value</param>
    /// <returns>The configured <see cref="ICallTaskDefinitionBuilder"/></returns>
    ICallTaskDefinitionBuilder With(string name, object value);

    /// <summary>
    /// Sets the arguments to call the function with
    /// </summary>
    /// <param name="arguments">A name/value mapping of the arguments to call the function with</param>
    /// <returns>The configured <see cref="ICallTaskDefinitionBuilder"/></returns>
    ICallTaskDefinitionBuilder With(IDictionary<string, object> arguments);

}
