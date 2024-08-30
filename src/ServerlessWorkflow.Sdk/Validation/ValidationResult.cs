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

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the result of a validation attempt
/// </summary>
[DataContract]
public record ValidationResult
    : IValidationResult
{

    /// <inheritdoc/>
    [DataMember(Name = "isValid", Order = 1), JsonPropertyName("isValid"), JsonPropertyOrder(1), YamlMember(Alias = "isValid", Order = 1)]
    public virtual bool IsValid => this.Errors?.Count < 1;

    /// <inheritdoc/>
    [DataMember(Name = "errors", Order = 2), JsonPropertyName("errors"), JsonPropertyOrder(2), YamlMember(Alias = "errors", Order = 2)]
    public virtual IReadOnlyCollection<ValidationError>? Errors { get; set; }

}
