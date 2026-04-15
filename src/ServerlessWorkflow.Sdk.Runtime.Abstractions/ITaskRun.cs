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

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the state of a single run of a task
/// </summary>
public interface ITaskRun
{

    /// <summary>
    /// Gets the start time of the run
    /// </summary>
    DateTimeOffset StartedAt { get; }

    /// <summary>
    /// Gets the end time of the run, if the task has completed
    /// </summary>
    DateTimeOffset? EndedAt { get; }

    /// <summary>
    /// Gets the run's outcome or, in other words, the status of the task when the run ended
    /// </summary>
    string? Outcome { get; }

}
