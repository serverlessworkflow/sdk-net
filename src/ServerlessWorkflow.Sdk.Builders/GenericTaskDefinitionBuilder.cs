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
public sealed class GenericTaskDefinitionBuilder
    : IGenericTaskDefinitionBuilder
{

    ITaskDefinitionBuilder? builder;

    /// <inheritdoc/>
    public ICallTaskDefinitionBuilder Call(string? function = null)
    {
        var builder = new CallTaskDefinitionBuilder(function);
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IDoTaskDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        var builder = new DoTaskDefinitionBuilder();
        builder.Do(setup);
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IEmitTaskDefinitionBuilder Emit(EventDefinition e)
    {
        ArgumentNullException.ThrowIfNull(e);
        var builder = new EmitTaskDefinitionBuilder(e);
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IEmitTaskDefinitionBuilder Emit(Action<IEventDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new EventDefinitionBuilder();
        setup(builder);
        var e = builder.Build();
        return Emit(e);
    }

    /// <inheritdoc/>
    public IForTaskDefinitionBuilder For()
    {
        var builder = new ForTaskDefinitionBuilder();
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IForkTaskDefinitionBuilder Fork()
    {
        var builder = new ForkTaskDefinitionBuilder();
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IListenTaskDefinitionBuilder Listen()
    {
        var builder = new ListenTaskDefinitionBuilder();
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IDoTaskDefinitionBuilder Execute()
    {
        var builder = new DoTaskDefinitionBuilder();
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IRaiseTaskDefinitionBuilder Raise(ErrorDefinition error)
    {
        ArgumentNullException.ThrowIfNull(error);
        var builder = new RaiseTaskDefinitionBuilder(error);
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IRaiseTaskDefinitionBuilder Raise(Action<IErrorDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ErrorDefinitionBuilder();
        setup(builder);
        var error = builder.Build();
        return Raise(error);
    }

    /// <inheritdoc/>
    public IRunTaskDefinitionBuilder Run()
    {
        var builder = new RunTaskDefinitionBuilder();
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public ISetTaskDefinitionBuilder Set(string name, string value) => Set(new() 
    { 
        { name, value } 
    });

    /// <inheritdoc/>
    public ISetTaskDefinitionBuilder Set(JsonObject? variables = null)
    {
        var builder = new SetTaskDefinitionBuilder(variables);
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public ISwitchTaskDefinitionBuilder Switch()
    {
        var builder = new SwitchTaskDefinitionBuilder();
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public ITryTaskDefinitionBuilder Try()
    {
        var builder = new TryTaskDefinitionBuilder();
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IWaitTaskDefinitionBuilder Wait(Duration? duration = null)
    {
        var builder = new WaitTaskDefinitionBuilder(duration);
        this.builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public TaskDefinition Build()
    {
        if (this.builder == null) throw new NullReferenceException();
        return this.builder.Build();
    }

}
