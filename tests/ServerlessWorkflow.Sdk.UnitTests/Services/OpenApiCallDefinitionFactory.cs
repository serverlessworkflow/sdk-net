namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OpenApiCallDefinitionFactory
{
    internal static OpenApiCallDefinition Create() => new()
    {
        Document = ExternalResourceDefinitionFactory.Create(),
        OperationId = "getPetById",
        Parameters = new()
        {
            ["id"] = 123  
        },
        Authentication = AuthenticationPolicyDefinitionFactory.CreateBasic(),
        Output = HttpOutputFormat.Content,
        Redirect = false
    };
}
