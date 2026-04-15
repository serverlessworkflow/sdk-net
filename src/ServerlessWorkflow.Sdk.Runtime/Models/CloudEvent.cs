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

namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents a <see href="https://cloudevents.io/">Cloud Event</see>
/// </summary>
[Description("Represents a Cloud Event")]
[DataContract]
public sealed record CloudEvent
    : ICloudEvent
{

    /// <summary>
    /// Gets the '1.0' version of the <see href="https://cloudevents.io/">Cloud Event spec</see>
    /// </summary>
    public const string DefaultVersion = "1.0";

    /// <summary>
    /// Gets/sets a string that uniquely identifies the cloud event in the scope of its source
    /// </summary>
    [Description("A string that uniquely identifies the cloud event in the scope of its source")]
    [Required]
    [DataMember(Order = 1, Name = "id"), JsonPropertyOrder(1), JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    /// <summary>
    /// Gets/sets the version of the CloudEvents specification which the event uses. Defaults to <see cref="DefaultVersion"/>
    /// </summary>
    [Description("The version of the CloudEvents specification which the event uses. Defaults to 1.0")]
    [DefaultValue(DefaultVersion)]
    [DataMember(Order = 2, Name = "specversion"), JsonPropertyOrder(2), JsonPropertyName("specversion")]
    public string SpecVersion { get; set; } = DefaultVersion;

    /// <summary>
    /// Gets/sets the date and time at which the event has been produced
    /// </summary>
    [Description("The date and time at which the event has been produced")]
    [DataMember(Order = 3, Name = "time"), JsonPropertyOrder(3), JsonPropertyName("time")]
    public DateTimeOffset? Time { get; set; }

    /// <summary>
    /// Gets/sets the cloud event's type
    /// </summary>
    [Description("The cloud event's type")]
    [Required]
    [DataMember(Order = 4, Name = "source"), JsonPropertyOrder(4), JsonPropertyName("source")]
    public required Uri Source { get; set; }

    /// <summary>
    /// Gets/sets the cloud event's type
    /// </summary>
    [Description]
    [Required]
    [DataMember(Order = 5, Name = "type"), JsonPropertyOrder(5), JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Gets/sets a value that describes the subject of the event in the context of the event producer. Used as correlation id by default.
    /// </summary>
    [Description("A value that describes the subject of the event in the context of the event producer. Used as correlation id by default.")]
    [DataMember(Order = 6, Name = "subject"), JsonPropertyOrder(6), JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Gets/sets the cloud event's data content type. Defaults to <see cref="MediaTypeNames.Application.Json"/>
    /// </summary>
    [Description("The cloud event's data content type. Defaults to application/json")]
    [DefaultValue(MediaTypeNames.Application.Json)]
    [DataMember(Order = 7, Name = "datacontenttype"), JsonPropertyOrder(7), JsonPropertyName("datacontenttype")]
    public string? DataContentType { get; set; } = MediaTypeNames.Application.Json;

    /// <summary>
    /// Gets/sets an <see cref="Uri"/> that references the versioned schema of the event's data
    /// </summary>
    [Description("An URI that references the versioned schema of the event's data")]
    [DataMember(Order = 8, Name = "dataschema"), JsonPropertyOrder(8), JsonPropertyName("dataschema")]
    public Uri? DataSchema { get; set; }

    /// <summary>
    /// Gets/sets the event's data, if any. Only used if the event has been formatted using the structured mode
    /// </summary>
    [Description("The event's data, if any. Only used if the event has been formatted using the structured mode")]
    [DataMember(Order = 9, Name = "data"), JsonPropertyOrder(9), JsonPropertyName("data")]
    public object? Data { get; set; }

    /// <summary>
    /// Gets/sets the event's binary data, encoded in base 64. Only used if the event has been formatted using the binary mode
    /// </summary>
    [Description("The event's binary data, encoded in base 64. Only used if the event has been formatted using the binary mode")]
    [DataMember(Order = 10, Name = "database64"), JsonPropertyOrder(10), JsonPropertyName("database64")]
    public string? DataBase64 { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> that contains the event's extension attributes
    /// </summary>
    [DataMember(Order = 11, Name = "extensionAttributes"), JsonExtensionData]
    public IDictionary<string, JsonElement>? ExtensionAttributes { get; set; }

    /// <summary>
    /// Gets/sets the specified attribute
    /// </summary>
    /// <param name="attributeName">The name of the attribute to set</param>
    /// <returns>The attribute's value</returns>
    public object? this[string attributeName] => GetAttribute(attributeName);

    /// <summary>
    /// Gets the specified attribute
    /// </summary>
    /// <param name="name">The name of the attribute to get</param>
    /// <returns>The value of the specified attribute</returns>
    public object? GetAttribute(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        switch (name)
        {
            case CloudEventAttributes.Id: return Id;
            case CloudEventAttributes.SpecVersion: return SpecVersion;
            case CloudEventAttributes.Time: return Time;
            case CloudEventAttributes.Source: return Source;
            case CloudEventAttributes.Type: return Type;
            case CloudEventAttributes.Subject: return Subject;
            case CloudEventAttributes.DataContentType: return DataContentType;
            case CloudEventAttributes.DataSchema: return DataSchema;
            case CloudEventAttributes.Data: return Data;
            case CloudEventAttributes.DataBase64: return DataBase64;
            default:
                if (ExtensionAttributes?.TryGetValue(name, out var value) == true) return value;
                else return null;
        }
    }

    /// <inheritdoc/>
    public override string ToString() => Id;

}
