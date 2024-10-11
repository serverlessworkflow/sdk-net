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
[DataContract]
public record ErrorCatcherDefinition
{

    /// <summary>
    /// Gets/sets the definition of the errors to catch
    /// </summary>
    [DataMember(Name = "errors", Order = 1), JsonPropertyName("errors"), JsonPropertyOrder(1), YamlMember(Alias = "errors", Order = 1)]
    public virtual ErrorFilterDefinition? Errors { get; set; }

    /// <summary>
    /// Gets/sets the name of the runtime expression variable to save the error as. Defaults to 'error'.
    /// </summary>
    [DataMember(Name = "as", Order = 2), JsonPropertyName("as"), JsonPropertyOrder(2), YamlMember(Alias = "as", Order = 2)]
    public virtual string? As { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to catch the filtered error
    /// </summary>
    [DataMember(Name = "when", Order = 3), JsonPropertyName("when"), JsonPropertyOrder(3), YamlMember(Alias = "when", Order = 3)]
    public virtual string? When { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to catch the filtered error
    /// </summary>
    [DataMember(Name = "exceptWhen", Order = 4), JsonPropertyName("exceptWhen"), JsonPropertyOrder(4), YamlMember(Alias = "exceptWhen", Order = 4)]
    public virtual string? ExceptWhen { get; set; }

    /// <summary>
    /// Gets/sets the retry policy to use, if any
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual RetryPolicyDefinition? Retry
    {
        get => this.RetryValue?.T1Value;
        set => this.RetryValue = value!;
    }

    /// <summary>
    /// Gets/sets the reference of the retry policy to use, if any
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? RetryReference
    {
        get => this.RetryValue?.T2Value;
        set => this.RetryValue = value!;
    }

    /// <summary>
    /// Gets/sets the retry policy to use, if any
    /// </summary>
    [Required]
    [DataMember(Name = "retry", Order = 5), JsonInclude, JsonPropertyName("retry"), JsonPropertyOrder(5), YamlMember(Alias = "retry", Order = 5)]
    protected virtual OneOf<RetryPolicyDefinition, string>? RetryValue { get; set; } = null!;

    /// <summary>
    /// Gets/sets a name/definition map of the tasks to run when catching an error
    /// </summary>
    [DataMember(Name = "do", Order = 6), JsonPropertyName("do"), JsonPropertyOrder(6), YamlMember(Alias = "do", Order = 6)]
    public virtual Map<string, TaskDefinition>? Do { get; set; }

}
