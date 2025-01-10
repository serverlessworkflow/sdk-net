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

namespace ServerlessWorkflow.Sdk.Models.Calls;

/// <summary>
/// Represents the definition of an OpenAPI call
/// </summary>
[DataContract]
public record OpenApiCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets/sets the document that defines the OpenAPI operation to call
    /// </summary>
    [Required]
    [DataMember(Name = "document", Order = 1), JsonPropertyName("document"), JsonPropertyOrder(1), YamlMember(Alias = "document", Order = 1)]
    public required virtual ExternalResourceDefinition Document { get; set; }

    /// <summary>
    /// Gets/sets the id of the OpenAPI operation to call
    /// </summary>
    [Required]
    [DataMember(Name = "operationId", Order = 2), JsonPropertyName("operationId"), JsonPropertyOrder(2), JsonInclude, YamlMember(Alias = "operationId", Order = 2)]
    public required virtual string OperationId { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the parameters of the OpenAPI operation to call
    /// </summary>
    [DataMember(Name = "parameters", Order = 3), JsonPropertyName("parameters"), JsonPropertyOrder(3), YamlMember(Alias = "parameters", Order = 3)]
    public virtual EquatableDictionary<string, object>? Parameters { get; set; }

    /// <summary>
    /// Gets/sets the authentication policy, if any, to use when calling the OpenAPI operation
    /// </summary>
    [DataMember(Name = "authentication", Order = 4), JsonPropertyName("authentication"), JsonPropertyOrder(4), YamlMember(Alias = "authentication", Order = 4)]
    public virtual AuthenticationPolicyDefinition? Authentication { get; set; }

    /// <summary>
    /// Gets/sets the http output format. Defaults to <see cref="HttpOutputFormat.Content"/>.
    /// </summary>
    [DataMember(Name = "output", Order = 6), JsonPropertyName("output"), JsonPropertyOrder(6), YamlMember(Alias = "output", Order = 6)]
    public virtual string? Output { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether redirection status codes (300–399) should be treated as errors.<para></para>
    /// If set to 'false', runtimes must raise an error for response status codes outside the 200–299 range.<para></para>
    /// If set to 'true', they must raise an error for status codes outside the 200–399 range.
    /// </summary>
    [DataMember(Name = "redirect", Order = 7), JsonPropertyName("redirect"), JsonPropertyOrder(7), YamlMember(Alias = "redirect", Order = 7)]
    public virtual bool Redirect { get; set; }

}
