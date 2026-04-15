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
/// Defines the fundamentals of a schema validation result
/// </summary>
public interface ISchemaValidationResult
{

    /// <summary>
    /// Gets a boolean indicating whether or not the validation result is valid
    /// </summary>
    bool IsValid { get; }

    /// <summary>
    /// Gets a mapping of errors, if any, that occurred during the validation process. The keys of the mapping represent the paths to the invalid nodes, while the values are lists of error messages related to each path.
    /// </summary>
    IReadOnlyDictionary<string, IReadOnlyList<string>>? Errors { get; }

}