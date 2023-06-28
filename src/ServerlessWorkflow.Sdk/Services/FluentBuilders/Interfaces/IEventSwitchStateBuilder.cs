// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="CloudEvent"/>-based <see cref="SwitchStateDefinition"/>
/// </summary>
public interface IEventSwitchStateBuilder
    : ISwitchStateBuilder
{

    /// <summary>
    /// Sets the duration after which the <see cref="SwitchStateDefinition"/>'s execution times out
    /// </summary>
    /// <param name="duration">The duration after which the <see cref="SwitchStateDefinition"/>'s execution times out</param>
    /// <returns>The configured <see cref="IDataSwitchCaseBuilder"/></returns>
    IEventSwitchStateBuilder TimeoutAfter(TimeSpan duration);

    /// <summary>
    /// Creates and configures a new data-based <see cref="SwitchCaseDefinition"/>
    /// </summary>
    /// <param name="caseBuilder">The <see cref="Action{T}"/> used to build the <see cref="CloudEvent"/>-based <see cref="SwitchCaseDefinition"/></param>
    /// <returns>The configured <see cref="IEventSwitchStateBuilder"/></returns>
    IEventSwitchStateBuilder WithCase(Action<IEventSwitchCaseBuilder> caseBuilder);

    /// <summary>
    /// Creates and configures a new data-based <see cref="SwitchCaseDefinition"/>
    /// </summary>
    /// <param name="name">The name of the case to add</param>
    /// <param name="caseBuilder">The <see cref="Action{T}"/> used to build the <see cref="CloudEvent"/>-based <see cref="SwitchCaseDefinition"/></param>
    /// <returns>The configured <see cref="IEventSwitchStateBuilder"/></returns>
    IEventSwitchStateBuilder WithCase(string name, Action<IEventSwitchCaseBuilder> caseBuilder);

}
