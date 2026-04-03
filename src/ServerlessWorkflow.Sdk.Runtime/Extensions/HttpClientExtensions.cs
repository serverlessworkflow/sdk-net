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