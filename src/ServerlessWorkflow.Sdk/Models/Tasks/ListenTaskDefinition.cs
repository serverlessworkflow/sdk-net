﻿// Copyright © 2024-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the configuration of a task used to listen to specific events
/// </summary>
[DataContract]
public record ListenTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public override string Type => TaskType.Listen;

    /// <summary>
    /// Gets/sets the configuration of the listener to use
    /// </summary>
    [Required]
    [DataMember(Name = "listen", Order = 1), JsonPropertyName("listen"), JsonPropertyOrder(1), YamlMember(Alias = "listen", Order = 1)]
    public required virtual ListenerDefinition Listen { get; set; }

    /// <summary>
    /// Gets/sets the configuration of the iterator, if any, used to process each consumed event
    /// </summary>
    [DataMember(Name = "foreach", Order = 2), JsonPropertyName("foreach"), JsonPropertyOrder(2), YamlMember(Alias = "foreach", Order = 2)]
    public virtual SubscriptionIteratorDefinition? Foreach { get; set; }

}
