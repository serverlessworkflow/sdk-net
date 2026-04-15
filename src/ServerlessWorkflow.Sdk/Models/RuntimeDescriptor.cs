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
/// Represents an runtime expression argument used to describe the current runtime
/// </summary>
[Description("Represents an runtime expression argument used to describe the current runtime.")]
[DataContract]
public sealed record RuntimeDescriptor
{

    /// <summary>
    /// Gets/sets the runtime's name
    /// </summary>
    [Description("The runtime's name.")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets/sets the runtime's version
    /// </summary>
    [Description("The runtime's version.")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1), SemanticVersion]
    [DataMember(Order = 2, Name = "version"), JsonPropertyOrder(2), JsonPropertyName("version")]
    public required string Version { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the runtime's metadata, if any
    /// </summary>
    [Description("A key/value mapping of the runtime's metadata, if any.")]
    [DataMember(Order = 3, Name = "metadata"), JsonPropertyOrder(3), JsonPropertyName("metadata")]
    public JsonObject? Metadata { get; init; }

}
