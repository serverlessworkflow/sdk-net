namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class SwitchCaseDefinitionFactory
{
    internal static SwitchCaseDefinition Create() => new()
    {
        When = "${ .orderStatus == 'approved' }",
        Then = "processOrder"
    };
}
