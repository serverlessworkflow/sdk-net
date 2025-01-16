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
/// Represents the definition of a subscription iterator, used to configure the processing of each event or message consumed by a subscription
/// </summary>
[DataContract]
public record SubscriptionIteratorDefinition
{

    /// <summary>
    /// Gets/sets the name of the variable used to store the item being enumerated.<para></para>
    /// Defaults to `item`
    /// </summary>
    [DataMember(Name = "item", Order = 1), JsonPropertyName("item"), JsonPropertyOrder(1), YamlMember(Alias = "item", Order = 1)]
    public virtual string? Item { get; set; }

    /// <summary>
    /// Gets/sets the name of the variable used to store the index of the item being enumerates<para></para>
    /// Defaults to `index`
    /// </summary>
    [DataMember(Name = "at", Order = 2), JsonPropertyName("at"), JsonPropertyOrder(2), YamlMember(Alias = "at", Order = 2)]
    public virtual string? At { get; set; }

    /// <summary>
    /// Gets/sets the tasks to run for each consumed event or message
    /// </summary>
    [DataMember(Name = "do", Order = 3), JsonPropertyName("do"), JsonPropertyOrder(3), YamlMember(Alias = "do", Order = 3)]
    public virtual Map<string, TaskDefinition>? Do { get; set; }

    /// <summary>
    /// Gets/sets the definition, if any, of the data to output for each iteration
    /// </summary>
    [DataMember(Name = "output", Order = 4), JsonPropertyName("output"), JsonPropertyOrder(4), YamlMember(Alias = "output", Order = 4)]
    public virtual OutputDataModelDefinition? Output { get; set; }

    /// <summary>
    /// Gets/sets the definition, if any, of the data to export for each iteration
    /// </summary>
    [DataMember(Name = "export", Order = 5), JsonPropertyName("export"), JsonPropertyOrder(5), YamlMember(Alias = "export", Order = 5)]
    public virtual OutputDataModelDefinition? Export { get; set; }

}