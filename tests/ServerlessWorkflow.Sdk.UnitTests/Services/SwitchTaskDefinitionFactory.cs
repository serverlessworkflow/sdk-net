namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class SwitchTaskDefinitionFactory
{
    internal static SwitchTaskDefinition Create()
    {
        var cases = new Map<string, SwitchCaseDefinition>();
        cases.Add(new MapEntry<string, SwitchCaseDefinition>("approved", SwitchCaseDefinitionFactory.Create()));
        cases.Add(new MapEntry<string, SwitchCaseDefinition>("default", new SwitchCaseDefinition { Then = "end" }));
        return new()
        {
            Switch = cases
        };
    }
}
