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
/// Defines the fundamentals of a service used to build <see cref="SwitchCaseDefinition"/>s
/// </summary>
public interface ISwitchCaseDefinitionBuilder
{

    /// <summary>
    /// Sets a runtime expression that defines whether or not the case applies
    /// </summary>
    /// <param name="expression">A runtime expression that defines whether or not the case applies</param>
    /// <returns>The configured <see cref="ISwitchCaseDefinitionBuilder"/></returns>
    ISwitchCaseDefinitionBuilder When(string expression);

    /// <summary>
    /// Sets the flow directive to execute when the case is matched
    /// </summary>
    /// <param name="directive">The flow directive to execute</param>
    /// <returns>The configured <see cref="ISwitchCaseDefinitionBuilder"/></returns>
    ISwitchCaseDefinitionBuilder Then(string directive);

    /// <summary>
    /// Builds the configured <see cref="SwitchCaseDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="SwitchCaseDefinition"/></returns>
    SwitchCaseDefinition Build();

}
