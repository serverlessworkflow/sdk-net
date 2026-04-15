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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the configuration of a process execution
/// </summary>
[Description("Represents the configuration of a process execution")]
[DataContract]
public sealed record ProcessTypeDefinition
{

    /// <summary>
    /// Gets/sets the configuration of the container to run
    /// </summary>
    [Description("The configuration of the container to run")]
    [DataMember(Order = 1, Name = "container"), JsonPropertyOrder(1), JsonPropertyName("container")]
    public ContainerProcessDefinition? Container { get; init; }

    /// <summary>
    /// Gets/sets the configuration of the shell command to run
    /// </summary>
    [Description("The configuration of the shell command to run")]
    [DataMember(Order = 2, Name = "shell"), JsonPropertyOrder(2), JsonPropertyName("shell")]
    public ShellProcessDefinition? Shell { get; init; }

    /// <summary>
    /// Gets/sets the configuration of the script to run
    /// </summary>
    [Description("The configuration of the script to run")]
    [DataMember(Order = 3, Name = "script"), JsonPropertyOrder(3), JsonPropertyName("script")]
    public ScriptProcessDefinition? Script { get; init; }

    /// <summary>
    /// Gets/sets the configuration of the workflow to run
    /// </summary>
    [Description("The configuration of the workflow to run")]
    [DataMember(Order = 4, Name = "workflow"), JsonPropertyOrder(4), JsonPropertyName("workflow")]
    public WorkflowProcessDefinition? Workflow { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to await the process completion before continuing. Defaults to 'true'.
    /// </summary>
    [Description("A boolean indicating whether or not to await the process completion before continuing. Defaults to 'true'.")]
    [DataMember(Order = 5, Name = "await"), JsonPropertyOrder(5), JsonPropertyName("await")]
    public bool? Await { get; init; }

    /// <summary>
    /// Gets/sets the output of the process.<para></para>
    /// See <see cref="ProcessReturnType"/><para></para>
    /// Defaults to <see cref="ProcessReturnType.Stdout"/>
    /// </summary>
    [Description("The output of the process. See ProcessReturnType. Defaults to ProcessReturnType.Stdout")]
    [DataMember(Order = 6, Name = "return"), JsonPropertyOrder(6), JsonPropertyName("return")]
    public string? Return { get; init; }

    /// <summary>
    /// Gets the type of the defined process tasks
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public string ProcessType
    {
        get
        {
            if (Container != null) return Sdk.ProcessType.Container;
            if (Shell != null) return Sdk.ProcessType.Shell;
            if (Script != null) return Sdk.ProcessType.Script;
            if (Workflow != null) return Sdk.ProcessType.Workflow;
            return Sdk.ProcessType.Extension;
        }
    }

}
