﻿// Copyright © 2023-Present The Serverless Workflow Specification Authors
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
/// Defines the fundamentals of a service used to build <see cref="StateOutcomeDefinition"/>s
/// </summary>
public interface IStateOutcomeBuilder
{

    /// <summary>
    /// Transitions to the specified state definition
    /// </summary>
    /// <param name="stateName">The name of the state definition to transition to</param>
    /// <returns>A new <see cref="IStateBuilder{TState}"/> used to configure the state definition to transition to</returns>
    void TransitionTo(string stateName);

    /// <summary>
    /// Transitions to the specified state definition
    /// </summary>
    /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the state definition to transition to</param>
    /// <returns>A new <see cref="IStateBuilder{TState}"/> used to configure the state definition to transition to</returns>
    void TransitionTo(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

    /// <summary>
    /// Configure the state definition to end the workflow
    /// </summary>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    void End();

    /// <summary>
    /// Builds the <see cref="StateOutcomeDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="StateOutcomeDefinition"/></returns>
    StateOutcomeDefinition Build();

}
