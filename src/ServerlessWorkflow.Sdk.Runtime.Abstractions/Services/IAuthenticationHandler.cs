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
