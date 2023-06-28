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
/// Represents the default implementation of the <see cref="ICallbackStateBuilder"/> interface
/// </summary>
public class CallbackStateBuilder
    : StateBuilder<CallbackStateDefinition>, ICallbackStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="CallbackStateBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    public CallbackStateBuilder(IPipelineBuilder pipeline)
        : base(pipeline)
    {

    }

    /// <inheritdoc/>
    public virtual ICallbackStateBuilder Execute(Action<IActionBuilder> actionSetup)
    {
        if (actionSetup == null) throw new ArgumentNullException(nameof(actionSetup));
        var builder = new ActionBuilder(this.Pipeline);
        actionSetup(builder);
        var action = builder.Build();
        return this.Execute(action);
    }

    /// <inheritdoc/>
    public ICallbackStateBuilder Execute(string name, Action<IActionBuilder> actionSetup)
    {
        if (actionSetup == null) throw new ArgumentNullException(nameof(actionSetup));
        var builder = new ActionBuilder(this.Pipeline);
        builder.WithName(name);
        actionSetup(builder);
        var action = builder.Build();
        return this.Execute(action);
    }

    /// <inheritdoc/>
    public virtual ICallbackStateBuilder Execute(ActionDefinition action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));
        this.State.Action = action;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICallbackStateBuilder FilterPayload(string expression)
    {
        this.State.EventDataFilter.Data = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICallbackStateBuilder ToStateData(string expression)
    {
        this.State.EventDataFilter.ToStateData = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICallbackStateBuilder On(string e)
    {
        if (string.IsNullOrWhiteSpace(e))
            throw new ArgumentNullException(nameof(e));
        this.State.EventRef = e;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICallbackStateBuilder On(Action<IEventBuilder> eventSetup)
    {
        if (eventSetup == null)
            throw new ArgumentNullException(nameof(eventSetup));
        this.State.EventRef = this.Pipeline.AddEvent(eventSetup).Name;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICallbackStateBuilder On(EventDefinition e)
    {
        if (e == null)
            throw new ArgumentNullException(nameof(e));
        this.Pipeline.AddEvent(e);
        this.State.EventRef = e.Name;
        return this;
    }

}
