#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="AuthenticationPolicyDefinition"/>s
/// </summary>
public static class AuthenticationPolicyDefinitionExtensions
{

    /// <summary>
    /// Attempts to get the name of the secret, if any, on which the <see cref="AuthenticationPolicyDefinition"/> is based
    /// </summary>
    /// <param name="authentication">The extended <see cref="AuthenticationPolicyDefinition"/></param>
    /// <param name="secretName">The name of the secret, if any, on which the <see cref="AuthenticationPolicyDefinition"/> is based</param>
    /// <returns>A boolean indicating whether or not the <see cref="AuthenticationPolicyDefinition"/> is secret based</returns>
    public static bool TryGetBaseSecret(this AuthenticationPolicyDefinition authentication, out string? secretName)
    {
        secretName = authentication.Scheme switch
        {
            AuthenticationScheme.Basic => authentication.Basic?.Use,
            AuthenticationScheme.Bearer => authentication.Bearer?.Use,
            AuthenticationScheme.Certificate => authentication.Certificate?.Use,
            AuthenticationScheme.Digest => authentication.Digest?.Use,
            AuthenticationScheme.OAuth2 => authentication.OAuth2?.Use,
            AuthenticationScheme.OpenIDConnect => authentication.Oidc?.Use,
            _ => throw new NotSupportedException($"The specified authentication schema '{authentication.Scheme}' is not supported")
        };
        return !string.IsNullOrWhiteSpace(secretName);
    }

}
