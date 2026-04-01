namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ComponentDefinitionCollectionFactory
{
    internal static ComponentDefinitionCollection Create() => new()
    {
        Authentications = new EquatableDictionary<string, AuthenticationPolicyDefinition>()
        {
            ["basicAuth"] = AuthenticationPolicyDefinitionFactory.CreateBasic()
        },
        Retries = new EquatableDictionary<string, RetryPolicyDefinition>()
        {
            ["defaultRetry"] = RetryPolicyDefinitionFactory.Create()
        },
        Secrets = ["my-secret-1", "my-secret-2"]
    };
}
