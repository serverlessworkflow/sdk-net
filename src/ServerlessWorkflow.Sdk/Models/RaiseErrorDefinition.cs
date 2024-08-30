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
/// Represents the definition of the error to raise
/// </summary>
[DataContract]
public record RaiseErrorDefinition
{

    /// <summary>
    /// Gets/sets the definition of the error to raise
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual ErrorDefinition? Error
    {
        get => this.ErrorValue.T1Value;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            this.ErrorValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the reference of the error to raise
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? ErrorReference
    {
        get => this.ErrorValue.T2Value;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            this.ErrorValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the endpoint at which to get the defined resource
    /// </summary>
    [Required]
    [DataMember(Name = "error", Order = 1), JsonInclude, JsonPropertyName("error"), JsonPropertyOrder(1), YamlMember(Alias = "error", Order = 1)]
    protected virtual OneOf<ErrorDefinition, string> ErrorValue { get; set; } = null!;

}
