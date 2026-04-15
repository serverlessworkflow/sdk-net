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
/// Represents the default implementation of the <see cref="IListenTaskDefinitionBuilder"/> interface
/// </summary>
public sealed class ListenTaskDefinitionBuilder
    : TaskDefinitionBuilder<IListenTaskDefinitionBuilder, ListenTaskDefinition>, IListenTaskDefinitionBuilder
{

    ListenTaskDefinition task = new()
    {
        Listen = null!
    };

    /// <inheritdoc/>
    public IListenTaskDefinitionBuilder To(Action<IListenerDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ListenerDefinitionBuilder();
        setup(builder);
        task = task with
        {
            Listen = builder.Build()
        };
        return this;
    }

    /// <inheritdoc/>
    public IListenTaskDefinitionBuilder Foreach(Action<ISubscriptionIteratorDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new SubscriptionIteratorDefinitionBuilder();
        setup(builder);
        task = task with
        {
            Foreach = builder.Build()
        };   
        return this;
    }

    /// <inheritdoc/>
    public override ListenTaskDefinition Build() => Configure(task);

}