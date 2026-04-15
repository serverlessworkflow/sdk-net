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
/// Represents an object used to configure the workflow's runtime expression evaluation
/// </summary>
[Description("Represents an object used to configure the workflow's runtime expression evaluation.")]
[DataContract]
public sealed record RuntimeExpressionEvaluationConfiguration
{

    /// <summary>
    /// Gets/sets the language used for writing runtime expressions. Defaults to <see cref="RuntimeExpressions.Languages.JQ"/>.
    /// </summary>
    [Description("The language used for writing runtime expressions. Defaults to JQ.")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "language"), JsonPropertyOrder(1), JsonPropertyName("language")]
    public string Language { get; init; } = RuntimeExpressions.Languages.JQ;

    /// <summary>
    /// Gets/sets the language used for writing runtime expressions. Defaults to <see cref="RuntimeExpressionEvaluationMode.Strict"/>
    /// </summary>
    [Description("The mode used for evaluating runtime expressions. Defaults to Strict.")]
    [DataMember(Order = 2, Name = "mode"), JsonPropertyOrder(2), JsonPropertyName("mode")]
    public string? Mode { get; init; }

}