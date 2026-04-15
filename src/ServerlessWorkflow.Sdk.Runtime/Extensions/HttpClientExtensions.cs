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

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="HttpClient"/>s
/// </summary>
public static class HttpClientExtensions
{

    /// <summary>
    /// Configures the <see cref="HttpClient"/> to use the specified authentication mechanism 
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> to configure</param>
    /// <param name="policy">An object that describes the authentication mechanism to use</param>
    /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/>, if any, that defines the authentication to configure</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    public static async Task ConfigureAuthenticationAsync(this HttpClient httpClient, AuthenticationPolicyDefinition? policy, IServiceProvider serviceProvider, WorkflowDefinition? workflow = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        if (policy == null) return;
        var authenticationHandler = serviceProvider.GetRequiredService<IAuthenticationHandler>();
        var authenticationResult = await authenticationHandler.HandleAsync(policy, workflow, cancellationToken).ConfigureAwait(false);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationResult.Scheme, authenticationResult.Value);
    }

}