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
/// Represents an object used to describe the result of a schema validation operation
/// </summary>
[Description("Represents an object used to describe the result of a schema validation operation")]
[DataContract]
public sealed record SchemaValidationResult
    : ISchemaValidationResult
{

    /// <inheritdoc/>
    [Description("Indicates whether the validation operation was successful or not")]
    [DataMember(Order = 1, Name = "isValid"), JsonPropertyOrder(1), JsonPropertyName("isValid")]
    public required bool IsValid { get; init; }

    /// <inheritdoc/>
    [Description("A mapping containing validation errors, if any")]
    [DataMember(Name = "errors", Order = 2), JsonPropertyOrder(2), JsonPropertyName("errors")]
    public IReadOnlyDictionary<string, IReadOnlyList<string>>? Errors { get; init; }

    /// <summary>
    /// Creates a new <see cref="SchemaValidationResult"/> indicating a successful validation operation
    /// </summary>
    /// <returns>A new <see cref="SchemaValidationResult"/> indicating a successful validation operation</returns>
    public static SchemaValidationResult Succeeded() => new()
    {
        IsValid = true
    };

    /// <summary>
    /// Creates a new <see cref="SchemaValidationResult"/> indicating a failed validation operation
    /// </summary>
    /// <param name="errors">A mapping containing validation errors, if any</param>
    /// <returns>A new <see cref="SchemaValidationResult"/> indicating a failed validation operation</returns>
    public static SchemaValidationResult Failed(IReadOnlyDictionary<string, IReadOnlyList<string>> errors) => new()
    {
        IsValid = false,
        Errors = errors
    };

    /// <summary>
    /// Creates a new <see cref="SchemaValidationResult"/> indicating a failed validation operation
    /// </summary>
    /// <param name="errors">A mapping containing validation errors, if any</param>
    /// <returns>A new <see cref="SchemaValidationResult"/> indicating a failed validation operation</returns>
    public static SchemaValidationResult Failed(IEnumerable<KeyValuePair<string, IEnumerable<string>>> errors) => new()
    {
        IsValid = false,
        Errors = errors.ToDictionary(e => e.Key, e => e.Value.ToArray() as IReadOnlyList<string>)
    };

}