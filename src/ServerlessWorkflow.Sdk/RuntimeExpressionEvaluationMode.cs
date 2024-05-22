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

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes the default runtime expression evaluation modes
/// </summary>
public static class RuntimeExpressionEvaluationMode
{

    /// <summary>
    /// Gets the 'strict' runtime expression evaluation mode, which requires all expressions to be enclosed within ${ } for proper identification and evaluation.
    /// </summary>
    public const string Strict = "strict";
    /// <summary>
    /// Gets the 'loose' runtime expression evaluation mode, which evaluates any value provided. If the evaluation fails, it results in a string with the expression as its content.
    /// </summary>
    public const string Loose = "loose";

}