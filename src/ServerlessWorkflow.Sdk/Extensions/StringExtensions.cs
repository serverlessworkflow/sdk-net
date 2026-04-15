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

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="string"/>s
/// </summary>
public static class StringExtensions
{

    /// <summary>
    /// Determines whether the specified string is formatted as a runtime expression.
    /// </summary>
    /// <param name="value">The string to evaluate for runtime expression formatting.</param>
    /// <returns>true if the string starts with "${" and ends with "}"; otherwise, false.</returns>
    public static bool IsRuntimeExpression(this string value) => value.TrimStart().StartsWith("${") && value.TrimEnd().EndsWith("}");

}