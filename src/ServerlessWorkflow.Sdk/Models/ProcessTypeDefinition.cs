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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the configuration of a process execution
/// </summary>
[DataContract]
public record ProcessTypeDefinition
{

    /// <summary>
    /// Gets/sets the configuration of the container to run
    /// </summary>
    [DataMember(Name = "container", Order = 1), JsonPropertyName("container"), JsonPropertyOrder(1), YamlMember(Alias = "container", Order = 1)]
    public virtual ContainerProcessDefinition? Container { get; set; }

    /// <summary>
    /// Gets/sets the configuration of the shell command to run
    /// </summary>
    [DataMember(Name = "shell", Order = 2), JsonPropertyName("shell"), JsonPropertyOrder(2), YamlMember(Alias = "shell", Order = 2)]
    public virtual ShellProcessDefinition? Shell { get; set; }

    /// <summary>
    /// Gets/sets the configuration of the script to run
    /// </summary>
    [DataMember(Name = "script", Order = 3), JsonPropertyName("script"), JsonPropertyOrder(3), YamlMember(Alias = "script", Order = 3)]
    public virtual ScriptProcessDefinition? Script { get; set; }

    /// <summary>
    /// Gets/sets the configuration of the workflow to run
    /// </summary>
    [DataMember(Name = "workflow", Order = 4), JsonPropertyName("workflow"), JsonPropertyOrder(4), YamlMember(Alias = "workflow", Order = 4)]
    public virtual WorkflowProcessDefinition? Workflow { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to await the process completion before continuing. Defaults to 'true'.
    /// </summary>
    [DataMember(Name = "await", Order = 5), JsonPropertyName("await"), JsonPropertyOrder(5), YamlMember(Alias = "await", Order = 5)]
    public virtual bool? Await { get; set; }

    /// <summary>
    /// Gets the type of the defined process tasks
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string ProcessType
    {
        get
        {
            if (this.Container != null) return Models.ProcessType.Container;
            if (this.Shell != null) return Models.ProcessType.Shell;
            if (this.Script != null) return Models.ProcessType.Script;
            if (this.Workflow != null) return Models.ProcessType.Workflow;
            return Models.ProcessType.Extension;
        }
    }

}
