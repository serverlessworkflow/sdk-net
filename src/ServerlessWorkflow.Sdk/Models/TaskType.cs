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
/// Exposes task types
/// </summary>
public static class TaskType
{

    /// <summary>
    /// Gets the 'call' task type
    /// </summary>
    public const string Call = "call";
    /// <summary>
    /// Gets the 'do' task type
    /// </summary>
    public const string Do = "do";
    /// <summary>
    /// Gets the 'emit' task type
    /// </summary>
    public const string Emit = "emit";
    /// <summary>
    /// Gets the 'extension' task type
    /// </summary>
    public const string Extension = "extension";
    /// <summary>
    /// Gets the 'for' task type
    /// </summary>
    public const string For = "for";
    /// <summary>
    /// Gets the 'fork' task type
    /// </summary>
    public const string Fork = "fork";
    /// <summary>
    /// Gets the 'listen' task type
    /// </summary>
    public const string Listen = "listen";
    /// <summary>
    /// Gets the 'raise' task type
    /// </summary>
    public const string Raise = "raise";
    /// <summary>
    /// Gets the 'run' task type
    /// </summary>
    public const string Run = "run";
    /// <summary>
    /// Gets the 'set' task type
    /// </summary>
    public const string Set = "set";
    /// <summary>
    /// Gets the 'switch' task type
    /// </summary>
    public const string Switch = "switch";
    /// <summary>
    /// Gets the 'try' task type
    /// </summary>
    public const string Try = "try";
    /// <summary>
    /// Gets the 'wait' task type
    /// </summary>
    public const string Wait = "wait";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported task types 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Call;
        yield return Do;
        yield return Emit;
        yield return Extension;
        yield return For;
        yield return Listen;
        yield return Raise;
        yield return Run;
        yield return Set;
        yield return Switch;
        yield return Try;
        yield return Wait;
    }

}