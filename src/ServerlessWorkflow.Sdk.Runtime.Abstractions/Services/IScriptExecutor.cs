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
/// Defines the fundamentals of a service used to execute scripts
/// </summary>
public interface IScriptExecutor
{

    /// <summary>
    /// Determines whether this <see cref="IScriptExecutor"/> supports executing scripts written in the specified language
    /// </summary>
    /// <param name="language">The language to check for support</param>
    /// <returns>A boolean indicating whether this <see cref="IScriptExecutor"/> supports executing scripts written in the specified language</returns>
    bool Supports(string language);

    /// <summary>
    /// Executes a script with the specified code, language, arguments and environment variables, returning the associated <see cref="Process"/>
    /// </summary>
    /// <param name="script">The code of the script to execute</param>
    /// <param name="arguments">An optional collection of arguments to pass to the script being executed</param>
    /// <param name="environment">A optional dictionary of environment variables to set for the script being executed</param>
    /// <param name="cancellationToken">A <cref name="CancellationToken"/></param>
    /// <returns>A new <see cref="Process"/> used to execute the script</returns>
    Task<Process> ExecuteAsync(string script, IEnumerable<string>? arguments = null, IDictionary<string, string>? environment = null, CancellationToken cancellationToken = default);

}
