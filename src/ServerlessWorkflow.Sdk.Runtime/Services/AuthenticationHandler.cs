namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IAuthenticationHandler"/> interface
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="secretsManager">The service used to manage secrets</param>
/// <param name="oauth2TokenManager">The service used to manage OAuth2 tokens</param>
public sealed class AuthenticationHandler(ILogger<AuthenticationHandler> logger, ISecretsManager secretsManager, IOAuth2TokenManager oauth2TokenManager)
    : IAuthenticationHandler
{

    /// <inheritdoc/>
    public async Task<IAuthenticationResult> HandleAsync(AuthenticationPolicyDefinition policy, WorkflowDefinition? workflow = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(policy);
        if (!string.IsNullOrWhiteSpace(policy.Use))
        {
            if (workflow?.Use?.Authentications?.TryGetValue(policy.Use, out AuthenticationPolicyDefinition? referencedAuthentication) != true || referencedAuthentication == null) throw new NullReferenceException($"Failed to find the specified authentication policy '{policy.Use}'");
            else policy = referencedAuthentication;
        }
        var isSecretBased = policy.TryGetBaseSecret(out var secretName);
        JsonNode? authenticationProperties = null;
        if (isSecretBased && !string.IsNullOrWhiteSpace(secretName))
        {
            logger.LogDebug("Authentication is secret based");
            var secrets = await secretsManager.GetAsync(cancellationToken).ConfigureAwait(false);
            if (!secrets.TryGetValue(secretName, out authenticationProperties) || authenticationProperties is null)
            {
                logger.LogError("Failed to resolve the specified secret '{secret}'", secretName);
                throw new NullReferenceException($"Failed to resolve the specified secret '{secretName}'");
            }
            logger.LogDebug("Authentication secret loaded");
        }
        string scheme, parameter;
        switch (policy.Scheme)
        {
            case AuthenticationScheme.Basic:
                if (policy.Basic == null) throw new Exception("Missing or invalid configuration of the specified authentication scheme");
                var basic = authenticationProperties is null ? policy.Basic : JsonSerializer.Deserialize(authenticationProperties, Sdk.Serialization.Json.JsonSerializationContext.Default.BasicAuthenticationSchemeDefinition)!;
                scheme = AuthenticationScheme.Basic;
                parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{basic.Username}:{basic.Password}"));
                break;
            case AuthenticationScheme.Bearer:
                if (policy.Bearer == null) throw new Exception("Missing or invalid configuration of the specified authentication scheme");
                var bearer = authenticationProperties is null ? policy.Bearer : JsonSerializer.Deserialize(authenticationProperties, Sdk.Serialization.Json.JsonSerializationContext.Default.BearerAuthenticationSchemeDefinition)!;
                scheme = AuthenticationScheme.Bearer;
                parameter = bearer.Token ?? throw new Exception("The Bearer token must be set");
                break;
            case AuthenticationScheme.OAuth2:
                if (policy.OAuth2 == null) throw new Exception("Missing or invalid configuration of the specified authentication scheme");
                var oauth2 = authenticationProperties is null ? policy.OAuth2 : JsonSerializer.Deserialize(authenticationProperties, Sdk.Serialization.Json.JsonSerializationContext.Default.OAuth2AuthenticationSchemeDefinition)!;
                scheme = AuthenticationScheme.Bearer;
                var token = await oauth2TokenManager.GetTokenAsync(oauth2, cancellationToken).ConfigureAwait(false) ?? throw new NullReferenceException("Failed to generate an OAUTH2 token");
                parameter = token.AccessToken!;
                break;
            case AuthenticationScheme.OpenIDConnect:
                if (policy.Oidc == null) throw new Exception("Missing or invalid configuration of the specified authentication scheme");
                var oidc = authenticationProperties is null ? policy.Oidc : JsonSerializer.Deserialize(authenticationProperties, Sdk.Serialization.Json.JsonSerializationContext.Default.OpenIDConnectSchemeDefinition)!;
                scheme = AuthenticationScheme.Bearer;
                token = await oauth2TokenManager.GetTokenAsync(oidc, cancellationToken).ConfigureAwait(false) ?? throw new NullReferenceException("Failed to generate an OIDC token");
                parameter = token.AccessToken!;
                break;
            default:
                throw new NotSupportedException($"The specified authentication schema '{policy.Scheme}' is not supported");
        }
        return new AuthenticationResult(scheme, parameter);
    }

}