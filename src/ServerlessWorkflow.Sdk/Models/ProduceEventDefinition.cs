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
/// Represents the object used to configure an event o produce
/// </summary>
[DataContract]
public class ProduceEventDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the name of a defined event to produce
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "eventReference", IsRequired = true), JsonPropertyName("eventReference"), YamlMember(Alias = "eventReference")]
    public string EventReference { get; set; } = null!;

    /// <summary>
    /// Gets/sets the data to pass to the cloud event to produce. If String, expression which selects parts of the states data output to become the data of the produced event. If object a custom object to become the data of produced event.
    /// </summary>
    [DataMember(Order = 2, Name = "data"), JsonPropertyName("data"), YamlMember(Alias = "data")]
    public IDictionary<string, object>? Data { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="FunctionDefinition"/>'s extension properties
    /// </summary>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

}
