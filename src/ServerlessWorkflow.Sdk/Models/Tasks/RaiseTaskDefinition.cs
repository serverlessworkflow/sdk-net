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
/// Represents the definition of a task used to raise an error
/// </summary>
[DataContract]
public record RaiseTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public override string Type => TaskType.Raise;

    /// <summary>
    /// Gets/sets the definition of the error to raise
    /// </summary>
    [Required]
    [DataMember(Name = "raise", Order = 1), JsonPropertyName("raise"), JsonPropertyOrder(1), YamlMember(Alias = "raise", Order = 1)]
    public required virtual RaiseErrorDefinition Raise { get; set; }

}