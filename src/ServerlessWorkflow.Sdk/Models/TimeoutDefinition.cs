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

using System.Xml;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a timeout
/// </summary>
[DataContract]
public record TimeoutDefinition
{

    /// <summary>
    /// Gets/sets the duration after which to timeout
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Duration After
    {
        get => this.AfterValue.T1Value ?? Duration.FromTimeSpan(XmlConvert.ToTimeSpan(this.AfterValue.T2Value!));
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            this.AfterValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the ISO 8601 expression of the duration after which to timeout
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string AfterExpression
    {
        get => this.AfterValue.T2Value ?? XmlConvert.ToString(this.AfterValue.T1Value!.ToTimeSpan());
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            this.AfterValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the duration after which to timeout
    /// </summary>
    [Required]
    [DataMember(Name = "after", Order = 1), JsonInclude, JsonPropertyName("after"), JsonPropertyOrder(1), YamlMember(Alias = "after", Order = 1)]
    protected virtual OneOf<Duration, string> AfterValue { get; set; } = null!;

}

