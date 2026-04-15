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
public sealed class ListenerDefinitionBuilder(EventConsumptionStrategyDefinition? to = null)
    : ListenerTargetDefinitionBuilder, IListenerDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the initial target value
    /// </summary>
    readonly EventConsumptionStrategyDefinition? initialTo = to;

    /// <summary>
    /// Gets/sets the read mode
    /// </summary>
    string? readMode;

    /// <inheritdoc/>
    public IListenerDefinitionBuilder Read(string readMode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(readMode);
        this.readMode = readMode;
        IListenerDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public new ListenerDefinition Build()
    {
        var target = initialTo ?? base.Build() ?? throw new NullReferenceException("The listener's target must be set");
        return new()
        {
            To = target,
            Read = readMode
        };
    }

}
