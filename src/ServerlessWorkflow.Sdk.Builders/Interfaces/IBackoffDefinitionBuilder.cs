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
/// Defines the fundamentals of a service used to build <see cref="BackoffDefinition"/>s
/// </summary>
public interface IBackoffDefinitionBuilder
{

    /// <summary>
    /// Builds the configured <see cref="BackoffDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="BackoffDefinition"/></returns>
    BackoffDefinition Build();

}

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="BackoffDefinition"/>s
/// </summary>
/// <typeparam name="TDefinition">The type of <see cref="BackoffDefinition"/> to build</typeparam>
public interface IBackoffDefinitionBuilder<TDefinition>
    : IBackoffDefinitionBuilder
    where TDefinition : BackoffDefinition
{

    /// <summary>
    /// Builds the configured <see cref="BackoffDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="BackoffDefinition"/></returns>
    new TDefinition Build();

}