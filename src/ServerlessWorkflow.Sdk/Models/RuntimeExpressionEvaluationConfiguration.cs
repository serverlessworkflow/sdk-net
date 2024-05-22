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
[DataContract]
public record RuntimeExpressionEvaluationConfiguration
{

    /// <summary>
    /// Gets/sets the language used for writing runtime expressions. Defaults to <see cref="RuntimeExpressions.Languages.JQ"/>.
    /// </summary>
    [DataMember(Name = "language", Order = 1), JsonPropertyName("language"), JsonPropertyOrder(1), YamlMember(Alias = "language", Order = 1)]
    public virtual string Language { get; set; } = RuntimeExpressions.Languages.JQ;

    /// <summary>
    /// Gets/sets the language used for writing runtime expressions. Defaults to <see cref="RuntimeExpressionEvaluationMode.Strict"/>
    /// </summary>
    [DataMember(Name = "mode", Order = 2), JsonPropertyName("mode"), JsonPropertyOrder(2), YamlMember(Alias = "mode", Order = 2)]
    public virtual string? Mode { get; set; }

}