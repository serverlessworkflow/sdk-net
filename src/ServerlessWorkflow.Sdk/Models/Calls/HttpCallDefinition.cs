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
/// Represents the definition of an HTTP call
/// </summary>
[Description("Represents the definition of an HTTP call")]
[DataContract]
public sealed record HttpCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets/sets the HTTP method of the request to perform
    /// </summary>
    [Description("The HTTP method of the request to perform")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "method"), JsonPropertyOrder(1), JsonPropertyName("method")]
    public required string Method { get; init; }

    /// <summary>
    /// Gets/sets the endpoint at which to get the defined resource
    /// </summary>
    [Description("The endpoint at which to get the defined resource")]
    [Required]
    [DataMember(Order = 2, Name = "endpoint"), JsonPropertyOrder(2), JsonPropertyName("endpoint"), JsonConverter(typeof(OneOfJsonConverter<EndpointDefinition, Uri>))]
    public required OneOf<EndpointDefinition, Uri> Endpoint { get; init; }

    /// <summary>
    /// Gets/sets a name/value mapping of the headers, if any, of the HTTP request to perform
    /// </summary>
    [Description("A name/value mapping of the headers, if any, of the HTTP request to perform")]
    [DataMember(Order = 3, Name = "headers"), JsonPropertyOrder(3), JsonPropertyName("headers")]
    public EquatableDictionary<string, string>? Headers { get; init; }

    /// <summary>
    /// Gets/sets the body, if any, of the HTTP request to perform
    /// </summary>
    [Description("The body, if any, of the HTTP request to perform")]
    [DataMember(Order = 4, Name = "body"), JsonPropertyOrder(4), JsonPropertyName("body")]
    public JsonNode? Body { get; init; }

    /// <summary>
    /// Gets/sets the http call output format. Defaults to <see cref="HttpOutputFormat.Content"/>.
    /// </summary>
    [Description("The http call output format. Defaults to HttpOutputFormat.Content.")]
    [DataMember(Order = 5, Name = "output"), JsonPropertyOrder(5), JsonPropertyName("output")]
    public string? Output { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether redirection status codes (300–399) should be treated as errors.<para></para>
    /// If set to 'false', runtimes must raise an error for response status codes outside the 200–299 range.<para></para>
    /// If set to 'true', they must raise an error for status codes outside the 200–399 range.
    /// </summary>
    [Description("A boolean indicating whether redirection status codes (300–399) should be treated as errors. If set to 'false', runtimes must raise an error for response status codes outside the 200–299 range. If set to 'true', they must raise an error for status codes outside the 200–399 range.")]
    [DataMember(Order = 6, Name = "redirect"), JsonPropertyOrder(6), JsonPropertyName("redirect")]
    public bool Redirect { get; init; }

}