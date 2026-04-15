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

namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents a single run of a task, including start and end times
/// </summary>
[DataContract]
public sealed class TaskRun
    : ITaskRun
{

    /// <summary>
    /// Gets/sets the start time of the run
    /// </summary>
    [DataMember(Name = "startedAt", Order = 1), JsonPropertyName("startedAt"), JsonPropertyOrder(1)]
    public required DateTimeOffset StartedAt { get; set; }

    /// <summary>
    /// Gets/sets the end time of the run, if the task has completed
    /// </summary>
    [DataMember(Name = "endedAt", Order = 2), JsonPropertyName("endedAt"), JsonPropertyOrder(2)]
    public DateTimeOffset? EndedAt { get; set; }

    /// <summary>
    /// Gets/sets the run's outcome or, in other words, the status of the task when the run ended
    /// </summary>
    [DataMember(Name = "outcome", Order = 3), JsonPropertyName("outcome"), JsonPropertyOrder(2)]
    public string? Outcome { get; set; }

}