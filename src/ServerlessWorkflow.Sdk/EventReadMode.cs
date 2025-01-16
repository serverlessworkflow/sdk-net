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
/// Enumerates all supported event read modes
/// </summary>
public static class EventReadMode
{

    /// <summary>
    /// Indicates that only the data of consumed events should be read
    /// </summary>
    public const string Data = "data";
    /// <summary>
    /// Indicates that the whole event envelope should be read, including context attributes
    /// </summary>
    public const string Envelope = "envelope";
    /// <summary>
    /// Indicates that the event's raw data should be read, without additional transformation (i.e. deserialization)
    /// </summary>
    public const string Raw = "raw";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported event read modes
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported event read modes</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Data;
        yield return Envelope;
        yield return Raw;
    }

}