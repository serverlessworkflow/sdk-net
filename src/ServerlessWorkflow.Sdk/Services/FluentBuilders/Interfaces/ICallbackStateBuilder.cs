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
/// Defines the fundamentals of the service used to build <see cref="CallbackStateDefinition"/>s
/// </summary>
public interface ICallbackStateBuilder
    : IStateBuilder<CallbackStateDefinition>
{

    /// <summary>
    /// Configures the <see cref="CallbackStateDefinition"/> to execute the specified <see cref="ActionDefinition"/> upon consumption of the callback <see cref="CloudEvent"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="ActionDefinition"/> to build</param>
    /// <param name="actionSetup">The <see cref="Action{T}"/> used to create the <see cref="ActionDefinition"/> to execute</param>
    /// <returns>The configured <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder Execute(string name, Action<IActionBuilder> actionSetup);

    /// <summary>
    /// Configures the <see cref="CallbackStateDefinition"/> to execute the specified <see cref="ActionDefinition"/> upon consumption of the callback <see cref="CloudEvent"/>
    /// </summary>
    /// <param name="actionSetup">The <see cref="Action{T}"/> used to create the <see cref="ActionDefinition"/> to execute</param>
    /// <returns>The configured <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder Execute(Action<IActionBuilder> actionSetup);

    /// <summary>
    /// Configures the <see cref="CallbackStateDefinition"/> to execute the specified <see cref="ActionDefinition"/> upon consumption of the callback <see cref="CloudEvent"/>
    /// </summary>
    /// <param name="action">The <see cref="ActionDefinition"/> to execute</param>
    /// <returns>The configured <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder Execute(ActionDefinition action);

    /// <summary>
    /// Configures the <see cref="CallbackStateDefinition"/> to wait for the consumption of a <see cref="CloudEvent"/> defined by specified <see cref="EventDefinition"/>
    /// </summary>
    /// <param name="e">The reference name of the <see cref="EventDefinition"/> that defines the <see cref="CloudEvent"/> to consume</param>
    /// <returns>The configured <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder On(string e);

    /// <summary>
    /// Configures the <see cref="CallbackStateDefinition"/> to wait for the consumption of a <see cref="CloudEvent"/> defined by specified <see cref="EventDefinition"/>
    /// </summary>
    /// <param name="eventSetup">The <see cref="Action{T}"/> used to build the <see cref="EventDefinition"/> that defines the <see cref="CloudEvent"/> to consume</param>
    /// <returns>The configured <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder On(Action<IEventBuilder> eventSetup);

    /// <summary>
    /// Configures the <see cref="CallbackStateDefinition"/> to wait for the consumption of a <see cref="CloudEvent"/> defined by specified <see cref="EventDefinition"/>
    /// </summary>
    /// <param name="e">The <see cref="EventDefinition"/> that defines the <see cref="CloudEvent"/> to consume</param>
    /// <returns>The configured <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder On(EventDefinition e);

    /// <summary>
    /// Configures the <see cref="CallbackStateDefinition"/> to filter the payload of the callback <see cref="CloudEvent"/>
    /// </summary>
    /// <param name="expression">The workflow expression used to filter payload of the callback <see cref="CloudEvent"/></param>
    /// <returns>The configured <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder FilterPayload(string expression);

    /// <summary>
    /// Configures the <see cref="CallbackStateDefinition"/> to filter the payload of the callback <see cref="CloudEvent"/>
    /// </summary>
    /// <param name="expression">The expression that selects a state data element to which the action results should be added/merged into</param>
    /// <returns>The configured <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder ToStateData(string expression);

}
