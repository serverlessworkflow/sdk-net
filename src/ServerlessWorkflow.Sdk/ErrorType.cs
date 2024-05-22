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
/// Exposes ServerlessWorkflow default error types
/// </summary>
public static class ErrorType
{

    const string BaseUri = "https://serverlessworkflow.io/dsl/errors/types";
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for communication errors
    /// </summary>
    public static readonly Uri Communication = new($"{BaseUri}/communication");
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for configuration errors
    /// </summary>
    public static readonly Uri Configuration = new($"{BaseUri}/configuration");
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for runtime errors
    /// </summary>
    public static readonly Uri Runtime = new($"{BaseUri}/runtime");
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for timeout errors
    /// </summary>
    public static readonly Uri Timeout = new($"{BaseUri}/timeout");
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for validation errors
    /// </summary>
    public static readonly Uri Validation = new($"{BaseUri}/validation");

}
