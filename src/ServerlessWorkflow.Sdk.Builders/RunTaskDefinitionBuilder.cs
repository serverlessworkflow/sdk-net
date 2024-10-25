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

using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IRunTaskDefinitionBuilder"/> interface
/// </summary>
public class RunTaskDefinitionBuilder
    : TaskDefinitionBuilder<IRunTaskDefinitionBuilder, RunTaskDefinition>, IRunTaskDefinitionBuilder
{

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the task to build should await the execution of the defined process
    /// </summary>
    protected bool? AwaitProcess { get; set; }

    /// <summary>
    /// Gets/sets the process to run
    /// </summary>
    protected IProcessDefinitionBuilder? ProcessBuilder { get; set; }

    /// <inheritdoc/>
    public virtual IContainerProcessDefinitionBuilder Container()
    {
        var builder = new ContainerProcessDefinitionBuilder();
        this.ProcessBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IScriptProcessDefinitionBuilder Script()
    {
        var builder = new ScriptProcessDefinitionBuilder();
        this.ProcessBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IShellProcessDefinitionBuilder Shell()
    {
        var builder = new ShellProcessDefinitionBuilder();
        this.ProcessBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IWorkflowProcessDefinitionBuilder Workflow()
    {
        var builder = new WorkflowProcessDefinitionBuilder();
        this.ProcessBuilder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IRunTaskDefinitionBuilder Await(bool await)
    {
        this.AwaitProcess = await;
        return this;
    }

    /// <inheritdoc/>
    public override RunTaskDefinition Build()
    {
        if (this.ProcessBuilder == null) throw new NullReferenceException("The process to run must be set");
        var process = this.ProcessBuilder.Build();
        return this.Configure(new()
        {
            Run = new()
            {
                Container = process is ContainerProcessDefinition container ? container : null,
                Script = process is ScriptProcessDefinition script ? script : null,
                Shell = process is ShellProcessDefinition shell ? shell : null,
                Workflow = process is WorkflowProcessDefinition workflow ? workflow : null,
                Await = this.AwaitProcess
            }
        });
    }

}
