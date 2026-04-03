namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class HttpCallDefinitionFactory
{
    internal static HttpCallDefinition Create() => new()
    {
        Method = "POST",
        Endpoint = EndpointDefinitionFactory.Create(),
        Headers = new EquatableDictionary<string, string>
        {
            ["Content-Type"] = "application/json",
            ["Accept"] = "application/json"
        },
        Body = new JsonObject { ["name"] = "test", ["value"] = 42 },
        Output = HttpOutputFormat.Response,
        Redirect = true
    };
}
