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
/// Represents an object used to configure how event data is to be filtered and added to or merged with the state data
/// </summary>
[DataContract]
public class EventDataFilterDefinition
{

    /// <summary>
    /// Gets/sets an expression that filters the event data (payload)
    /// </summary>
    [DataMember(Order = 1, Name = "data"), JsonPropertyOrder(1), JsonPropertyName("data"), YamlMember(Alias = "data", Order = 1)]
    public virtual string? Data { get; set; }

    /// <summary>
    /// Gets/sets an expression that selects a state data element to which the action results should be added/merged into. If not specified denotes the top-level state data element
    /// </summary>
    [DataMember(Order = 2, Name = "toStateData"), JsonPropertyOrder(2), JsonPropertyName("toStateData"), YamlMember(Alias = "toStateData", Order = 2)]
    public virtual string? ToStateData { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to merge the event's data into state data.<para></para> If set to false, action data results are not added/merged to state data. In this case 'data' and 'toStateData' should be ignored. Defaults to true.
    /// </summary>
    [DefaultValue(true)]
    [DataMember(Order = 3, Name = "useData"), JsonPropertyOrder(3), JsonPropertyName("useData"), YamlMember(Alias = "useData", Order = 3)]
    public virtual bool UseData { get; set; } = true;

}