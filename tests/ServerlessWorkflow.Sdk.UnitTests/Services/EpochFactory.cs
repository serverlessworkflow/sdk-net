namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class EpochFactory
{     
    internal static Epoch Create() => new()
    {
        Milliseconds = 69
    };
}
