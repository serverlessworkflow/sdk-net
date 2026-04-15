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
[Description("Represents the definition of an OpenAPI call")]
[DataContract]
public sealed record OpenApiCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets/sets the document that defines the OpenAPI operation to call
    /// </summary>
    [Description("The document that defines the OpenAPI operation to call")]
    [Required]
    [DataMember(Order = 1, Name = "document"), JsonPropertyOrder(1), JsonPropertyName("document")]
    public required ExternalResourceDefinition Document { get; set; }

    /// <summary>
    /// Gets/sets the id of the OpenAPI operation to call
    /// </summary>
    [Description("The id of the OpenAPI operation to call")]
    [Required]
    [DataMember(Order = 2, Name = "operationId"), JsonPropertyOrder(2), JsonPropertyName("operationId")]
    public required string OperationId { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the parameters of the OpenAPI operation to call
    /// </summary>
    [Description("A name/value mapping of the parameters of the OpenAPI operation to call")]
    [DataMember(Order = 3, Name = "parameters"), JsonPropertyOrder(3), JsonPropertyName("parameters")]
    public JsonObject? Parameters { get; set; }

    /// <summary>
    /// Gets/sets the authentication policy, if any, to use when calling the OpenAPI operation
    /// </summary>
    [Description("The authentication policy, if any, to use when calling the OpenAPI operation")]
    [DataMember(Order = 4, Name = "authentication"), JsonPropertyOrder(4), JsonPropertyName("authentication")]
    public AuthenticationPolicyDefinition? Authentication { get; set; }

    /// <summary>
    /// Gets/sets the http output format. Defaults to <see cref="HttpOutputFormat.Content"/>.
    /// </summary>
    [Description("The http output format. Defaults to HttpOutputFormat.Content.")]
    [DataMember(Order = 5, Name = "output"), JsonPropertyOrder(5), JsonPropertyName("output")]
    public string? Output { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether redirection status codes (300–399) should be treated as errors.<para></para>
    /// If set to 'false', runtimes must raise an error for response status codes outside the 200–299 range.<para></para>
    /// If set to 'true', they must raise an error for status codes outside the 200–399 range.
    /// </summary>
    [Description("A boolean indicating whether redirection status codes (300–399) should be treated as errors. If set to 'false', runtimes must raise an error for response status codes outside the 200–299 range. If set to 'true', they must raise an error for status codes outside the 200–399 range.")]
    [DataMember(Order = 6, Name = "redirect"), JsonPropertyOrder(6), JsonPropertyName("redirect")]
    public bool Redirect { get; set; }

}