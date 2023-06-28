﻿// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Defines the fundamentals of an object used to describe a <see cref="WorkflowDefinition"/>'s validation results
/// </summary>
public interface IWorkflowValidationResult
{

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the schema-related validation errors that have occured during the <see cref="WorkflowDefinition"/>'s validation
    /// </summary>
    IEnumerable<KeyValuePair<string, string>>? SchemaValidationErrors { get; }

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the DSL-related validation errors that have occured during the <see cref="WorkflowDefinition"/>'s validation
    /// </summary>
    IEnumerable<KeyValuePair<string, string>>? DslValidationErrors { get; }

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="WorkflowDefinition"/> is valid
    /// </summary>
    bool IsValid { get; }

}
