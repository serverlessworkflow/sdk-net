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
/// Represents the default implementation of the <see cref="IEmitTaskDefinitionBuilder"/> interface
/// </summary>
/// <param name="e">The definition of the event to emit</param>
public class EmitTaskDefinitionBuilder(EventDefinition? e = null)
    : TaskDefinitionBuilder<IEmitTaskDefinitionBuilder, EmitTaskDefinition>, IEmitTaskDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the definition of the event to emit
    /// </summary>
    protected virtual EventDefinition? EventDefinition { get; set; } = e;

    /// <inheritdoc/>
    public virtual IEmitTaskDefinitionBuilder Event(EventDefinition e)
    {
        ArgumentNullException.ThrowIfNull(e);
        this.EventDefinition = e;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEmitTaskDefinitionBuilder Event(Action<IEventDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new EventDefinitionBuilder();
        setup(builder);
        this.EventDefinition = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public override EmitTaskDefinition Build()
    {
        if (this.EventDefinition == null) throw new NullReferenceException("The event to emit must be defined");
        return new() 
        { 
            Emit = new() 
            { 
                Event = this.EventDefinition 
            } 
        };
    }

}
