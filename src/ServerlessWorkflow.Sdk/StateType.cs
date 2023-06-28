// Copyright © 2023-Present The Serverless Workflow Specification Authors
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
/// Enumerates all types of states
/// </summary>
public static class StateType
{

    /// <summary>
    /// Indicates an operation state
    /// </summary>
    public const string Operation = "operation";

    /// <summary>
    /// Indicates a sleep state
    /// </summary>
    public const string Sleep = "sleep";

    /// <summary>
    /// Indicates an event state
    /// </summary>
    public const string Event = "event";

    /// <summary>
    /// Indicates a parallel state
    /// </summary>
    public const string Parallel = "parallel";

    /// <summary>
    /// Indicates a switch state
    /// </summary>
    public const string Switch = "switch";

    /// <summary>
    /// Indicates an inject state
    /// </summary>
    public const string Inject = "inject";

    /// <summary>
    /// Indicates a foreach state
    /// </summary>
    public const string ForEach = "foreach";

    /// <summary>
    /// Indicates a callback state
    /// </summary>
    public const string Callback = "callback";

    /// <summary>
    /// Gets all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> GetValues()
    {
        yield return Operation;
        yield return Sleep;
        yield return Event;
        yield return Parallel;
        yield return Switch;
        yield return Inject;
        yield return ForEach;
        yield return Callback;
    }

}
