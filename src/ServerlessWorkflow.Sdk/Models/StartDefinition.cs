// Copyright © 2023-Present The Serverless Workflow Specification Authors
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
/// Represents an object used to explicitly define how/when workflow instances should be created
/// </summary>
[DataContract]
public class StartDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the name of the workflow definition's start state definition. If not defined, defaults to the first defined state
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "stateName", IsRequired = true), JsonPropertyName("stateName"), YamlMember(Alias = "stateName")]
    public virtual string StateName { get; set; } = null!;

    /// <summary>
    /// Gets/sets the object used to define the time/repeating intervals at which workflow instances can/should be started
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "schedule", IsRequired = true), JsonPropertyName("schedule"), YamlMember(Alias = "schedule")]
    public virtual ScheduleDefinition? Schedule { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

}
