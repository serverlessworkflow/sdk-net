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
/// Exposes the titles of all default ServerlessWorkflow errors
/// </summary>
public static class ErrorTitle
{

    /// <summary>
    /// Gets the title of communication errors
    /// </summary>
    public const string Communication = "Communication Error";
    /// <summary>
    /// Gets the title of configuration errors
    /// </summary>
    public const string Configuration = "Configuration Error";
    /// <summary>
    /// Gets the title of runtime errors
    /// </summary>
    public const string Runtime = "Runtime Error";
    /// <summary>
    /// Gets the title of timeout errors
    /// </summary>
    public const string Timeout = "Timeout Error";
    /// <summary>
    /// Gets the title of validation errors
    /// </summary>
    public const string Validation = "Validation Error";

}
