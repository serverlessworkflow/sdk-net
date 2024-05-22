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
/// Represents the definition of a workflow's schedule
/// </summary>
[DataContract]
public record WorkflowScheduleDefinition
{

    /// <summary>
    /// Gets/sets an object used to document the defined workflow
    /// </summary>
    [DataMember(Name = "every", Order = 1), JsonPropertyName("every"), JsonPropertyOrder(1), YamlMember(Alias = "every", Order = 1)]
    public virtual Duration? Every { get; set; }

    /// <summary>
    /// Gets/sets the schedule using a CRON expression, e.g., '0 0 * * *' for daily at midnight.
    /// </summary>
    [DataMember(Name = "cron", Order = 2), JsonPropertyName("cron"), JsonPropertyOrder(2), YamlMember(Alias = "cron", Order = 2)]
    public virtual string? Cron { get; set; }

    /// <summary>
    /// Gets/sets a delay duration, if any, that the workflow must wait before starting again after it completes. In other words, when this workflow completes, it should run again after the specified amount of time.
    /// </summary>
    [DataMember(Name = "after", Order = 3), JsonPropertyName("after"), JsonPropertyOrder(3), YamlMember(Alias = "after", Order = 3)]
    public virtual Duration? After { get; set; }

    /// <summary>
    /// Gets/sets the events that trigger the workflow execution.
    /// </summary>
    [DataMember(Name = "on", Order = 4), JsonPropertyName("on"), JsonPropertyOrder(4), YamlMember(Alias = "on", Order = 4)]
    public virtual EventConsumptionStrategyDefinition? On { get; set; }

}