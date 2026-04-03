namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class HttpRequestFactory
{
    internal static HttpRequest Create() => new()
    {
        Method = "POST",
        Uri = new("https://api.example.com/resources"),
        Headers = new EquatableDictionary<string, string>
        {
            ["Content-Type"] = "application/json",
            ["Authorization"] = "Bearer token123"
        },
        Body = new JsonObject { ["name"] = "test" }
    };
}
