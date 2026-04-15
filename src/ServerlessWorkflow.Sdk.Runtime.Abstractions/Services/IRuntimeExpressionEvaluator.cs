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

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to evaluate runtime expressions
/// </summary>
public interface IRuntimeExpressionEvaluator
{

    /// <summary>
    /// Determines whether the specified language is supported by the expression evaluator
    /// </summary>
    /// <param name="language">The expression language to check</param>
    /// <returns>A boolean indicating whether the specified language is supported by the expression evaluator</returns>
    bool Supports(string language);

    /// <summary>
    /// Evaluates the specified expression with the given input and arguments, if any
    /// </summary>
    /// <param name="expression">The expression to evaluate</param>
    /// <param name="input">The input to evaluate the expression with</param>
    /// <param name="arguments">The arguments, if an, to evaluate the expression with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The result, if any, of the expression evaluation</returns>
    Task<JsonNode?> EvaluateAsync(string expression, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default);

}
