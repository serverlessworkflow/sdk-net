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
/// Defines the fundamentals of a service used to handle authentication policies
/// </summary>
public interface IAuthenticationHandler
{

    /// <summary>
    /// Handles the specified authentication policy and returns an <see cref="IAuthenticationResult"/> that can be used to authenticate requests to external resources
    /// </summary>
    /// <param name="policy">The <see cref="AuthenticationPolicyDefinition"/> to handle</param>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/>, if any, that defines the authentication policy to handle</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAuthenticationResult"/></returns>
    Task<IAuthenticationResult> HandleAsync(AuthenticationPolicyDefinition policy, WorkflowDefinition? workflow = null, CancellationToken cancellationToken = default);

}
