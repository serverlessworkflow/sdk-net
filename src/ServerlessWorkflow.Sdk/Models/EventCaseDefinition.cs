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
/// Represents the definition of an event-based <see cref="SwitchStateDefinition"/>
/// </summary>
[DataContract]
public class EventCaseDefinition
    : SwitchCaseDefinition
{

    /// <summary>
    /// Gets/sets the unique event name the condition applies to
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "eventRef", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("eventRef"), YamlMember(Alias = "eventRef", Order = 1)]
    public string EventRef { get; set; } = null!;

}