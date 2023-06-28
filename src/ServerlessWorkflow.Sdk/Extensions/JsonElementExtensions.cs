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

using Json.More;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="JsonElement"/>s
/// </summary>
public static class JsonElementExtensions
{

    /// <summary>
    /// Unwraps the <see cref="JsonElement"/> into a new, non-JSON value
    /// </summary>
    /// <param name="jsonElement">The <see cref="JsonElement"/> to unwrap</param>
    /// <returns>The unwrapped value</returns>
    public static object? ToObject(this JsonElement jsonElement) => jsonElement.AsNode()?.ToObject();

}
