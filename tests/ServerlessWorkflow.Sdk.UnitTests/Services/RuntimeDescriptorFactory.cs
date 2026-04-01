namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RuntimeDescriptorFactory
{
    internal static RuntimeDescriptor Create() => new()
    {
        Name = "test",
        Version = "1.0.0",
        Metadata = new()
        {
            ["key"] = "value"
        }
    };
}
