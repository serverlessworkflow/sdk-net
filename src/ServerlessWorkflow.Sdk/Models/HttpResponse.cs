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
/// Represents an object used to describe an HTTP response
/// </summary>
[DataContract]
public record HttpResponse
{

    /// <summary>
    /// Gets/sets the HTTP request associated with the HTTP response
    /// </summary>
    [Required]
    [DataMember(Name = "request", Order = 1), JsonPropertyName("request"), JsonPropertyOrder(1), YamlMember(Alias = "request", Order = 1)]
    public required virtual HttpRequest Request { get; set; }

    /// <summary>
    /// Gets/sets the HTTP response's status code
    /// </summary>
    [Required]
    [DataMember(Name = "statusCode", Order = 2), JsonPropertyName("statusCode"), JsonPropertyOrder(2), YamlMember(Alias = "statusCode", Order = 2)]
    public required virtual int StatusCode { get; set; }

    /// <summary>
    /// Gets/sets the response headers, if any
    /// </summary>
    [DataMember(Name = "headers", Order = 3), JsonPropertyName("headers"), JsonPropertyOrder(3), YamlMember(Alias = "headers", Order = 3)]
    public virtual EquatableDictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Gets/sets the HTTP response's content, if any
    /// </summary>
    [DataMember(Name = "content", Order = 4), JsonPropertyName("content"), JsonPropertyOrder(4), YamlMember(Alias = "content", Order = 4)]
    public virtual object? Content { get; set; }

}
