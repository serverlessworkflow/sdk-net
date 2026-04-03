namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class HttpResponseFactory
{
    internal static HttpResponse Create() => new()
    {
        Request = HttpRequestFactory.Create(),
        StatusCode = 200,
        Headers = new EquatableDictionary<string, string>
        {
            ["Content-Type"] = "application/json"
        },
        Content = new JsonObject { ["id"] = 1, ["name"] = "test" }
    };
}
