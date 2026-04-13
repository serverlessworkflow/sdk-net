// Copyright © 2024-Present The Synapse Authors
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

namespace ServerlessWorkflow.Sdk.Events.Tasks;

/// <summary>
/// Represents the data carried by the cloud event that notifies that a task has been created
/// </summary>
[DataContract]
public sealed record TaskCreatedEvent
{

    /// <summary>
    /// Gets/sets the qualified name of the workflow instance the task that has been created belongs to
    /// </summary>
    [DataMember(Name = "workflow", Order = 1), JsonPropertyName("workflow"), JsonPropertyOrder(1)]
    public required string Workflow { get; set; }

    /// <summary>
    /// Gets/sets the reference of the task that has been created
    /// </summary>
    [DataMember(Name = "task", Order = 2), JsonPropertyName("task"), JsonPropertyOrder(2)]
    public required JsonPointer Task { get; set; }

    /// <summary>
    /// Gets/sets the date and time at which the task has been created
    /// </summary>
    [DataMember(Name = "createdAt", Order = 3), JsonPropertyName("createdAt"), JsonPropertyOrder(3)]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

}