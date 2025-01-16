﻿// Copyright © 2024-Present The Serverless Workflow Specification Authors
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
/// Defines the fundamentals of a service used to build <see cref="ListenTaskDefinition"/>s
/// </summary>
public interface IListenTaskDefinitionBuilder
    : ITaskDefinitionBuilder<IListenTaskDefinitionBuilder, ListenTaskDefinition>
{

    /// <summary>
    /// Configures the task to listen to the specified event(s)
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the task's listener target</param>
    /// <returns>The configured <see cref="IListenTaskDefinitionBuilder"/></returns>
    IListenTaskDefinitionBuilder To(Action<IListenerDefinitionBuilder> setup);

    /// <summary>
    /// Configures the iterator used to process each consumed event
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="SubscriptionIteratorDefinition"/> to use</param>
    /// <returns>The configured <see cref="IListenTaskDefinitionBuilder"/></returns>
    IListenTaskDefinitionBuilder Foreach(Action<ISubscriptionIteratorDefinitionBuilder> setup);

}
