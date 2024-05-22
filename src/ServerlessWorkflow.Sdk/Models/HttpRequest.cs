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
[DataContract]
public record HttpRequest
{

    /// <summary>
    /// Gets/sets the HTTP method of the described request
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "method", Order = 1), JsonPropertyName("method"), JsonPropertyOrder(1), YamlMember(Alias = "method", Order = 1)]
    public required virtual string Method { get; set; }

    /// <summary>
    /// Gets/sets the request URI
    /// </summary>
    [Required]
    [DataMember(Name = "uri", Order = 2), JsonPropertyName("uri"), JsonPropertyOrder(2), YamlMember(Alias = "uri", Order = 2)]
    public required virtual Uri Uri { get; set; }

    /// <summary>
    /// Gets/sets the request headers, if any
    /// </summary>
    [DataMember(Name = "headers", Order = 3), JsonPropertyName("headers"), JsonPropertyOrder(3), YamlMember(Alias = "headers", Order = 3)]
    public virtual EquatableDictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Gets/sets the request body, if any
    /// </summary>
    [DataMember(Name = "body", Order = 4), JsonPropertyName("body"), JsonPropertyOrder(4), YamlMember(Alias = "body", Order = 4)]
    public virtual object? Body { get; set; }

}
