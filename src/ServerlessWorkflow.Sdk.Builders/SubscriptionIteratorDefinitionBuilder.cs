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
/// Represents the default implementation of the <see cref="ISubscriptionIteratorDefinitionBuilder"/> interface
/// </summary>
public sealed class SubscriptionIteratorDefinitionBuilder
    : ISubscriptionIteratorDefinitionBuilder
{

    string? itemValue;
    string? atValue;
    Map<string, TaskDefinition>? doTasks;
    OutputDataModelDefinition? outputValue;
    OutputDataModelDefinition? exportValue;

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Item(string item)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(item);
        itemValue = item;
        return this;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder At(string at)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(at);
        atValue = at;
        return this;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        doTasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Output(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        outputValue = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public ISubscriptionIteratorDefinitionBuilder Export(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        exportValue = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public SubscriptionIteratorDefinition Build() => new()
    {
        Item = itemValue,
        At = atValue,
        Do = doTasks,
        Output = outputValue,
        Export = exportValue
    };

}
