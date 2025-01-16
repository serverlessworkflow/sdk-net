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
public class SubscriptionIteratorDefinitionBuilder
    : ISubscriptionIteratorDefinitionBuilder
{

    /// <summary>
    /// Gets the <see cref="SubscriptionIteratorDefinition"/> to configure
    /// </summary>
    protected SubscriptionIteratorDefinition Iterator { get; } = new();

    /// <inheritdoc/>
    public virtual ISubscriptionIteratorDefinitionBuilder Item(string item)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(item);
        this.Iterator.Item = item;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISubscriptionIteratorDefinitionBuilder At(string at)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(at);
        this.Iterator.At = at;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISubscriptionIteratorDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        this.Iterator.Do = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual ISubscriptionIteratorDefinitionBuilder Output(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        this.Iterator.Output = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual ISubscriptionIteratorDefinitionBuilder Export(Action<IOutputDataModelDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new OutputDataModelDefinitionBuilder();
        setup(builder);
        this.Iterator.Export = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual SubscriptionIteratorDefinition Build() => this.Iterator;

}