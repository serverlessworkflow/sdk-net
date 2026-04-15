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
[Description("Represents the definition of a subscription iterator, used to configure the processing of each event or message consumed by a subscription")]
[DataContract]
public sealed record SubscriptionIteratorDefinition
{

    /// <summary>
    /// Gets/sets the name of the variable used to store the item being enumerated.<para></para>
    /// Defaults to `item`
    /// </summary>
    [Description("The name of the variable used to store the item being enumerated. Defaults to `item`")]
    [DataMember(Order = 1, Name = "item"), JsonPropertyOrder(1), JsonPropertyName("item")]
    public string? Item { get; init; }

    /// <summary>
    /// Gets/sets the name of the variable used to store the index of the item being enumerates<para></para>
    /// Defaults to `index`
    /// </summary>
    [Description("The name of the variable used to store the index of the item being enumerates. Defaults to `index`")]
    [DataMember(Order = 2, Name = "index"), JsonPropertyOrder(2), JsonPropertyName("index")]
    public string? At { get; init; }

    /// <summary>
    /// Gets/sets the tasks to run for each consumed event or message
    /// </summary>
    [Description("The tasks to run for each consumed event or message")]
    [DataMember(Order = 3, Name = "do"), JsonPropertyOrder(3), JsonPropertyName("do")]
    public Map<string, TaskDefinition>? Do { get; init; }

    /// <summary>
    /// Gets/sets the definition, if any, of the data to output for each iteration
    /// </summary>
    [Description("The definition, if any, of the data to output for each iteration")]
    [DataMember(Order = 4, Name = "output"), JsonPropertyOrder(4), JsonPropertyName("output")]
    public OutputDataModelDefinition? Output { get; init; }

    /// <summary>
    /// Gets/sets the definition, if any, of the data to export for each iteration
    /// </summary>
    [Description("The definition, if any, of the data to export for each iteration")]
    [DataMember(Order = 5, Name = "export"), JsonPropertyOrder(5), JsonPropertyName("export")]
    public OutputDataModelDefinition? Export { get; init; }

}