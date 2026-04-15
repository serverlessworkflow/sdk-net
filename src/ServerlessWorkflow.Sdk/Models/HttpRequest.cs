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
/// Represents an object used to describe an HTTP request
/// </summary>
[Description("Represents an object used to describe an HTTP request.")]
[DataContract]
public sealed record HttpRequest
{

    /// <summary>
    /// Gets/sets the HTTP method of the described request
    /// </summary>
    [Description("The HTTP method of the described request.")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "method"), JsonPropertyOrder(1), JsonPropertyName("method")]
    public required string Method { get; init; }

    /// <summary>
    /// Gets/sets the request URI
    /// </summary>
    [Description("The request URI.")]
    [Required]
    [DataMember(Order = 2, Name = "uri"), JsonPropertyOrder(2), JsonPropertyName("uri")]
    public required Uri Uri { get; init; }

    /// <summary>
    /// Gets/sets the request headers, if any
    /// </summary>
    [Description("The request headers, if any.")]
    [DataMember(Order = 3, Name = "headers"), JsonPropertyOrder(3), JsonPropertyName("headers")]
    public EquatableDictionary<string, string>? Headers { get; init; }

    /// <summary>
    /// Gets/sets the request body, if any
    /// </summary>
    [Description("The request body, if any.")]
    [DataMember(Order = 4, Name = "body"), JsonPropertyOrder(4), JsonPropertyName("body")]
    public JsonNode? Body { get; init; }

}