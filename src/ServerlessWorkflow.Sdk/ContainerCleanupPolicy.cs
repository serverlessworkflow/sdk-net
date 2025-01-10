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
/// Enumerates all supported container cleanup policies
/// </summary>
public static class ContainerCleanupPolicy
{

    /// <summary>
    /// Indicates that the runtime must delete the container immediately after execution
    /// </summary>
    public const string Always = "always";
    /// <summary>
    /// Indicates that the runtime must eventually delete the container, after waiting for a specific amount of time.
    /// </summary>
    public const string Eventually = "eventually";
    /// <summary>
    /// Indicates that the runtime must never delete the container.
    /// </summary>
    public const string Never = "never";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Always;
        yield return Eventually;
        yield return Never;
    }

}