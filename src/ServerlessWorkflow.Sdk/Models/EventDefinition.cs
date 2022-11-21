/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to define events and their correlations
/// </summary>
[ProtoContract]
[DataContract]
public class EventDefinition
{

    /// <summary>
    /// Gets/sets the Unique event name
    /// </summary>
    [Required]
    [Newtonsoft.Json.JsonRequired]
    [ProtoMember(1)]
    [DataMember(Order = 1)]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets the cloud event source
    /// </summary>
    [Required]
    [Newtonsoft.Json.JsonRequired]
    [ProtoMember(2)]
    [DataMember(Order = 2)]
    public virtual string? Source { get; set; }

    /// <summary>
    /// Gets/sets the cloud event type
    /// </summary>
    [ProtoMember(3)]
    [DataMember(Order = 3)]
    public virtual string Type { get; set; } = null!;

    /// <summary>
    /// Gets/sets a value that defines the CloudEvent as either '<see cref="EventKind.Consumed"/>' or '<see cref="EventKind.Produced"/>' by the workflow. Default is '<see cref="EventKind.Consumed"/>'.
    /// </summary>
    [ProtoMember(4)]
    [DataMember(Order = 4)]
    [DefaultValue(EventKind.Consumed)]
    public virtual string Kind { get; set; } = EventKind.Consumed;

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> containing the <see cref="EventCorrelationDefinition"/>s used to define the way the cloud event is correlated
    /// </summary>
    [Newtonsoft.Json.JsonProperty(PropertyName = "correlation"), MinLength(1)]
    [System.Text.Json.Serialization.JsonPropertyName("correlation")]
    [YamlMember(Alias = "correlation")]
    [ProtoMember(5, Name = "correlation")]
    [DataMember(Order = 5, Name = "correlation")]
    public virtual List<EventCorrelationDefinition>? Correlations { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to use the event's data only (thus making data the top level element, instead of including all context attributes at top level). Defaults to true.
    /// </summary>
    [ProtoMember(6)]
    [DataMember(Order = 6)]
    [DefaultValue(true)]
    public virtual bool DataOnly { get; set; } = true;

    /// <summary>
    /// Gets/sets the <see cref="EventDefinition"/>'s metadata
    /// </summary>
    [ProtoMember(7)]
    [DataMember(Order = 7)]
    public virtual DynamicObject? Metadata { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="EventDefinition"/>'s extension properties
    /// </summary>
    [ProtoMember(8)]
    [DataMember(Order = 8)]
    [Newtonsoft.Json.JsonExtensionData]
    [System.Text.Json.Serialization.JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionProperties { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.Name;
    }

}
