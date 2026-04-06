namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ErrorDefinitionFactory
{
    internal static ErrorDefinition Create() => new()
    {
        Type = "https://example.com/errors/not-found",
        Title = "Not Found",
        Status = "404",
        Detail = "The requested resource was not found",
        Instance = "https://example.com/errors/not-found/12345"
    };
}
