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
[DataContract]
public record HttpCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets/sets the HTTP method of the request to perform
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "method", Order = 1), JsonPropertyName("method"), JsonPropertyOrder(1), YamlMember(Alias = "method", Order = 1)]
    public required virtual string Method { get; set; }

    /// <summary>
    /// Gets/sets the endpoint at which to get the defined resource
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual EndpointDefinition Endpoint
    {
        get => this.EndpointValue.T1Value ?? new() { Uri = this.EndpointUri };
        set => this.EndpointValue = value;
    }

    /// <summary>
    /// Gets/sets the endpoint at which to get the defined resource
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri EndpointUri
    {
        get => this.EndpointValue.T1Value?.Uri ?? this.EndpointValue.T2Value!;
        set => this.EndpointValue = value;
    }

    /// <summary>
    /// Gets/sets the endpoint at which to get the defined resource
    /// </summary>
    [Required]
    [DataMember(Name = "endpoint", Order = 2), JsonInclude, JsonPropertyName("endpoint"), JsonPropertyOrder(2), YamlMember(Alias = "endpoint", Order = 2)]
    protected virtual OneOf<EndpointDefinition, Uri> EndpointValue { get; set; } = null!;

    /// <summary>
    /// Gets/sets a name/value mapping of the headers, if any, of the HTTP request to perform
    /// </summary>
    [DataMember(Name = "headers", Order = 3), JsonPropertyName("headers"), JsonPropertyOrder(3), YamlMember(Alias = "headers", Order = 3)]
    public virtual EquatableDictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Gets/sets the body, if any, of the HTTP request to perform
    /// </summary>
    [DataMember(Name = "body", Order = 4), JsonPropertyName("body"), JsonPropertyOrder(4), YamlMember(Alias = "body", Order = 4)]
    public virtual object? Body { get; set; }

    /// <summary>
    /// Gets/sets the http call output format. Defaults to <see cref="HttpOutputFormat.Content"/>.
    /// </summary>
    [DataMember(Name = "output", Order = 5), JsonPropertyName("output"), JsonPropertyOrder(5), YamlMember(Alias = "output", Order = 5)]
    public virtual string? Output { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether redirection status codes (300–399) should be treated as errors.<para></para>
    /// If set to 'false', runtimes must raise an error for response status codes outside the 200–299 range.<para></para>
    /// If set to 'true', they must raise an error for status codes outside the 200–399 range.
    /// </summary>
    [DataMember(Name = "redirect", Order = 6), JsonPropertyName("redirect"), JsonPropertyOrder(6), YamlMember(Alias = "redirect", Order = 6)]
    public virtual bool Redirect { get; set; }

}