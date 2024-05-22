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
/// Represents the definition of a task execution strategy
/// </summary>
[DataContract]
public record TaskExecutionStrategyDefinition
{

    /// <summary>
    /// Gets a value indicating how the defines tasks should be executed
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string ExecutionMode => this.Sequentially?.Count > 0 ? TaskExecutionMode.Sequential : this.Concurrently?.Count > 0 ? TaskExecutionMode.Concurrent : throw new Exception("The task execution strategy must be set");

    /// <summary>
    /// Gets/sets a name/value mapping of the task to execute sequentially. Required if <see cref="Concurrently"/> has not been set, otherwise null.
    /// </summary>
    [DataMember(Name = "sequentially", Order = 1), JsonPropertyName("sequentially"), JsonPropertyOrder(1), YamlMember(Alias = "sequentially", Order = 1)]
    public virtual EquatableDictionary<string, TaskDefinition>? Sequentially { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the task to execute concurrently. Required if <see cref="Sequentially"/> has not been set, otherwise null.
    /// </summary>
    [DataMember(Name = "concurrently", Order = 2), JsonPropertyName("concurrently"), JsonPropertyOrder(2), YamlMember(Alias = "concurrently", Order = 2)]
    public virtual EquatableDictionary<string, TaskDefinition>? Concurrently { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not concurrently executed tasks should race each other, with only one winner setting the task's output.
    /// </summary>
    [DataMember(Name = "compete", Order = 3), JsonPropertyName("compete"), JsonPropertyOrder(3), YamlMember(Alias = "compete", Order = 3)]
    public virtual bool? Compete { get; set; }

}
