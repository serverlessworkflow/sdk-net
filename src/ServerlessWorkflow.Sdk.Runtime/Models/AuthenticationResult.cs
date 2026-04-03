namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents the result of an authentication operation
/// </summary>
/// <param name="Scheme">The authentication scheme</param>
/// <param name="Value">The authentication value</param>
[Description("Represents the result of an authentication operation")]
[DataContract]
public sealed record AuthenticationResult(string Scheme, string Value)
    : IAuthenticationResult
{

    /// <inheritdoc/>
    [Description("The OAUTH2 token")]
    [DataMember(Order = 1, Name = "token"), JsonPropertyOrder(1), JsonPropertyName("token")]
    public string Scheme { get; init; } = Scheme;

    /// <inheritdoc/>
    [Description("The OAUTH2 token")]
    [DataMember(Order = 2, Name = "token"), JsonPropertyOrder(2), JsonPropertyName("token")]
    public string Value { get; init; } = Value;

}