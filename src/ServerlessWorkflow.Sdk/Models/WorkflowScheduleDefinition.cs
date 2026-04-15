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
[Description("Represents the definition of a workflow's schedule.")]
[DataContract]
public sealed record WorkflowScheduleDefinition
{

    /// <summary>
    /// Gets/sets an object used to document the defined workflow
    /// </summary>
    [Description("An object used to document the defined workflow.")]
    [DataMember(Order = 1, Name = "document"), JsonPropertyOrder(1), JsonPropertyName("document")]
    public Duration? Every { get; init; }

    /// <summary>
    /// Gets/sets the schedule using a CRON expression, e.g., '0 0 * * *' for daily at midnight.
    /// </summary>
    [Description("The schedule using a CRON expression, e.g., '0 0 * * *' for daily at midnight.")]
    [DataMember(Order = 2, Name = "cron"), JsonPropertyOrder(2), JsonPropertyName("cron")]
    public string? Cron { get; init; }

    /// <summary>
    /// Gets/sets a delay duration, if any, that the workflow must wait before starting again after it completes. In other words, when this workflow completes, it should run again after the specified amount of time.
    /// </summary>
    [Description("A delay duration, if any, that the workflow must wait before starting again after it completes. In other words, when this workflow completes, it should run again after the specified amount of time.")]
    [DataMember(Order = 3, Name = "after"), JsonPropertyOrder(3), JsonPropertyName("after")]
    public Duration? After { get; init; }

    /// <summary>
    /// Gets/sets the events that trigger the workflow execution.
    /// </summary>
    [Description("The events that trigger the workflow execution.")]
    [DataMember(Order = 4, Name = "on"), JsonPropertyOrder(4), JsonPropertyName("on")]
    public EventConsumptionStrategyDefinition? On { get; init; }

}