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
/// Represents the configuration of an event's emission
/// </summary>
[DataContract]
public record EventEmissionDefinition
{

    /// <summary>
    /// Gets/sets the definition of the event to emit
    /// </summary>
    [Required]
    [DataMember(Name = "event", Order = 1), JsonPropertyName("event"), JsonPropertyOrder(1), YamlMember(Alias = "event", Order = 1)]
    public required virtual EventDefinition Event { get; set; }

    /// <summary>
    /// Gets/sets an additional endpoint for emitting a carbon copy of the event. 
    /// While the runtime's default cloud event endpoint remains the primary destination, setting this property ensures that the event is also published to the specified endpoint. 
    /// Ideally, this property is left unset so that event delivery relies solely on the runtime's configured endpoint, but when provided, the event will be sent to both endpoints concurrently.
    /// </summary>
    [DataMember(Name = "cc", Order = 2), JsonPropertyName("cc"), JsonPropertyOrder(2), YamlMember(Alias = "cc", Order = 2)]
    public virtual EndpointDefinition? Cc { get; set; }

}
