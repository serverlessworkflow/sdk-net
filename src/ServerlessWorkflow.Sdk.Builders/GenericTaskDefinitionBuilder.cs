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
/// Represents the default implementation of the <see cref="IGenericTaskDefinitionBuilder"/> interface
/// </summary>
public class GenericTaskDefinitionBuilder
    : IGenericTaskDefinitionBuilder
{

    /// <summary>
    /// Gets the underlying <see cref="ITaskDefinitionBuilder"/>
    /// </summary>
    protected ITaskDefinitionBuilder? Builder { get; set; }

    /// <inheritdoc/>
    public virtual ICallTaskDefinitionBuilder Call(string? function = null)
    {
        var builder = new CallTaskDefinitionBuilder(function);
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IEmitTaskDefinitionBuilder Emit(EventDefinition e)
    {
        ArgumentNullException.ThrowIfNull(e);
        var builder = new EmitTaskDefinitionBuilder(e);
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IEmitTaskDefinitionBuilder Emit(Action<IEventDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new EventDefinitionBuilder();
        setup(builder);
        var e = builder.Build();
        return this.Emit(e);
    }

    /// <inheritdoc/>
    public virtual IForTaskDefinitionBuilder For()
    {
        var builder = new ForTaskDefinitionBuilder();
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IListenTaskDefinitionBuilder Listen()
    {
        var builder = new ListenTaskDefinitionBuilder();
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual ICompositeTaskDefinitionBuilder Execute()
    {
        var builder = new CompositeTaskDefinitionBuilder();
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IRaiseTaskDefinitionBuilder Raise(ErrorDefinition error)
    {
        ArgumentNullException.ThrowIfNull(error);
        var builder = new RaiseTaskDefinitionBuilder(error);
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IRaiseTaskDefinitionBuilder Raise(Action<IErrorDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ErrorDefinitionBuilder();
        setup(builder);
        var error = builder.Build();
        return this.Raise(error);
    }

    /// <inheritdoc/>
    public virtual IRunTaskDefinitionBuilder Run()
    {
        var builder = new RunTaskDefinitionBuilder();
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual ISetTaskDefinitionBuilder Set(string name, string value) => this.Set(new Dictionary<string, object>() { { name, value } });

    /// <inheritdoc/>
    public virtual ISetTaskDefinitionBuilder Set(IDictionary<string, object>? variables = null)
    {
        var builder = new SetTaskDefinitionBuilder(variables);
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual ISwitchTaskDefinitionBuilder Switch()
    {
        var builder = new SwitchTaskDefinitionBuilder();
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual ITryTaskDefinitionBuilder Try()
    {
        var builder = new TryTaskDefinitionBuilder();
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IWaitTaskDefinitionBuilder Wait(Duration? duration = null)
    {
        var builder = new WaitTaskDefinitionBuilder(duration);
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual TaskDefinition Build()
    {
        if (this.Builder == null) throw new NullReferenceException();
        return this.Builder.Build();
    }

}
