namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class AsyncApiCallDefinitionFactory
{
    internal static AsyncApiCallDefinition Create() => new()
    {
        Document = ExternalResourceDefinitionFactory.Create(),
        Operation = "onUserSignedUp",
        Server = "production",
        Protocol = "amqp",
        Message = AsyncApiMessageDefinitionFactory.Create(),
        Subscription = AsyncApiSubscriptionDefinitionFactory.Create(),
        Authentication = AuthenticationPolicyDefinitionFactory.CreateBearer()
    };
}
