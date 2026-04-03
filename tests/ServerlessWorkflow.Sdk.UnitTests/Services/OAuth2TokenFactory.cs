using ServerlessWorkflow.Sdk.Runtime.Models;

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OAuth2TokenFactory
{
    internal static OAuth2Token Create() => new()
    {
        CreatedAt = new DateTime(2026, 4, 3, 12, 0, 0, DateTimeKind.Utc),
        TokenType = "Bearer",
        TokenId = "token-123",
        AccessToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.test",
        RefreshToken = "refresh-token-456",
        Ttl = 3600,
        ExpiresAt = new DateTime(2026, 4, 3, 13, 0, 0, DateTimeKind.Utc)
    };
}
