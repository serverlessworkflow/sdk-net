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
/// Defines the fundamentals of a service used to provide <see cref="IScriptExecutor"/>s
/// </summary>
public interface IScriptExecutorProvider
{

    /// <summary>
    /// Gets the <see cref="IScriptExecutor"/> for the specified language.
    /// </summary>
    /// <param name="language">The scripting language.</param>
    /// <returns>The <see cref="IScriptExecutor"/> for the specified language.</returns>
    IScriptExecutor? GetExecutor(string language);

}
