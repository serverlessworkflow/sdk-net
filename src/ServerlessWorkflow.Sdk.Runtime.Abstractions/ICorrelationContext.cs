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
/// Defines the fundamentals of a correlation context
/// </summary>
public interface ICorrelationContext
{

    /// <summary>
    /// Gets the context's unique identifier
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the context's status
    /// </summary>
    string Status { get; }

    /// <summary>
    /// Gets a key/value mapping of the context's correlation keys
    /// </summary>
    EquatableDictionary<string, string> Keys { get; }

    /// <summary>
    /// Gets a key/value mapping of all correlated events, with the key being the index of the matched correlation filter
    /// </summary>
    EquatableDictionary<int, ICloudEvent> Events { get; }

    /// <summary>
    /// Gets the offset that serves as the index of the event being processed by the consumer, if streaming has been enabled for the correlation associated with the context.
    /// </summary>
    uint? Offset { get; init; }

}