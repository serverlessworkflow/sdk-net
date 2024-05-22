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
/// Represents the base class for all <see cref="ITaskDefinitionBuilder{TDefinition}"/> implementations
/// </summary>
/// <typeparam name="TDefinition">The type of <see cref="TaskDefinition"/> to build</typeparam>
public abstract class TaskDefinitionBuilder<TDefinition>
    : ITaskDefinitionBuilder<TDefinition>
    where TDefinition : TaskDefinition
{

    /// <summary>
    /// Applies the configuration common to all types of tasks
    /// </summary>
    /// <param name="definition">The task definition to configure</param>
    /// <returns>The configured task definition</returns>
    protected virtual TDefinition Configure(TDefinition definition)
    {
        //todo: common properties (timeouts, etc)
        return definition;
    }

    /// <inheritdoc/>
    public abstract TDefinition Build();

    TaskDefinition ITaskDefinitionBuilder.Build() => this.Build();

}
