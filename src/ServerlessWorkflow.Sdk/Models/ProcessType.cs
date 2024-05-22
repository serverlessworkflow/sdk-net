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
/// Exposes process types
/// </summary>
public static class ProcessType
{

    /// <summary>
    /// Gets the 'container' process type
    /// </summary>
    public const string Container = "container";
    /// <summary>
    /// Gets the 'script' process type
    /// </summary>
    public const string Script = "script";
    /// <summary>
    /// Gets the 'shell' process type
    /// </summary>
    public const string Shell = "shell";
    /// <summary>
    /// Gets the 'workflow' process type
    /// </summary>
    public const string Workflow = "workflow";
    /// <summary>
    /// Gets the 'extension' process type
    /// </summary>
    public const string Extension = "extension";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported process types 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Container;
        yield return Script;
        yield return Shell;
        yield return Workflow;
    }

}
