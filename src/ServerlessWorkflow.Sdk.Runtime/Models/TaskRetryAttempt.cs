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
/// Represents an object used to describe a retry attempt
/// </summary>
[DataContract]
public sealed class TaskRetryAttempt
    : ITaskRetryAttempt
{

    /// <summary>
    /// Gets/sets the retry attempt number
    /// </summary>
    [Required]
    [DataMember(Name = "number", Order = 1), JsonPropertyName("number"), JsonPropertyOrder(1)]
    public required uint Number { get; set; }

    /// <summary>
    /// Gets/sets the date and time at which the retry attempt was performed
    /// </summary>
    [DataMember(Name = "time", Order = 2), JsonPropertyName("time"), JsonPropertyOrder(2)]
    public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

    /// <summary>
    /// Gets/sets the <see cref="Error"/> that is the cause of the try attempt
    /// </summary>
    [Required]
    [DataMember(Name = "cause", Order = 3), JsonPropertyName("cause"), JsonPropertyOrder(3)]
    public required Error Cause { get; set; }

}