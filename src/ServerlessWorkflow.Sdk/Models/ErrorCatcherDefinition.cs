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
/// Represents the configuration of a concept used to catch errors
/// </summary>
[Description("Represents the configuration of a concept used to catch errors")]
[DataContract]
public sealed record ErrorCatcherDefinition
{

    /// <summary>
    /// Gets/sets the definition of the errors to catch
    /// </summary>
    [Description("The definition of the errors to catch")]
    [DataMember(Order = 1, Name = "errors"), JsonPropertyOrder(1), JsonPropertyName("errors")]
    public ErrorFilterDefinition? Errors { get; init; }

    /// <summary>
    /// Gets/sets the name of the runtime expression variable to save the error as. Defaults to 'error'.
    /// </summary>
    [Description("The name of the runtime expression variable to save the error as. Defaults to 'error'.")]
    [DataMember(Order = 2, Name = "as"), JsonPropertyOrder(2), JsonPropertyName("as")]
    public string? As { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to catch the filtered error
    /// </summary>
    [Description("A runtime expression used to determine whether or not to catch the filtered error")]
    [DataMember(Order = 3, Name = "when"), JsonPropertyOrder(3), JsonPropertyName("when")]
    public string? When { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to catch the filtered error
    /// </summary>
    [Description("A runtime expression used to determine whether or not to catch the filtered error")]
    [DataMember(Order = 4, Name = "exceptWhen"), JsonPropertyOrder(4), JsonPropertyName("exceptWhen")]
    public string? ExceptWhen { get; init; }

    /// <summary>
    /// Gets/sets the retry policy to use, if any
    /// </summary>
    [Description("The retry policy to use, if any")]
    [Required]
    [DataMember(Order = 5, Name = "retry"), JsonPropertyOrder(5), JsonPropertyName("retry"), JsonConverter(typeof(OneOfJsonConverter<RetryPolicyDefinition, string>))]
    public OneOf<RetryPolicyDefinition, string>? Retry { get; init; } = null!;

    /// <summary>
    /// Gets/sets a name/definition map of the tasks to run when catching an error
    /// </summary>
    [Description("A name/definition map of the tasks to run when catching an error")]
    [DataMember(Order = 6, Name = "do"), JsonPropertyOrder(6), JsonPropertyName("do")]
    public Map<string, TaskDefinition>? Do { get; init; }

}
