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
/// Represents the definition of a schema
/// </summary>
[Description("Represents the definition of a schema")]
[DataContract]
public sealed record SchemaDefinition
{

    /// <summary>
    /// Gets/sets the schema's format. Defaults to 'json'. The (optional) version of the format can be set using `{format}:{version}`.
    /// </summary>
    [Required, StringLength(int.MaxValue, MinimumLength = 1), DefaultValue(SchemaFormat.Json)]
    [Description("The schema's format. Defaults to 'json'. The (optional) version of the format can be set using `{format}:{version}`.")]
    [DataMember(Order = 1, Name = "format"), JsonPropertyOrder(1), JsonPropertyName("format")]
    public string Format { get; init; } = SchemaFormat.Json;

    /// <summary>
    /// Gets/sets the schema's external resource, if any. Required if <see cref="Document"/> has not been set.
    /// </summary>
    [Description("The schema's external resource, if any. Required if Document has not been set.")]
    [DataMember(Order = 2, Name = "resource"), JsonPropertyOrder(2), JsonPropertyName("resource")]
    public ExternalResourceDefinition? Resource { get; init; }

    /// <summary>
    /// Gets/sets the inline definition of the schema to use. Required if <see cref="Resource"/> has not been set.
    /// </summary>
    [Description("The inline definition of the schema to use. Required if Resource has not been set.")]
    [DataMember(Order = 3, Name = "document"), JsonPropertyOrder(3), JsonPropertyName("document"), JsonConverter(typeof(OneOfJsonConverter<JsonObject, string>))]
    public OneOf<JsonObject, string>? Document { get; init; }

}