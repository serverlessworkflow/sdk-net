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

using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes all supported HTTP call output formats
/// </summary>
public static class HttpOutputFormat
{

    /// <summary>
    /// Indicates that the HTTP call should output the HTTP response's raw content
    /// </summary>
    public const string Raw = "raw";
    /// <summary>
    /// Indicates that the HTTP call should output the HTTP response's content, possibly deserialized
    /// </summary>
    public const string Content = "content";
    /// <summary>
    /// Indicates that the HTTP call should output an <see cref="HttpResponse"/>
    /// </summary>
    public const string Response = "response";

}