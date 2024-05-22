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
/// Defines the fundamentals of a service used to create <see cref="ITaskDefinitionBuilder"/>s
/// </summary>
public interface IGenericTaskDefinitionBuilder
{

    /// <summary>
    /// Configures the task to call the specified function
    /// </summary>
    /// <param name="function">The name of the function to call</param>
    /// <returns>A new <see cref="ICallTaskDefinitionBuilder"/></returns>
    ICallTaskDefinitionBuilder Call(string? function = null);

    /// <summary>
    /// Configures the task to emit the specified event
    /// </summary>
    /// <param name="e">The event to emit</param>
    /// <returns>The configured <see cref="IEmitTaskDefinitionBuilder"/></returns>
    IEmitTaskDefinitionBuilder Emit(EventDefinition e);

    /// <summary>
    /// Configures the task to emit the specified event
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the event to emit</param>
    /// <returns>The configured <see cref="IEmitTaskDefinitionBuilder"/></returns>
    IEmitTaskDefinitionBuilder Emit(Action<IEventDefinitionBuilder> setup);

    /// <summary>
    /// Configures the task to iterate over a collection and perform a task for each of the items it contains
    /// </summary>
    /// <returns>A new <see cref="IForTaskDefinitionBuilder"/></returns>
    IForTaskDefinitionBuilder For();

    /// <summary>
    /// Configures the task to listen for events
    /// </summary>
    /// <returns>A new <see cref="IForTaskDefinitionBuilder"/></returns>
    IListenTaskDefinitionBuilder Listen();

    /// <summary>
    /// Configures the task to perform a list of subtasks
    /// </summary>
    /// <returns>A new <see cref="ICompositeTaskDefinitionBuilder"/></returns>
    ICompositeTaskDefinitionBuilder Execute();

    /// <summary>
    /// Configures the task to raise the specified error
    /// </summary>
    /// <param name="error">The error to raise</param>
    /// <returns>The configured <see cref="IRaiseTaskDefinitionBuilder"/></returns>
    IRaiseTaskDefinitionBuilder Raise(ErrorDefinition error);

    /// <summary>
    /// Configures the task to raise the specified error
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the error to raise</param>
    /// <returns>The configured <see cref="IRaiseTaskDefinitionBuilder"/></returns>
    IRaiseTaskDefinitionBuilder Raise(Action<IErrorDefinitionBuilder> setup);

    /// <summary>
    /// Configures the task to run a process
    /// </summary>
    /// <returns>A new <see cref="IForTaskDefinitionBuilder"/></returns>
    IRunTaskDefinitionBuilder Run();

    /// <summary>
    /// Sets the specified variable
    /// </summary>
    /// <param name="name">The name of the variable to set</param>
    /// <param name="value">The value of the variable to set. Supports runtime expressions</param>
    /// <returns>A new <see cref="ISetTaskDefinitionBuilder"/></returns>
    ISetTaskDefinitionBuilder Set(string name, string value);

    /// <summary>
    /// Configures the task to set the specified variable
    /// </summary>
    /// <param name="variables">A name/value mapping of the variables to set. Supports runtime expressions</param>
    /// <returns>A new <see cref="ISetTaskDefinitionBuilder"/></returns>
    ISetTaskDefinitionBuilder Set(IDictionary<string, object>? variables = null);

    /// <summary>
    /// Configures the task to branch the flow based on defined conditions
    /// </summary>
    /// <returns>A new <see cref="IForTaskDefinitionBuilder"/></returns>
    ISwitchTaskDefinitionBuilder Switch();

    /// <summary>
    /// Configures the task to try executing a specific task, and handle potential errors
    /// </summary>
    /// <returns></returns>
    ITryTaskDefinitionBuilder Try();

    /// <summary>
    /// Configures the task to wait a defined amount of time
    /// </summary>
    /// <param name="duration">The duration to wait for</param>
    /// <returns>A new <see cref="ISetTaskDefinitionBuilder"/></returns>
    IWaitTaskDefinitionBuilder Wait(Duration? duration = null);

    /// <summary>
    /// Builds a new <see cref="TaskDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="TaskDefinition"/></returns>
    TaskDefinition Build();

}