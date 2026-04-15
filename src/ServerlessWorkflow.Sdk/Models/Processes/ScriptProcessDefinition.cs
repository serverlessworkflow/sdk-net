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

namespace ServerlessWorkflow.Sdk.Models.Processes;

/// <summary>
/// Represents the definition of a script evaluation process
/// </summary>
[Description("Represents the definition of a script evaluation process")]
[DataContract]
public sealed record ScriptProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the language of the script to run
    /// </summary>
    [Description("The language of the script to run")]
    [DataMember(Order = 1, Name = "language"), JsonPropertyOrder(1), JsonPropertyName("language")]
    public required string Language { get; init; }

    /// <summary>
    /// Gets/sets the script's code. Required if <see cref="Source"/> has not been set.
    /// </summary>
    [Description("The script's code. Required if Source has not been set.")]
    [DataMember(Order = 2, Name = "code"), JsonPropertyOrder(2), JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>
    /// Gets the the script's source. Required if <see cref="Code"/> has not been set.
    /// </summary>
    [Description("The script's source. Required if Code has not been set.")]
    [DataMember(Order = 3, Name = "source"), JsonPropertyOrder(3), JsonPropertyName("source")]
    public ExternalResourceDefinition? Source { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the arguments, if any, to pass to the script to run
    /// </summary>
    [Description("A key/value mapping of the arguments, if any, to pass to the script to run")]
    [DataMember(Order = 4, Name = "arguments"), JsonPropertyOrder(4), JsonPropertyName("arguments")]
    public EquatableDictionary<string, object>? Arguments { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the environment variables, if any, to use when running the configured process
    /// </summary>
    [Description("A key/value mapping of the environment variables, if any, to use when running the configured process")]
    [DataMember(Order = 5, Name = "environment"), JsonPropertyOrder(5), JsonPropertyName("environment")]
    public EquatableDictionary<string, string>? Environment { get; init; }

}