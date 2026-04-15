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
/// Represents an helper class used to perform string formatting operations
/// </summary>
public static class StringFormatter
{

    /// <summary>
    /// Formats the specified string template by replacing the placeholders with the provided parameters
    /// </summary>
    /// <param name="template">The string template containing the placeholders to replace</param>
    /// <param name="parameters">A key-value collection containing the parameters to replace in the template, where the key represents the placeholder name and the value represents the value to replace it with</param>
    /// <returns>The formatted string</returns>
    public static string Format(string template, IDictionary<string, object?>? parameters)
    {
        if (string.IsNullOrWhiteSpace(template) || parameters == null || !parameters.Any()) return template;
        var text = template;
        foreach (var parameter in parameters) text = text.Replace("{" + parameter.Key + "}", parameter.Value?.ToString(), StringComparison.OrdinalIgnoreCase);
        return text;
    }

}