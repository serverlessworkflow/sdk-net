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
/// Represents the default implementation of the <see cref="IListenerDefinitionBuilder"/> interface
/// </summary>
/// <param name="target">The listener's target</param>
public class ListenerDefinitionBuilder(EventConsumptionStrategyDefinition? target = null)
    : IListenerDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the listener's target
    /// </summary>
    protected EventConsumptionStrategyDefinition? Target { get; set; } = target;

    /// <inheritdoc/>
    public virtual IListenerDefinitionBuilder To(Action<IListenerTargetDefinitionBuilder> setup)
    {
        var builder = new ListenerTargetDefinitionBuilder();
        setup(builder);
        this.Target = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual ListenerDefinition Build()
    {
        if (this.Target == null) throw new NullReferenceException("The listener's target must be set");
        return new()
        {
            To = this.Target
        };
    }

}