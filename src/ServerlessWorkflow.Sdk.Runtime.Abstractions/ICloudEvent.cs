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

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a <see href="https://cloudevents.io/">Cloud Event</see>
/// </summary>
public interface ICloudEvent
{

    /// <summary>
    /// Gets/sets the version of the CloudEvents specification which the event uses
    /// </summary>
    string SpecVersion { get; }

    /// <summary>
    /// Gets/sets the date and time at which the event has been produced
    /// </summary>
    DateTimeOffset? Time { get; }

    /// <summary>
    /// Gets/sets the cloud event's type
    /// </summary>
    Uri Source { get; }

    /// <summary>
    /// Gets/sets the cloud event's type
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Gets/sets a value that describes the subject of the event in the context of the event producer. Used as correlation id by default.
    /// </summary>
    string? Subject { get; }

    /// <summary>
    /// Gets/sets the cloud event's data content type. Defaults to <see cref="MediaTypeNames.Application.Json"/>
    /// </summary>
    string? DataContentType { get; }

    /// <summary>
    /// Gets/sets an <see cref="Uri"/> that references the versioned schema of the event's data
    /// </summary>
    Uri? DataSchema { get; }

    /// <summary>
    /// Gets/sets the event's data, if any. Only used if the event has been formatted using the structured mode
    /// </summary>
    object? Data { get; }

    /// <summary>
    /// Gets/sets the event's binary data, encoded in base 64. Only used if the event has been formatted using the binary mode
    /// </summary>
    string? DataBase64 { get; }

    /// <summary>
    /// Gets a <see cref="IDictionary{TKey, TValue}"/> that contains the event's extension attributes
    /// </summary>
    IDictionary<string, JsonElement>? ExtensionAttributes { get; }

}
