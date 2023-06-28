// Copyright © 2023-Present The Serverless Workflow Specification Authors
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
/// Represents the default implementation of the <see cref="IEventStateBuilder"/> interface
/// </summary>
public class EventStateBuilder
    : StateBuilder<EventStateDefinition>, IEventStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="EventStateBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="EventStateBuilder"/> belongs to</param>
    public EventStateBuilder(IPipelineBuilder pipeline) : base(pipeline) { }

    /// <inheritdoc/>
    public virtual IEventStateBuilder TriggeredBy(Action<IEventStateTriggerBuilder> triggerSetup)
    {
        if (triggerSetup == null) throw new ArgumentNullException(nameof(triggerSetup));
        var builder = new EventStateTriggerBuilder(this.Pipeline);
        triggerSetup(builder);
        this.State.OnEvents.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder WaitForAll()
    {
        this.State.Exclusive = false;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder WaitForAny()
    {
        this.State.Exclusive = true;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder For(TimeSpan duration)
    {
        this.State.Timeout = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder Forever()
    {
        this.State.Timeout = null;
        return this;
    }

}
